using UnityEngine;

public class Health : MonoBehaviour
{
    public void UpdateHealthVisual(int value, int maxValue)
    {
        float healthPercentage = (float)value / (float)maxValue;
        if (healthPercentage < 0)
            healthPercentage = 0;
        transform.localScale = new Vector3(healthPercentage, 1f, 1f);
    }
}
