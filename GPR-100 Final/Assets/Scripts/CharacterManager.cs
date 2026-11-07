using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public void characterDies(GameObject character)
    {
        characters.Remove(character);
        Destroy(character);
    }

}
