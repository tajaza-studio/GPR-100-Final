using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Character : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CharacterManager characterManager;

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

    public int healHealth(int healAmount)
    {
        int heal = healAmount + 2;
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
        damage -= 2;
    }
    

    //Priest Skills
     public void priestHeal(int characterIndex)
     {
        if (characterIndex == 0)
        {
            int healthIncrease = healHealth(damage);
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character priestScript);
            Debug.Log($"The Priest's health was increased by {healthIncrease} !");
        }

        if (characterIndex == 1)
        {
            int healthIncrease = healHealth(damage);
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character mcScript);
            Debug.Log($"The MC's health was increased by {healthIncrease} !");
        }

        if (characterIndex == 2)
        {
            int healthIncrease = healHealth(damage);
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character fighterScript);
            Debug.Log($"The Fighter's health was increased by {healthIncrease} !");
        }
     }

     public void priestBlessing()
     {

        for (int i = 0; i < characterManager.characters.Count; i++)
        {
            if (i == 0)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character priestScript);
                priestScript.healHealth(damage);
            }

            if (i == 1)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character mcScript);
                mcScript.healHealth(damage);
            }

            if (i == 2)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character fighterScript);
                fighterScript.healHealth(damage);
            }
        }
        Debug.Log($"All of your allies health were healed by the Priest!");
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

     public void fighterCleave()
     {

        for (int i = 0; i < enemyManager.enemies.Count; i++)
        {
            enemyManager.enemies[i].TryGetComponent<Enemy>(out Enemy enemyScript);
            enemyScript.takeDamage(damage);
        }
        Debug.Log("All Enemies were Cleaved by the Fighter!");
     }

     public void fighterBreathe(int healAmount)
     {
        characterManager.characters[2].TryGetComponent<Character>(out Character fighterScript);
        int healthIncrease = fighterScript.healHealth(healAmount);
        Debug.Log($"Fighter healed their health by {healthIncrease} !");
     }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            priestHeal(0);
        }
    }*/


}
