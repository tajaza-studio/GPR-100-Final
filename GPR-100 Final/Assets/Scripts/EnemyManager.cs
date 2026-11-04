using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    public int enemyCount;
    public List<GameObject> enemies;

    [HideInInspector]
    public float enemyBoundSize = 8f;

    bool setUpEnemies = true;

    void Start()
    {
        enemies = new List<GameObject>();

        // set up enemies
        SetUpEnemies();

        for (int i = 0; i < enemyCount; i++) {
            enemies.Add(enemyPrefab);
        }
    }

    public float GetEnemyDistance() {
        return enemyBoundSize * 2 / (enemyCount + 1);
    }

    public void SetUpEnemies() {
        for (int i = 0; i < enemyCount; i++) {
            Vector3 enemyPos = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), 2.5f, 0f);
            GameObject enemyInstance = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemies.Add(enemyInstance);
        }
        setUpEnemies = false;
    }

    void UpdateEnemies() {
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].transform.position = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), 2.5f, 0f);
        }
    }

    public void EnemyDies(GameObject enemy) {
        enemies.Remove(enemy);
        Destroy(enemy);
        UpdateEnemies();
    }
}
