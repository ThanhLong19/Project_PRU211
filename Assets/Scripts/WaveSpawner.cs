using System;
using UnityEngine;
using UnityEngine.Pool;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    private float _curSpawnTimer;
    private float _timer;

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
    }

    private void ChangeSpawnTimer(float multiplier)
    {
        _curSpawnTimer = Mathf.Clamp(_curSpawnTimer * multiplier, minSpawnTimer, maxSpawnTimer);
    }

    private void Update()
    {
        if (GameController.Instance.gameState != GameState.Playing) return;
        _timer -= Time.deltaTime;
        if (!(_timer <= 0)) return;
        _timer = _curSpawnTimer;
        _objectPool.Get();
    }
}