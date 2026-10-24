using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] GameObject Enemy;
    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    public void takeDamage(int damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

   //Priest Skills
    /*void priestHeal()
    {

    }

    void priestBlessing()
    {

    }

    void priestBonk()
    {
        Enemy.takeDamage(damage);
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
        Enemy.takeDamage(damage);
    }


     //Fighter Skills
    void fighterChop()
    {
        Enemy.takeDamage(damage);
    }

    void fighterCleave()
    {

    }

    void fighterBreathe()
    {

    }*/
}
