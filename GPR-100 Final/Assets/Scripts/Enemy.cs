using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    /*[SerializeField] MCScript MC;
    [SerializeField] PriestScript Priest;
    [SerializeField] FighterScript Fighter;*/

    /*MCScript mcScript;
    PriestScript priestScript;
    FighterScript fighterScript;*/

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    void Start()
    {
        
    }

    int getRandomCharacter()
    {
        int character = Random.Range(0, 2);
        return character;
    }
    void attack(int damage, bool aoe)
    {
        int attackRandom = getRandomCharacter();

       /* if (aoe) 
        {
            if (MC.isAlive)
            {
                MC.takeDamage(damage);
            }

            if(Priest.isAlive)
            {
                Priest.takeDamage(damage);
            }
            Priest.takeDamage(damage);

            if (Fighter.isAlive)
            {
                Fighter.takeDamage(damage);
            }
            return;
        }
        
        if (attackRandom == 0)
        {
            Priest.takeDamage(damage);
        }

        if (attackRandom == 1)
        {
            MC.takeDamage(damage);
        }

        if (attackRandom == 2)
        {
            Fighter.takeDamage(damage);
        }*/

    }


    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }


}
