using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float damageInvincLength = 1f;
    private float invincCount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if(invincCount <= 0)
            {
                PlayerController.instance.spriteRenderer.color = new Color(PlayerController.instance.spriteRenderer.color.r,
                    PlayerController.instance.spriteRenderer.color.g,
                    PlayerController.instance.spriteRenderer.color.b,
                    1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            currentHealth--;

            AudioManager.instance.PlaySFX(11);

            invincCount = damageInvincLength;

            PlayerController.instance.spriteRenderer.color = new Color(PlayerController.instance.spriteRenderer.color.r,
                PlayerController.instance.spriteRenderer.color.g,
                PlayerController.instance.spriteRenderer.color.b,
                0.5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);

                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(8);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
