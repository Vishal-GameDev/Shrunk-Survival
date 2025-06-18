using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth = 50f;
    private float healthDecayRate = 0.5f;

    private Animator animator;  // Reference to Animator
    private bool isDead = false;

    void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator = GetComponent<Animator>();  // Get Animator component
        InvokeRepeating("HealthDecay", 1f, 1f);
    }

    void HealthDecay()
    {
        DecreaseHealth(healthDecayRate);
    }

    public void DecreaseHealth(float amount)
    {
        if (isDead) return;  // Prevent further health decrease after death

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (isDead) return;  // Prevent healing after death

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");  // Trigger the death animation
        CancelInvoke("HealthDecay");  // Stop health decay
        // Optionally, disable player movement script
        GetComponent<PlayerMovement>().enabled = false;
    }
}
