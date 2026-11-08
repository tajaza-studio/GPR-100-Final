using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Character : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] GameObject healthBar;

    [SerializeField] GameObject deathMarker;
    HealthBar healthBarScript;

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    void Start()
    {
        Debug.Log("The Fight is Starting!"); 
        SetupHealthBar();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarScript.UpdateHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            Instantiate(deathMarker, transform.position, Quaternion.identity);
            characterManager.EveryoneDeadCheck();
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
        healthBarScript.UpdateHealth(currentHealth);
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

    public void SetupHealthBar() {
        Vector3 offset = new Vector3(3f, 0f, 0f);
        GameObject healthBarObject = Instantiate(healthBar, transform.position + offset, Quaternion.identity);
        healthBarScript = healthBarObject.GetComponent<HealthBar>();
        healthBarScript.GetAllyScript(gameObject);
        healthBarScript.UpdateHealth(currentHealth);
    }
    
    //
    // SKILLS
    //

    //Priest Skills
     public void priestHeal(int characterIndex)
     {
        if (characterIndex == 0)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character priestScript);
            int healthIncrease = priestScript.healHealth(damage);
            Debug.Log($"The Priest's health was increased by {healthIncrease} !");
        }

        if (characterIndex == 1)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character mcScript);
            int healthIncrease = mcScript.healHealth(damage);
            Debug.Log($"The MC's health was increased by {healthIncrease} !");
        }

        if (characterIndex == 2)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character fighterScript);
            int healthIncrease = fighterScript.healHealth(damage);
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
                if (priestScript.isAlive) {
                    priestScript.healHealth(damage);
                }
            }

            if (i == 1)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character mcScript);
                if (mcScript.isAlive) {
                    mcScript.healHealth(damage);
                }
            }

            if (i == 2)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character fighterScript);
                if (fighterScript.isAlive) {
                    fighterScript.healHealth(damage);
                }
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
     public void mcEnhance(int characterIndex)
     {
        if (characterIndex == 0)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character priestScript);
            priestScript.buffDamage();
            Debug.Log("MC buffed the Priest's damage!");
        }

        if (characterIndex == 1)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character mcScript);
            mcScript.buffDamage();
            Debug.Log("MC buffed their own damage!");
        }

        if (characterIndex == 2)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character fighterScript);
            fighterScript.buffDamage();
            Debug.Log("MC buffed the Fighter's damage!");
        }
     }

     public void mcSunder(int enemyIndex)
     {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        enemyScript.debuffDamage();
        Debug.Log("MC debuffed the enemies damage!");
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

        for (int i = enemyManager.enemies.Count - 1; i >= 0; i--)
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
