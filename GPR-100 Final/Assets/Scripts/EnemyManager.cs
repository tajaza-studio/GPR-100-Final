using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] GameObject enemyPrefab;
    public int enemyCount;
    public List<GameObject> enemies;
    public List<GameObject> healthBars;

    [HideInInspector]
    public float enemyBoundSize = 8f;
    float healthBarOffset = -1.5f;

    void Start()
    {
        enemies = new List<GameObject>();
        healthBars = new List<GameObject>();

        // set up enemies
        SetUpEnemies();
    }

    public float GetEnemyDistance() {
        return enemyBoundSize * 2 / (enemyCount + 1);
    }

    public void SetUpEnemies() {
        for (int i = 0; i < enemyCount; i++) {

            Vector3 enemyPos = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), 2.5f, 0f);
            Vector3 healthBarPos = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), (2.5f + healthBarOffset), 0f);

            GameObject enemyInstance = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemies.Add(enemyInstance);

            GameObject healthBarInstance = Instantiate(enemyHealthBar, healthBarPos, Quaternion.identity);
            healthBars.Add(healthBarInstance);

            healthBars[i].TryGetComponent<HealthBar>(out HealthBar healthBarScript);
            healthBarScript.GetEnemyScript(enemies[i]);

            enemies[i].TryGetComponent<Enemy>(out Enemy enemyScript);
            enemyScript.GetHealthBar(healthBars[i]);
        }
    }

    void UpdateEnemies() {
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].transform.position = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), 2.5f, 0f);
            healthBars[i].transform.position = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), (2.5f + healthBarOffset), 0f);
        }
    }

    public void EnemyDies(GameObject enemy, GameObject healthBar) {
        enemyCount--;
        enemies.Remove(enemy);
        Destroy(enemy);
        healthBars.Remove(healthBar);
        Destroy(healthBar);
        Debug.Log(GetEnemyDistance());
        UpdateEnemies();
    }
}
