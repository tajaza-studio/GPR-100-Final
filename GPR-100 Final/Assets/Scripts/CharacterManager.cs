using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject MC;
    [SerializeField] GameObject Priest;
    [SerializeField] GameObject Fighter;

    public int characterCount;
    public List<GameObject> characters;

    void Start()
    {
        //Sets up list, priest must be added first for index 0 and so on
        characters = new List<GameObject>(3);
        characters.Add(Priest);
        characters.Add(MC);
        characters.Add(Fighter);
    }

    public void EveryoneDeadCheck()
    {
        for (int i = 0; i < characters.Count; i++) {
            Character characterScript = characters[i].GetComponent<Character>();
            if (characterScript.isAlive) {
                Debug.Log("found someone alive!");
                return;
            }
        }
        // load lose screen
        SceneManager.LoadScene("Lose");
    }

}
