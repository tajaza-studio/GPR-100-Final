using System.Collections;
using UnityEngine;
using static UIController;
//using UnityEditor.ShaderKeywordFilter;

public class BattleSystem : MonoBehaviour
{
    [Header("Skills UI")]
    [SerializeField] GameObject priestSkillsUI;
    [SerializeField] GameObject mcSkillsUI;
    [SerializeField] GameObject fighterSkillsUI;

    [Header("UI Controller")]
    [SerializeField] UIController uiController;

    GameObject priestSkillsInstance;
    GameObject mcSkillsInstance;
    GameObject fighterSkillsInstance;

    EnemyManager enemyManager;
    bool setUpEnemies = true;

    [HideInInspector]
    public float enemyBoundSize = 8f;

    public State state;

    bool temp = false;

    // these are the states for turn based combat
    [HideInInspector]
    public enum State {
        PRIEST_TURN,
        MC_TURN,
        FIGHTER_TURN,
        BUSY // does not allow for the player to make an action
    }

    private void Start() {
        // get enemy manager script
        enemyManager = GetComponent<EnemyManager>();

        // it starts off as the priest's turn
        Debug.Log("It's Olive's turn!");
        state = State.PRIEST_TURN;
    }

    private void Update() {
        // set up enemies
        if (setUpEnemies) {
            for (int i = 0; i < enemyManager.enemyCount; i++) {
                Vector3 enemyPos = new Vector3(-enemyBoundSize + GetEnemyDistance() * (i + 1), 2.5f, 0f);
                Instantiate(enemyManager.enemies[i], enemyPos, Quaternion.identity);
            }
            setUpEnemies = false;
        }

        // case for each of the character's turns
        switch (state) {
            case (State.PRIEST_TURN):
                if (temp) { // replace this with checking if priest dead
                    state = State.MC_TURN;
                }
                // show priest's skills
                if (priestSkillsInstance == null) {
                    priestSkillsInstance = Instantiate(priestSkillsUI, priestSkillsUI.transform.position, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (uiController.selectionState != UIController.SelectionState.SKILL && uiController.selectionState != UIController.SelectionState.BUSY) {
                        uiController.selectionState = SelectionState.BUSY;
                        Destroy(priestSkillsInstance);
                        StartCoroutine(PriestAttack(2.0f));
                    } else if (uiController.selectionState == UIController.SelectionState.SKILL) {
                        uiController.FindSkill();
                    }
                }
                break;
            case (State.MC_TURN):
                if (temp) { // replace this with checking if mc dead
                    state = State.FIGHTER_TURN;
                }
                if (mcSkillsInstance == null) {
                    mcSkillsInstance = Instantiate(mcSkillsUI, mcSkillsUI.transform.position, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (uiController.selectionState != UIController.SelectionState.SKILL && uiController.selectionState != UIController.SelectionState.BUSY) {
                        uiController.selectionState = SelectionState.BUSY;
                        Destroy(mcSkillsInstance);
                        StartCoroutine(MCAttack(2.0f));
                    } else if (uiController.selectionState == UIController.SelectionState.SKILL) {
                        Debug.Log(state);
                        uiController.FindSkill();
                    }
                }
                break;
            case (State.FIGHTER_TURN):
                if (temp) { // replace this with checking if fighter dead
                    state = State.BUSY;
                    StartCoroutine(EnemyAttack(2.0f));
                }
                if (fighterSkillsInstance == null) {
                    fighterSkillsInstance = Instantiate(fighterSkillsUI, fighterSkillsUI.transform.position, Quaternion.identity);
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (uiController.selectionState != UIController.SelectionState.SKILL && uiController.selectionState != UIController.SelectionState.BUSY) {
                        uiController.selectionState = SelectionState.BUSY;
                        Destroy(fighterSkillsInstance);
                        StartCoroutine(FighterAttack(2.0f));
                    } else if (uiController.selectionState == UIController.SelectionState.SKILL) {
                        uiController.FindSkill();
                    }
                }
                break;
        }
    }

    public float GetEnemyDistance() {
        return enemyBoundSize * 2 / (enemyManager.enemyCount + 1);
    }

    IEnumerator PriestAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;

        // this will be changed with what actually happens
        Debug.Log("Olive Attacks!");

        // waits
        yield return new WaitForSeconds(seconds);
        uiController.selectionState = SelectionState.SKILL;
        Debug.Log("It's Your turn!");
        state = State.MC_TURN;
    }
    IEnumerator MCAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;

        // this will be changed with what actually happens
        Debug.Log("You Attack!");

        // waits
        yield return new WaitForSeconds(seconds);
        uiController.selectionState = SelectionState.SKILL;
        Debug.Log("It's Dracovich's turn!");
        state = State.FIGHTER_TURN;
    }
    IEnumerator FighterAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;

        // this will be changed with what actually happens
        Debug.Log("Dracovich Attack!");

        // waits
        yield return new WaitForSeconds(seconds);
        StartCoroutine(EnemyAttack(2.0f));
    }

    IEnumerator EnemyAttack(float seconds) {
        // this will be changed with what actually happens
        Debug.Log("The Enemy Attacks!");

        // waits
        yield return new WaitForSeconds(seconds);

        // when it's done waiting, the player can act again
        Debug.Log("It's Olive's turn!");
        uiController.selectionState = SelectionState.SKILL;
        state = State.PRIEST_TURN;
    }
}
