using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    GameObject battleSystem;
    EnemyManager enemyManager;
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

    public GameObject healthBar;
    public HealthBar healthBarScript;

    void Start()
    {
        // get enemy manager and character manager script
        battleSystem = GameObject.Find("BattleSystem");
        enemyManager = battleSystem.GetComponent<EnemyManager>();
        characterManager = battleSystem.GetComponent<CharacterManager>();


        /* // get character scripts
        mcScript = MC.GetComponent<Character>();
        priestScript = Priest.GetComponent<Character>();
        fighterScript = Fighter.GetComponent<Character>(); */
    }

    private int getRandomCharacter()
    {
        int character = Random.Range(0, 2);
        return character;
    }
    public void attackCharacter()
    {
        bool aoe = false;
        int attackRandom = getRandomCharacter();
        bool attacking = true;
        characterManager.characters[0].TryGetComponent<Character>(out Character priestScript);
        characterManager.characters[1].TryGetComponent<Character>(out Character mcScript);
        characterManager.characters[2].TryGetComponent<Character>(out Character fighterScript);
        Debug.Log("Work Please");

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
        healthBarScript.UpdateHealth(currentHealth);
        if (currentHealth <= 0)
        {
            enemyManager.EnemyDies(gameObject, healthBar);
        }
    }

    public void GetHealthBar(GameObject healthBarObject)
    {
        healthBar = healthBarObject;
        healthBarScript = healthBar.GetComponent<HealthBar>();
    }

    public void debuffDamage()
    {
        damage -= 2;
    }

    private void Update()
    {
        healthBarScript.UpdateHealth(currentHealth);
    }
}
