using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    GameObject battleSystem;
    EnemyManager enemyManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] GameObject MC;
    [SerializeField] GameObject Priest;
    [SerializeField] GameObject Fighter;

    [SerializeField] GameObject debuffMarker;

    Character mcScript;
    Character priestScript;
    Character fighterScript;

    public int damage;
    public bool aoe = false;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;
    public bool buffed = false;
    public bool debuffed = false;

    public GameObject healthBar;
    public HealthBar healthBarScript;

    void Start()
    {
        // get enemy manager and character manager script
        battleSystem = GameObject.Find("BattleSystem");
        enemyManager = battleSystem.GetComponent<EnemyManager>();
        characterManager = battleSystem.GetComponent<CharacterManager>();
    }
    private int getRandomCharacter()
    {
        int character = Random.Range(0, 3);
        return character;
    }
    public void attackCharacter()
    {
        int attackRandom = getRandomCharacter();
        bool attacking = true;
        characterManager.characters[0].TryGetComponent<Character>(out Character priestScript);
        characterManager.characters[1].TryGetComponent<Character>(out Character mcScript);
        characterManager.characters[2].TryGetComponent<Character>(out Character fighterScript);

        int attackDamage = damage;
        if (buffed) {
            attackDamage = damage + 3;
        }
        if (debuffed) {
            attackDamage = damage - 3;
        }

        if (aoe) 
        {
            if (mcScript.isAlive)
            {
                mcScript.takeDamage(attackDamage);
            }

            if(priestScript.isAlive)
            {
                priestScript.takeDamage(attackDamage);
            }

            if (fighterScript.isAlive)
            {
                fighterScript.takeDamage(attackDamage);
            }
            debuffed = false;
            buffed = false;
            return;
        }
        while (attacking)
        {
            switch (attackRandom % 3)
            {
                case (0):
                    if (priestScript.isAlive)
                    {
                        priestScript.takeDamage(attackDamage);
                        attacking = false;
                    }
                    break;
                case (1):
                    if (mcScript.isAlive)
                    { 
                        mcScript.takeDamage(attackDamage);
                        attacking = false;
                    }
                    break;
                case (2):
                    if (fighterScript.isAlive)
                    {
                        fighterScript.takeDamage(attackDamage);
                        attacking = false;
                    }
                    break;

            }
            attackRandom++;
        }
        debuffed = false;
        buffed = false;
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

    public void GetHealthBar(GameObject healthBarObject) {
        healthBar = healthBarObject;
        healthBarScript = healthBar.GetComponent<HealthBar>();
    }

    private void Update()
    {
        DisplayDebuffIcon();
        healthBarScript.UpdateHealth(currentHealth);
    }

    private void DisplayDebuffIcon() {
        if (debuffed) {
            debuffMarker.SetActive(true);
        } else {
            debuffMarker.SetActive(false);
        }
    }
}
