using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum GameState
{
    Playing,
    Pausing,
    Ending
}

public class GameController : MonoBehaviour
{
    [SerializeField] private float multiplierAmount;
    public static GameController Instance { get; private set; }
    public PlayerController player;
    private int _enemiesCount;
    [HideInInspector] public float speedMultiplier;
    public GameState gameState;
    private int _points;
    private int _highScore;
    public event Action<float> OnChangeSpawnTimer;

    private int Points
    {
        get => _points;
        set
        {
            _points = value;
            OnScoreChanged?.Invoke(_points);
            // Kiểm tra nếu điểm mới vượt qua điểm cao nhất, cập nhật điểm cao nhất
            if (_points > _highScore)
            {
                _highScore = _points;
                OnHighScoreChanged?.Invoke(_highScore);
            }
        }
    }
    public static event Action<int> OnHighScoreChanged;
    public int GetScore()
    {
        return _points;
    }
    public int GetHighScore()
    {
        return _highScore;
    }
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
    public void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        OnHighScoreChanged?.Invoke(_highScore);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += AddScoreWhenEnemyDefeat;
        player.health.OnDeath += StopGame;
        
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= AddScoreWhenEnemyDefeat;
        player.health.OnDeath -= StopGame;

    }

    [HideInInspector] public float pointMultiplier;
    public static event Action<int> OnScoreChanged;

    private void AddScoreWhenEnemyDefeat(int score)
    {
        Points += (int)(score * pointMultiplier);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
		Time.timeScale = 1f;
		speedMultiplier = 1f;
        _enemiesCount = 0;
        gameState = GameState.Playing;
        pointMultiplier = 1f;
        _points = 0;
    }

    public void IncreaseDifficulty()
    {
        _enemiesCount++;
        if (_enemiesCount % 5 != 0) return;
        speedMultiplier *= multiplierAmount;
        OnChangeSpawnTimer?.Invoke(multiplierAmount);
    }

    private void StopGame()
    {
        gameState = GameState.Ending;
        Time.timeScale = 0f;
        //_highScore = 0;
        //OnHighScoreChanged?.Invoke(_highScore);
        //SaveHighScore();
        SceneManager.LoadScene(2);
    }
}