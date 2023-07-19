using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ScriptableObject;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject data;
    [SerializeField] public List<Item> listItemsCanDrop;

    public delegate void OnDisableCallback(Enemy instance);

    public OnDisableCallback ReturnToPool;
    public static event Action<int> OnEnemyDefeated;
    public Health health;

    private void OnEnable()
    {
        health.Setup(data.health);
        health.OnDeath += ReleaseWhenDied;
        var gameObjectTransform = gameObject.transform;
        gameObjectTransform.position = new Vector3(
            UnityEngine.Random.Range(CameraBounds.GetXMin(Camera.main), CameraBounds.GetXMax(Camera.main)),
            CameraBounds.GetYMax(Camera.main) + 1f, 0);
        transform.DOMoveY(-5.5f, data.speed * GameController.Instance.speedMultiplier).SetSpeedBased(true).OnComplete(
            () =>
            {
                ReturnToPool?.Invoke(this);
            }).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.DOKill();
        health.OnDeath -= ReleaseWhenDied;
    }

    // Start is called before the first frame update
    private void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void ReleaseWhenDied()
    {
        var chanceToDrop = UnityEngine.Random.Range(0f, 1f);
        Item itemToSelect = null;
        var lastItemDropChance = 0f;
        foreach (var item in listItemsCanDrop.Where(item =>
                     chanceToDrop <= item.itemData.itemDropChance && chanceToDrop >= lastItemDropChance))
        {
            itemToSelect = item;
            lastItemDropChance = item.itemData.itemDropChance;
        }

        if (itemToSelect != null)
        {
            Instantiate(itemToSelect.gameObject, transform.position, Quaternion.identity);
        }

        ReturnToPool?.Invoke(this);
        GameController.Instance.IncreaseDifficulty();
        OnEnemyDefeated?.Invoke(data.pointWhenDeath);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.GetComponent<Health>().ChangeHealth(data.damage, -1);
                ReturnToPool?.Invoke(this);
                break;
            case "Bullet":
                health.ChangeHealth(other.GetComponent<BulletController>().damage, -1);
                break;
        }
    }
}