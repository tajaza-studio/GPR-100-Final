using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Character : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] GameObject healthBar;

    [SerializeField] GameObject deathMarker;

    [SerializeField] GameObject buffMarker;
    HealthBar healthBarScript;

    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;
    public bool buffed = false;
    public bool debuffed = false;

    [SerializeField] AudioClip priestBonkSound;

    private AudioSource audioSource;

    void Start()
    {
        Debug.Log("The Fight is Starting!"); 
        SetupHealthBar();

        if (gameObject.name == "Priest") {
            audioSource = GetComponent<AudioSource>();
        }
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
        int heal = healAmount;
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBarScript.UpdateHealth(currentHealth);
        return heal;
    }

   /* public void buffDamage()
    {
        damage += 3;
    }
    
    public void debuffDamage()
    {
        damage -= 3;
    } */

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
        int attackDamage = damage;
        if (buffed)
        {
            attackDamage = damage + 3;
        }
        if (debuffed)
        {
            attackDamage = damage - 3;
        }
        int healAmount = (int)(attackDamage * 1.3);
        if (characterIndex == 0)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character priestScript);
            priestScript.healHealth(healAmount);
            Debug.Log($"The Priest's health was increased by {healAmount} !");
        }

        if (characterIndex == 1)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character mcScript);
            mcScript.healHealth(healAmount);
            Debug.Log($"The MC's health was increased by {healAmount} !");
        }

        if (characterIndex == 2)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character fighterScript);
            fighterScript.healHealth(healAmount);
            Debug.Log($"The Fighter's health was increased by {healAmount} !");
        }
        debuffed = false;
        buffed = false;
    }

     public void priestBlessing()
     {
        int attackDamage = damage;
        if (buffed)
        {
            attackDamage = damage + 3;
        }
        if (debuffed)
        {
            attackDamage = damage - 3;
        } 

        int healAmount = (int)(attackDamage * 0.6);
        for (int i = 0; i < characterManager.characters.Count; i++)
        {
            if (i == 0)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character priestScript);
                if (priestScript.isAlive) {
                    priestScript.healHealth(healAmount);
                }
            }

            if (i == 1)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character mcScript);
                if (mcScript.isAlive) {
                    mcScript.healHealth(healAmount);
                }
            }

            if (i == 2)
            {
                characterManager.characters[i].TryGetComponent<Character>(out Character fighterScript);
                if (fighterScript.isAlive) {
                    fighterScript.healHealth(healAmount);
                }
            }
        }
        debuffed = false;
        buffed = false;
        Debug.Log($"All of your allies health were healed by the Priest!");
     }

    public void priestBonk(int enemyIndex)
    {
        int attackDamage = damage;
        if (buffed)
        {
            attackDamage = damage + 3;
        }
        if (debuffed)
        {
            attackDamage = damage - 3;
        }
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        Debug.Log("The Enemy Was Bonked by the Priest!");
        enemyScript.takeDamage((int)(attackDamage * 0.6));
        debuffed = false;
        buffed = false;

        // play sound
        audioSource.clip = priestBonkSound;
        audioSource.Play();
    }


     //MC Skills
     public void mcEnhance(int characterIndex)
     {
        
        if (characterIndex == 0)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character priestScript);
            priestScript.buffed = true;
            Debug.Log("MC buffed the Priest's damage!");
        }

        if (characterIndex == 1)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character mcScript);
            mcScript.buffed = true;
            Debug.Log("MC buffed their own damage!");
        }

        if (characterIndex == 2)
        {
            characterManager.characters[characterIndex].TryGetComponent<Character>(out Character fighterScript);
            fighterScript.buffed = true;
            Debug.Log("MC buffed the Fighter's damage!");
        }
        
    }

     public void mcSunder(int enemyIndex)
     {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        enemyScript.debuffed = true;
        Debug.Log("MC debuffed the enemies damage!");
        debuffed = false;
        buffed = false;
    }

    public void mcSlash(int enemyIndex)
    {
       int attackDamage = damage;
       if (buffed)
       {
           attackDamage = damage + 3;
       }
       if (debuffed)
       {
           attackDamage = damage - 3;
       }
       enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
       Debug.Log("The Enemy Was Slashed by the MC!");
       enemyScript.takeDamage((int)(attackDamage * 0.8));
       debuffed = false;
       buffed = false;
    }


    //Fighter Skills
    public void fighterChop(int enemyIndex)
    {
        enemyManager.enemies[enemyIndex].TryGetComponent<Enemy>(out Enemy enemyScript);
        Debug.Log("The Enemy Was Chopped by the Fighter!");
        enemyScript.takeDamage(damage);
        debuffed = false;
        buffed = false;
    }

    public void fighterCleave()
    {

       for (int i = enemyManager.enemies.Count - 1; i >= 0; i--)
       {
           enemyManager.enemies[i].TryGetComponent<Enemy>(out Enemy enemyScript);
           enemyScript.takeDamage( (int)((float)damage * 0.6));
       }
       debuffed = false;
       buffed = false;
       Debug.Log("All Enemies were Cleaved by the Fighter!");
    }

    public void fighterBreathe(int healAmount)
    {
       characterManager.characters[2].TryGetComponent<Character>(out Character fighterScript);
       int healthIncrease = fighterScript.healHealth(healAmount);
       Debug.Log($"Fighter healed their health by {healthIncrease} !");
       debuffed = false;
       buffed = false;


    }

    private void DisplayBuffMarker() {
        if (buffed) {
            buffMarker.SetActive(true);
        } else {
            buffMarker.SetActive(false);
        }
    }

    void Update()
    {
        DisplayBuffMarker();
    }


}
