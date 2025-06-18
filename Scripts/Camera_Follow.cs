using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Assign the player in the Inspector
    public Vector3 startOffset = new Vector3(0, 10, -10); // Start far from the player
    public Vector3 followOffset = new Vector3(0, 2, -5); // Normal follow offset
    public float approachSpeed = 2f; // Speed of approach to the player
    public float smoothSpeed = 5f; // Speed of smooth following
    public float rotationSpeed = 100f; // Rotation speed

    private float currentRotation = 0f; // Track camera rotation angle
    private bool hasReachedPlayer = false; // Check if camera reached the player

    void Start()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogError("Player not found! Assign the player manually.");
            }
        }

        // Start from a far position
        transform.position = player.position + startOffset;
        transform.LookAt(player);

        // Start the approach sequence
        StartCoroutine(ApproachPlayer());
    }

    void LateUpdate()
    {
        if (player != null && hasReachedPlayer)
        {
            // Handle rotation with A & D keys
            HandleRotation();

            // Smoothly follow the player
            Vector3 targetPosition = player.position + Quaternion.Euler(0, currentRotation, 0) * followOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            // Make camera always look at the player
            transform.LookAt(player);
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }

        // Apply rotation to player
        player.rotation = Quaternion.Euler(0, currentRotation, 0);
    }

    IEnumerator ApproachPlayer()
    {
        float distanceThreshold = 0.5f; // Stop moving when close enough

        while (Vector3.Distance(transform.position, player.position + followOffset) > distanceThreshold)
        {
            // Move camera towards player's follow position
            transform.position = Vector3.Lerp(transform.position, player.position + followOffset, approachSpeed * Time.deltaTime);
            yield return null; // Wait for next frame
        }

        // Camera has reached the player, enable normal follow
        hasReachedPlayer = true;
    }
}
