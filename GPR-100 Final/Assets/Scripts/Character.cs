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

















    //MC Skills
    void mcSkill1()
    {
        
    }

    void mcSkill2()
    {
        
    }

    void mcSkill3()
    {

    }

}
