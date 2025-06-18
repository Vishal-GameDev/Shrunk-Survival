using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int fireDamage = 10;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 Fire Collision with: " + other.name);

        // ✅ Find PlayerHealth in parent (fix for 'model' issue)
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            Debug.Log("🔥 Before Fire Damage: " + playerHealth.currentHealth);
            playerHealth.DecreaseHealth(fireDamage);
            Debug.Log("🔥 After Fire Damage: " + playerHealth.currentHealth);
        }
        else
        {
            Debug.LogError("❌ PlayerHealth component NOT found on: " + other.name);
        }

        Destroy(gameObject); // Remove fire after touching the player
    }
}
