using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // for enemy health bars
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Enemy enemy;

    // for ally health bars
    [SerializeField] Character character;

    [SerializeField] Health health;

    public int maxHealthValue;
    public int currentHealthValue;

    public void UpdateHealth(int hp)
    {
        currentHealthValue = hp;
        health.UpdateHealthVisual(currentHealthValue, maxHealthValue);
    }

    public void GetEnemyScript(GameObject enemyObject)
    {
        enemy = enemyObject.GetComponent<Enemy>();
        maxHealthValue = enemy.maxHealth;
        currentHealthValue = enemy.currentHealth;
    }

    public void GetAllyScript(GameObject enemyObject)
    {
        character = enemyObject.GetComponent<Character>();
        maxHealthValue = character.maxHealth;
        currentHealthValue = character.currentHealth;
    }
}
