using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Enemy enemy;
    [SerializeField] Health health;

    public int maxValue;
    public int value;


    public void SetUpHealth()
    {
        maxValue = enemy.maxHealth;
        value = enemy.currentHealth;
    }

    public void UpdateHealth(int hp)
    {
        value = hp;
        health.UpdateHealthVisual(value, maxValue);
    }

    public void GetEnemyScript(GameObject enemyObject)
    {
        enemy = enemyObject.GetComponent<Enemy>();
        SetUpHealth();
    }
}
