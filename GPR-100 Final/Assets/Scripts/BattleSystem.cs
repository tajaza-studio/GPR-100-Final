using UnityEngine;
using System.Collections;
using UnityEditor.ShaderKeywordFilter;

public class BattleSystem : MonoBehaviour
{
    private State state;

    bool temp = false;

    // these are the states for turn based combat
    private enum State {
        PRIEST_TURN,
        MC_TURN,
        FIGHTER_TURN,
        BUSY // does not allow for the player to make an action
    }

    private void Start() {
        // it starts off as the player's turn
        Debug.Log("It's Olive's turn!");
        state = State.PRIEST_TURN;
    }

    private void Update() {
        // case for each of the character's turns
        switch (state) {
            case (State.PRIEST_TURN):
                if (temp) { // replace this with checking if priest dead
                    state = State.MC_TURN;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartCoroutine(PriestAttack(2.0f));
                }
                break;
            case (State.MC_TURN):
                if (temp) { // replace this with checking if mc dead
                    state = State.FIGHTER_TURN;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartCoroutine(MCAttack(2.0f));
                }
                break;
            case (State.FIGHTER_TURN):
                if (temp) { // replace this with checking if fighter dead
                    state = State.BUSY;
                    StartCoroutine(EnemyAttack(2.0f));
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartCoroutine(FighterAttack(2.0f));
                }
                break;
        }
    }

    IEnumerator PriestAttack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;

        // this will be changed with what actually happens
        Debug.Log("Olive Attacks!");

        // waits
        yield return new WaitForSeconds(seconds);
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
        state = State.PRIEST_TURN;
    }
}
