using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
    private State state;

    // these are the states for turn based combat
    private enum State {
        PLAYER_TURN, // allows for the player to make an action
        BUSY // does not allow for the player to make an action
    }

    private void Start() {
        // it starts off as the player's turn
        Debug.Log("It's your turn!");
        state = State.PLAYER_TURN;
    }

    private void Update() {
        // if it is the player's turn,
        if (state == State.PLAYER_TURN) {
            // if you press space,
            if (Input.GetKeyDown(KeyCode.Space)) {
                // it starts the attack
                StartCoroutine(Attack(2.0f));
            }
        }
    }

    IEnumerator Attack(float seconds) {
        // sets the state to busy so the player cant act
        state = State.BUSY;

        // this will be changed with what actually happens
        Debug.Log("You Attack!");

        // waits
        yield return new WaitForSeconds(seconds);

        // starts enemy's attack
        StartCoroutine(EnemyAttack(2.0f));

    }

    IEnumerator EnemyAttack(float seconds) {
        // this will be changed with what actually happens
        Debug.Log("The Enemy Attacks!");

        // waits
        yield return new WaitForSeconds(seconds);

        // when it's done waiting, the player can act again
        Debug.Log("It's your turn!");
        state = State.PLAYER_TURN;
    }
}
