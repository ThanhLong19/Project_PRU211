using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private int minSpawnEnemy;
    [SerializeField] private int maxSpawnEnemy;
    private float _curSpawnTimer;
    private float _timer;
    private int _curSpawnEnemy;
    private float _elapsedTime;


    private ObjectPool<Enemy> _objectPool;

    private void OnEnable()
    {
        GameController.Instance.OnChangeSpawnTimer += ChangeSpawnTimer;
    }

    private void OnDisable()
    {
        GameController.Instance.OnChangeSpawnTimer -= ChangeSpawnTimer;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _objectPool = new ObjectPool<Enemy>(() =>
            {
                var instance = Instantiate(enemyPrefab, transform);
                instance.ReturnToPool = _objectPool.Release;
                return instance;
            },
            obj => obj.gameObject.SetActive(true),
            obj => obj.gameObject.SetActive(false),
            Destroy,
            false,
            50,
            200);
        _timer = _curSpawnTimer = maxSpawnTimer;
        _curSpawnEnemy = minSpawnEnemy;
        _elapsedTime = 0;
    }

    private void ChangeSpawnTimer(float multiplier)
    {
        _curSpawnTimer = Mathf.Clamp(_curSpawnTimer * multiplier, minSpawnTimer, maxSpawnTimer);
    }

    private void IncreaseEnemyAmountSpawn()
    {
        _curSpawnEnemy = Mathf.Clamp(++_curSpawnEnemy, minSpawnEnemy, maxSpawnEnemy);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Mathf.Ceil(_elapsedTime) % 10 == 0)
        {
            IncreaseEnemyAmountSpawn();
            _elapsedTime = 0f;
        }

        if (GameController.Instance.gameState != GameState.Playing) return;
        _timer -= Time.deltaTime;
        if (!(_timer <= 0)) return;
        _timer = _curSpawnTimer;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for (var i = 0; i < _curSpawnEnemy; i++)
        {
            _objectPool.Get();
            yield return new WaitForSeconds(0.2f);
        }
    }
}