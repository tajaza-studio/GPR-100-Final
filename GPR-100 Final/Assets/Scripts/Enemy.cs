using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject MC;
    [SerializeField] GameObject Priest;
    [SerializeField] GameObject Fighter;


    public int damage;
    public bool aoe;
    public int maxHealth;
    public int currentHealth;

    void attack(int damage, bool aoe)
    {
        if (aoe) 
        {
            /*for (each character)
            {
                if (!isAlive)
                    continue;
                character.takeDamage(damage);
            }*/

           
            return;
        }

        //character.takeDamage(damage);
    }


    //void takeDamage(damage);


}
