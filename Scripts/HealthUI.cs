using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthFill;  // The Red Fill Bar
    public PlayerHealth playerHealth;  // Reference to Player's Health Script

    void Update()
    {
        if (playerHealth != null && healthFill != null)
        {
            float healthPercent = playerHealth.currentHealth / playerHealth.maxHealth;
            healthFill.rectTransform.localScale = new Vector3(healthPercent, 1, 1); // Shrink bar
        }
    }
}
