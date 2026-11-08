using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    GameObject battleSystem;
    EnemyManager enemyManager;

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
        // get enemy manager script
        battleSystem = GameObject.Find("BattleSystem");
        enemyManager = battleSystem.GetComponent<EnemyManager>();

        // get character scripts
        mcScript = MC.GetComponent<Character>();
        priestScript = Priest.GetComponent<Character>();
        fighterScript = Fighter.GetComponent<Character>();
    }

    int getRandomCharacter()
    {
        int character = Random.Range(0, 2);
        return character;
    }
    public void attack(int damage, bool aoe)
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
