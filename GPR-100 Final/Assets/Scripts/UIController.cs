//using UnityEditor.Experimental.GraphView;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;

    [SerializeField] EnemyManager enemyManager;

    private SpriteRenderer spriteRenderer;

    public int skillHovered = 0;

    [SerializeField] GameObject enemyPointer;
    public int enemyHovered = 0;
    bool enemyPointerSpawned = false;
    float enemyPointerY = 1.5f;
    GameObject enemyPointerInstance = null;
    List<GameObject> allEnemyPointerInstance = new List<GameObject>();

    [SerializeField] GameObject allyPointer;
    public int allyHovered = 0;
    bool allyPointerSpawned = false;
    float allyPointerX = -1.5f;
    float allyPointerY = -1f;
    float allyPointerYSpacing = 1.5f;
    GameObject allyPointerInstance = null;
    List<GameObject> allAllyPointerInstance = new List<GameObject>();

    public SelectionState selectionState;

    public enum SelectionState {
        SKILL,
        ENEMY,
        ALL_ENEMY,
        ALLY,
        ALL_ALLY,
        PRIEST,
        MC,
        FIGHTER,
        BUSY
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        selectionState = SelectionState.SKILL;
    }

    void Update()
    {
        switch (skillHovered) {
            case (0):
                transform.position = new Vector3(3f, -1f, 0f);
                break;
            case (1):
                transform.position = new Vector3(3f, -2.5f, 0f);
                break;
            case (2):
                transform.position = new Vector3(3f, -4f, 0f);
                break;
        }
        if (battleSystem.state == BattleSystem.State.BUSY) {
            skillHovered = 0;
            spriteRenderer.enabled = false;
            return;
        }
        spriteRenderer.enabled = true;

        switch (selectionState) {
            case (SelectionState.SKILL):
                if (Input.GetKeyDown(KeyCode.W)) {
                    skillHovered = (skillHovered + 2) % 3;
                }
                if (Input.GetKeyDown(KeyCode.S)) {
                    skillHovered = (skillHovered + 1) % 3;
                }
                break;
            case (SelectionState.ENEMY):
                if (!enemyPointerSpawned) {
                    Vector3 enemyPointerPos = new Vector3(-battleSystem.enemyBoundSize + battleSystem.GetEnemyDistance() * (enemyHovered + 1), enemyPointerY, 0f);
                    enemyPointerInstance = Instantiate(enemyPointer, enemyPointerPos, Quaternion.identity);
                    enemyPointerSpawned = true;
                }
                if (Input.GetKeyDown(KeyCode.A)) {
                    enemyHovered = (enemyHovered + enemyManager.enemyCount - 1) % enemyManager.enemyCount;
                    Vector3 enemyPointerPos = new Vector3(-battleSystem.enemyBoundSize + battleSystem.GetEnemyDistance() * (enemyHovered + 1), enemyPointerY, 0f);
                    enemyPointerInstance.transform.position = enemyPointerPos;
                }
                if (Input.GetKeyDown(KeyCode.D)) {
                    enemyHovered = (enemyHovered + 1) % enemyManager.enemyCount;
                    Vector3 enemyPointerPos = new Vector3(-battleSystem.enemyBoundSize + battleSystem.GetEnemyDistance() * (enemyHovered + 1), enemyPointerY, 0f);
                    enemyPointerInstance.transform.position = enemyPointerPos;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    enemyHovered = 0;
                    enemyPointerSpawned = false;
                    Destroy(enemyPointerInstance);
                }
                if (Input.GetKeyDown(KeyCode.C)) {
                    selectionState = SelectionState.SKILL;
                    enemyHovered = 0;
                    enemyPointerSpawned = false;
                    Destroy(enemyPointerInstance);
                }

                break;
            case (SelectionState.ALL_ENEMY):
                if (!enemyPointerSpawned) {
                    for (int i = 0; i < enemyManager.enemyCount; i++) {
                        Vector3 enemyPointerPos = new Vector3(-battleSystem.enemyBoundSize + battleSystem.GetEnemyDistance() * (i + 1), enemyPointerY, 0f);
                        allEnemyPointerInstance.Add(Instantiate(enemyPointer, enemyPointerPos, Quaternion.identity));
                    }
                    enemyPointerSpawned = true;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    enemyPointerSpawned = false;
                    for (int i = 0; i < enemyManager.enemyCount; i++) {
                        Destroy(allEnemyPointerInstance[0]);
                        allEnemyPointerInstance.RemoveAt(0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.C)) {
                    selectionState = SelectionState.SKILL;
                    enemyPointerSpawned = false;
                    for (int i = 0; i < enemyManager.enemyCount; i++) {
                        Destroy(allEnemyPointerInstance[0]);
                        allEnemyPointerInstance.RemoveAt(0);
                    }
                }
                break;
            case (SelectionState.ALLY):
                if (!allyPointerSpawned) {
                    Vector3 allyPointerPos = new Vector3(allyPointerX, allyPointerY - (allyPointerYSpacing * allyHovered), 0f);
                    allyPointerInstance = Instantiate(allyPointer, allyPointerPos, Quaternion.identity);
                    allyPointerSpawned = true;
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                    allyHovered = (allyHovered + 2) % 3;
                    Vector3 allyPointerPos = new Vector3(allyPointerX, allyPointerY - (allyPointerYSpacing * allyHovered), 0f);
                    allyPointerInstance.transform.position = allyPointerPos;
                }
                if (Input.GetKeyDown(KeyCode.S)) {
                    allyHovered = (allyHovered + 1) % 3;
                    Vector3 allyPointerPos = new Vector3(allyPointerX, allyPointerY - (allyPointerYSpacing * allyHovered), 0f);
                    allyPointerInstance.transform.position = allyPointerPos;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    allyHovered = 0;
                    allyPointerSpawned = false;
                    Destroy(allyPointerInstance);
                }
                if (Input.GetKeyDown(KeyCode.C)) {
                    selectionState = SelectionState.SKILL;
                    allyHovered = 0;
                    allyPointerSpawned = false;
                    Destroy(allyPointerInstance);
                }

                break;
            case (SelectionState.ALL_ALLY):
                if (!allyPointerSpawned) {
                    for (int i = 0; i < 3; i++) {
                        Vector3 allyPointerPos = new Vector3(allyPointerX, allyPointerY - (allyPointerYSpacing * i), 0f);
                        allAllyPointerInstance.Add(Instantiate(allyPointer, allyPointerPos, Quaternion.identity));
                    }
                    allyPointerSpawned = true;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    allyPointerSpawned = false;
                    for (int i = 0; i < 3; i++) {
                        Destroy(allAllyPointerInstance[0]);
                        allAllyPointerInstance.RemoveAt(0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.C)) {
                    selectionState = SelectionState.SKILL;
                    allyPointerSpawned = false;
                    for (int i = 0; i < 3; i++) {
                        Destroy(allAllyPointerInstance[0]);
                        allAllyPointerInstance.RemoveAt(0);
                    }
                }
                break;
            case (SelectionState.PRIEST):
                break;
            case (SelectionState.MC):
                break;
            case (SelectionState.FIGHTER):
                allyHovered = 2;
                if (!allyPointerSpawned) {
                    Vector3 allyPointerPos = new Vector3(allyPointerX, allyPointerY - (allyPointerYSpacing * allyHovered), 0f);
                    allyPointerInstance = (Instantiate(allyPointer, allyPointerPos, Quaternion.identity));
                    allyPointerSpawned = true;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    allyHovered = 0;
                    allyPointerSpawned = false;
                    Destroy(allyPointerInstance);
                }
                if (Input.GetKeyDown(KeyCode.C)) {
                    selectionState = SelectionState.SKILL;
                    allyHovered = 0;
                    allyPointerSpawned = false;
                    Destroy(allyPointerInstance);
                }
                break;
        }
    }

    public void FindSkill() {
        if (battleSystem.state == BattleSystem.State.PRIEST_TURN) {
            switch (skillHovered) {
                case (0):
                    selectionState = SelectionState.ALLY;
                    break;
                case (1):
                    selectionState = SelectionState.ALL_ALLY;
                    break;
                case (2):
                    selectionState = SelectionState.ENEMY;
                    break;
            }
        }
        if (battleSystem.state == BattleSystem.State.MC_TURN) {
            switch (skillHovered) {
                case (0):
                    selectionState = SelectionState.ALLY;
                    break;
                case (1):
                    selectionState = SelectionState.ENEMY;
                    break;
                case (2):
                    selectionState = SelectionState.ENEMY;
                    break;
            }
        }
        if (battleSystem.state == BattleSystem.State.FIGHTER_TURN) {
            switch (skillHovered) {
                case (0):
                    selectionState = SelectionState.ENEMY;
                    break;
                case (1):
                    selectionState = SelectionState.ALL_ENEMY;
                    break;
                case (2):
                    selectionState = SelectionState.FIGHTER;
                    break;
            }
        }
    }
}
