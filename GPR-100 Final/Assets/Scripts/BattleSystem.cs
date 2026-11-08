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

    [Header("Characters")]
    [SerializeField] Character priest;
    [SerializeField] Character mc;
    [SerializeField] Character fighter;

    GameObject priestSkillsInstance;
    GameObject mcSkillsInstance;
    GameObject fighterSkillsInstance;

    EnemyManager enemyManager;
    bool setUpEnemies = true;

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

        // tolsta said to refactor this into https://gameprogrammingpatterns.com/state.html
        // case for each of the character's turns
        uiController.UpdateGamePointers();

        switch (state) {
            case (State.PRIEST_TURN):
                if (!priest.isAlive) { // skips if the priest is dead
                    state = State.MC_TURN;
                    break;
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
                        uiController.FindSkill((int)state);
                    }
                }
                break;
            case (State.MC_TURN):
                if (!mc.isAlive) { // skips if the mc is dead
                    state = State.FIGHTER_TURN;
                    break;
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
                        uiController.FindSkill((int)state);
                    }
                }
                break;
            case (State.FIGHTER_TURN):
                if (!fighter.isAlive) { // skips if the fighter is dead
                    state = State.BUSY;
                    StartCoroutine(EnemyAttack(2.0f));
                    break;
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
                        uiController.FindSkill((int)state);
                    }
                }
                break;
        }
    }

    IEnumerator PriestAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;
        uiController.SetHiddenStatus(false);

        // this will be changed with what actually happens
        switch (uiController.skillHovered) {
            case 0:
                priest.priestHeal(uiController.allyHovered);
                break;
            case 1:
                priest.priestBlessing();
                break;
            case (2):
                priest.priestBonk(uiController.enemyHovered);
                break;
        }

        // waits
        uiController.ResetGamePointers();
        yield return new WaitForSeconds(seconds);
        uiController.SetHiddenStatus(true);
        uiController.selectionState = SelectionState.SKILL;
        Debug.Log("It's Your turn!");
        state = State.MC_TURN;
    }
    IEnumerator MCAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;
        uiController.SetHiddenStatus(false);

        switch (uiController.skillHovered) {
            case (0):
                Debug.Log(uiController.allyHovered);
                mc.mcEnhance(uiController.allyHovered);
                break;
            case (1):
                mc.mcSunder(uiController.enemyHovered);
                break;
            case (2):
                mc.mcSlash(uiController.enemyHovered);
                break;
        }

        // waits
        uiController.ResetGamePointers();
        yield return new WaitForSeconds(seconds);
        uiController.SetHiddenStatus(true);
        uiController.selectionState = SelectionState.SKILL;
        Debug.Log("It's Dracovich's turn!");
        state = State.FIGHTER_TURN;
    }
    IEnumerator FighterAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;
        uiController.SetHiddenStatus(false);

        switch (uiController.skillHovered) {
            case (0):
                fighter.fighterChop(uiController.enemyHovered);
                break;
            case (1):
                fighter.fighterCleave();
                break;
            case (2):
                fighter.fighterBreathe(uiController.allyHovered);
                break;
        }

        // waits
        uiController.ResetGamePointers();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(EnemyAttack(2.0f));
    }

    IEnumerator EnemyAttack(float seconds) {
        // this will be changed with what actually happens
        bool aoe = false;
        for (int i = 0; i < enemyManager.enemies.Count; i++)
        {
            enemyManager.enemies[i].TryGetComponent<Enemy>(out Enemy enemyScript);
            enemyScript.attackCharacter();
            Debug.Log("The Enemy Attacked and did 4 damage!");
        }
        Debug.Log("The Enemy Attacks!");

        // waits
        yield return new WaitForSeconds(seconds);

        // when it's done waiting, the player can act again

        Debug.Log("It's Olive's turn!");
        uiController.SetHiddenStatus(true);
        uiController.selectionState = SelectionState.SKILL;
        state = State.PRIEST_TURN;
    }
}
