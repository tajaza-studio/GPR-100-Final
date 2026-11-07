using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth = 20;

    private void Update() {
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        transform.localScale = new Vector3(healthPercentage, 1f, 1f);
    }
}
