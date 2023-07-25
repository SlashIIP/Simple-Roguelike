using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 7.5f;
    public GameObject impactEffect;
    private Rigidbody2D bulletRigidbody;

    public int damageToGive = 50;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        bulletRigidbody.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(4);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
