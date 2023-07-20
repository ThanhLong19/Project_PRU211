using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private string pathFileHighScore = @"../highscore.txt";
    public event Action<float> OnChangeSpawnTimer;
    public int HighScore { get; set; } = 0;

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
        var highScores = new List<int>();
        var rsHighScore = ReadFromFile(pathFileHighScore);
        if (string.IsNullOrEmpty(rsHighScore))
        {
            WriteToFile(_highScore.ToString(), pathFileHighScore);
        }
        else
        {
            var arrEnemy = rsHighScore.Trim().Split("\n");
            foreach (var item in arrEnemy)
            {
                highScores.Add(int.Parse(item));
            }
            highScores.Add(_highScore);
            highScores = highScores.OrderByDescending(x => x).ToList();
            WriteToFile(string.Join("\n", highScores), pathFileHighScore);
        }
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
    public void LoadHighScore()
    {
        var highScores = new List<int>();
        var rsHighScore = ReadFromFile(pathFileHighScore);
        if (string.IsNullOrEmpty(rsHighScore))
        {
            HighScore = _highScore;
        }
        else
        {
            var arrEnemy = rsHighScore.Trim().Split("\n");
            foreach (var item in arrEnemy)
            {
                highScores.Add(int.Parse(item));
            }
            highScores.Add(_highScore);
            highScores = highScores.OrderByDescending(x => x).ToList();
            HighScore = highScores.First();
        }
         
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        OnHighScoreChanged?.Invoke(_highScore);
    }
    public void WriteToFile(string content, string path)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(content);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }
    public string ReadFromFile(string path)
    {
        string rs = string.Empty;
        try
        {
            rs = string.Empty;
            if (File.Exists(path))
            {
                rs = File.ReadAllText(path);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return rs;
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
        SaveHighScore();
        LoadHighScore();
        gameState = GameState.Ending;
        Time.timeScale = 0f;
        //_highScore = 0;
        //OnHighScoreChanged?.Invoke(_highScore);
        //SaveHighScore();
        SceneManager.LoadScene(1);
    }
}