using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject MC;
    [SerializeField] GameObject Priest;
    [SerializeField] GameObject Fighter;

    Character mcScript;
    Character priestScript;
    Character fighterScript;

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    void Start()
    {
        mcScript = MC.GetComponent<Character>();
        priestScript = Priest.GetComponent<Character>();
        fighterScript = Fighter.GetComponent<Character>();
    }

    int getRandomCharacter()
    {
        int character = Random.Range(0, 2);
        return character;
    }
    void attack(int damage, bool aoe)
    {
        int attackRandom = getRandomCharacter();
        bool attacking = true;
        if (aoe) 
        {
            if (mcScript.isAlive)
            {
                mcScript.takeDamage(damage);
            }

            if(priestScript.isAlive)
            {
                priestScript.takeDamage(damage);
            }

            if (fighterScript.isAlive)
            {
                fighterScript.takeDamage(damage);
            }
            return;
        }
        while (attacking)
        {


            switch (attackRandom % 3)
            {
                case (0):
                    if (priestScript.isAlive)
                    {
                        priestScript.takeDamage(damage);
                        attacking = false;
                    }
                    break;
                case (1):
                    if (mcScript.isAlive)
                    { 
                        mcScript.takeDamage(damage);
                        attacking = false;
                    }
                    break;
                case (2):
                    if (fighterScript.isAlive)
                    {
                        fighterScript.takeDamage(damage);
                        attacking = false;
                    }
                    break;

            }
            attackRandom++;

         
        }
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
