using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damage;

    private Rigidbody2D rb;

    public AudioSource bulletSoundEffect;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
        bulletSoundEffect.Play();
    }

}
