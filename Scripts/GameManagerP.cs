using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // ✅ Import SceneManager

public class KeyCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🎉 You Win! ...");

            // Load Scene 3
            SceneManager.LoadScene(5);

            // Remove the key after collection
            Destroy(gameObject);
        }
    }
}
