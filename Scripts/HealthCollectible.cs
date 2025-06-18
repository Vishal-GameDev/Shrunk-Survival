using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healAmount = 20;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("❤️ Collision detected with: " + other.name);

        // ✅ Find PlayerHealth component correctly
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            Debug.Log("✅ Before Healing: " + playerHealth.currentHealth);
            playerHealth.IncreaseHealth(healAmount);
            Debug.Log("✅ After Healing: " + playerHealth.currentHealth);

            Destroy(gameObject); // Remove collectible after collection
        }
        else
        {
            Debug.LogError("❌ PlayerHealth component NOT found on: " + other.name);
        }
    }
}
