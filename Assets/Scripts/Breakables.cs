using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Breakables : MonoBehaviour
{
    public GameObject destroyEffect;
    public int shotsToDestroy;
    private int currentShots = 0;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    void Start()
    {

    }

    void Update()
    {
        if (currentShots == shotsToDestroy)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(0);

            Instantiate(destroyEffect, transform.position, transform.rotation);

            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);
                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            currentShots++;
        }
    }
}
