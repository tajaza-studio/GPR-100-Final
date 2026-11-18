using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public Sprite Alive, Hurt, Dead;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int currentHealth, int maxHealth)
    {
        if (currentHealth >= (maxHealth / 2))
        {
            spriteRenderer.sprite = Alive;
        }
        else if (currentHealth > 0)
        {
            spriteRenderer.sprite = Hurt;
        }
        else
        {
            spriteRenderer.sprite = Dead;
        }
    }

}
