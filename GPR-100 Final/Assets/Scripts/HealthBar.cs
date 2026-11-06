using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Enemy enemyHealth;

    private void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = enemyHealth.maxHealth;
        healthBar.value = enemyHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
