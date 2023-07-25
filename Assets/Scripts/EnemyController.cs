using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enemyRigidbody;
    public float moveSpeed;

    private SpriteRenderer enemySprite;

    [Header("Преследовать игрока")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Убегать от игрока")]
    public bool shouldRunAway;
    public float runawayRange;

    [Header("Блуждать по локации")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    [Header("Патрулировать локацию")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("Стрельба")]
    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float shootRange; 
    public float fireRate;
    private float fireCounter;

    public int health = 150;

    public GameObject deathEffect;

    void Start()
    {
        enemySprite = GetComponentInChildren<SpriteRenderer>();

        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        }
    }

    void Update()
    {
        if (enemySprite.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            EnemyBehavior();
            UpdateSprite();
            EnemyShooting();
        }
        else
        {
            enemyRigidbody.velocity = Vector2.zero;
        }
    }

    public void EnemyShooting()
    {
        if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;

            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(13);
            }
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        AudioManager.instance.PlaySFX(2);

        if (health <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);

            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    void EnemyBehavior()
    {
        moveDirection = Vector3.zero;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            if (shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;

                    moveDirection = wanderDirection;

                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                    }
                }

                if(pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;

                    if(pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);

                        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    }
                }
            }

            if (shouldPatrol)
            {
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.2f)
                {
                    currentPatrolPoint++;
                    if(currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                }
            }
        }

        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange && shouldRunAway)
        {
            moveDirection = transform.position - PlayerController.instance.transform.position;
        }

        moveDirection.Normalize();

        enemyRigidbody.velocity = moveDirection * moveSpeed;
    }

    void UpdateSprite()
    {
        if(enemyRigidbody.velocity.x < 0)
        {
            enemySprite.flipX = true;
        }
        else
            enemySprite.flipX = false;
    }
}
