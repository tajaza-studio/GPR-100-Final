using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite PriestAlive, PriestHurt, PriestDead;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangePriestSprite(int currentHealth, int maxHealth)
    {
        if(currentHealth >= (maxHealth / 2))
        {
            spriteRenderer.sprite = PriestAlive;
        }
        else if (currentHealth > 0)
        {
            spriteRenderer.sprite = PriestHurt;
        }
        else
        {
            spriteRenderer.sprite = PriestDead;
        }
    }
}
