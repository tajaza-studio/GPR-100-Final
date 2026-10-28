using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    public int enemyCount;
    public List<GameObject> enemies;

    void Start()
    {
        enemies = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++) {
            enemies.Add(enemyPrefab);
        }
    }
}
