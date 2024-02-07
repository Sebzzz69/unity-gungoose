using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    private void Die()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            gameManager.LoseGame();
        }
        else if (this.gameManager.CompareTag("Enemy"))
        {
            gameManager.WinGame();
        }

        Destroy(gameObject);
    }
}
