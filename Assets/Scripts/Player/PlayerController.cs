using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.1f;

    private bool canShoot = true;
    private Rigidbody2D rb;

    private int collisionCount = 0;

    private void Start()
    {
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
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collisionCount++;
            //if(collisionCount >= 3)
            //{
                PlayerDie();
            //}
        }
    }

    private void PlayerDie()
    {
        Debug.Log("Player collided with enemy");
    }

}
