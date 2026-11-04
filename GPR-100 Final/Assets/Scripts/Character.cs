using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Character : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    void Start()
    {
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

    public int healHealth(int damage)
    {
        int heal = damage + 2;
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        return heal;
    }

    public void buffDamage()
    {
        damage += 3;
    }
    
    public void debuffDamage()
    {

    }
    

    //Priest Skills
     void priestHeal()
     {
        //int healthIncrease = healHealth(damage);
        //Character.healHealth(damage);
        //Debug.Log($"Your ally's health was increased by {healthIncrease} !");
     }

     void priestBlessing()
     {
        //int healthIncrease = healHealth(damage);
        //Character.healHealth(damage);
        //Debug.Log($"All of your allies health were increased by {healthIncrease} !");
     }

    public void priestBonk(int enemyIndex)
     {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        Debug.Log("The Enemy Was Bonked by the Priest!");
        enemyScript.takeDamage(damage);
     }


     //MC Skills
     void mcEnhance()
     {
        //Character.buffDamage();
        //Debug.Log("MC buffed your allies damage!");
     }

     void mcSunder()
     {

     }

     public void mcSlash(int enemyIndex)
     {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        Debug.Log("The Enemy Was Slashed by the MC!");
        enemyScript.takeDamage(damage);
     }


    //Fighter Skills
    public void fighterChop(int enemyIndex)
    {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        Debug.Log("The Enemy Was Chopped by the Fighter!");
        enemyScript.takeDamage(damage);
    }

     void fighterCleave()
     {
        for (int i = 0; i < enemyManager.enemies.Count; i++)
        {
            enemyManager.enemies[i].TryGetComponent<Enemy>(out Enemy enemyScript);
            enemyScript.takeDamage(damage);
        }
        Debug.Log("All Enemies were Cleaved by the Fighter!");
     }

     void fighterBreathe()
     {
        //int healthIncrease = healHealth(damage);
        //Character.healHealth(damage);
        //Debug.Log($"Fighter healed their health by {healthIncrease} !");
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    fighterChop();
        //}
    }


}
