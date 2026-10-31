using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject Enemy;

    Enemy enemyScript;

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    void Start()
    {
        enemyScript = Enemy.GetComponent<Enemy>();
        Debug.Log("The Fight is Starting!");
    }

    

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

    
    

    //Priest Skills
     void priestHeal()
     {

     }

     void priestBlessing()
     {

     }

     void priestBonk()
     {
        Debug.Log("The Enemy Was Bonked by the Priest!");
        enemyScript.takeDamage(damage);
    }


     //MC Skills
     void mcEnhance()
     {

     }

     void mcSunder()
     {

     }

     void mcSlash()
     {
        Debug.Log("The Enemy Was Slashed by the MC!");
        enemyScript.takeDamage(damage);
    }


//Fighter Skills
     void fighterChop()
     {
        Debug.Log("The Enemy Was Chopped by the Fighter!");
        enemyScript.takeDamage(damage);
     }

     void fighterCleave()
     {

     }

     void fighterBreathe()
     {

     }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fighterChop();
        }
    }


}
