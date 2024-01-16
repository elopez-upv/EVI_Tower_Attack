using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    [Header("Health UI")]
    [SerializeField] private Image healthBar;

    private float health;
    private bool isDestroyed = false;

    public void Start() {
        health = (float)hitPoints;
    }

    public void TakeDamage(int damage) {
        hitPoints -= damage;

        healthBar.fillAmount = (float)hitPoints/health;
        AudioManager.main.PlayExplosionEffect();
        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
