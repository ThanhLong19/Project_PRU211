using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.1f;
public Health health;
    private List<Item> _listItems;
    private List<float> _listItemsTimer;
    private bool canShoot = true;
    private Rigidbody2D rb;

    private int collisionCount = 0;

    private void Start()
    {
        health = GetComponent<Health>();
        health.Setup(10);
        StartCoroutine(AutoShoot());
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            if (canShoot)
            {
                Shoot();
                yield return new WaitForSeconds(fireRate);
            }
            yield return null;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().damage = 1;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            switch (item.itemData.itemDuration)
            {
                case < 0f:
                    item.OnActive(this);
                    break;
                case > 0f when _listItems.Any(i => i.itemData.itemID == item.itemData.itemID):
                    _listItems.Add(item);
                    _listItemsTimer.Add(item.itemData.itemDuration);
                    item.OnActive(this);
                    break;
            }
        }
    }

    private void OnCheckItemsList()
    {
        for (var index = 0; index < _listItemsTimer.Count; index++)
        {
            var itemDuration = _listItemsTimer[index];
            if (!(itemDuration < 0f)) continue;
            _listItems[index].OnDeactivate(this);
            _listItems.RemoveAt(index);
        }

        _listItemsTimer.RemoveAll(o => o < 0f);
    }
}
