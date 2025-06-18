using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ✅ Import TextMeshPro

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float jumpForce = 10f;

    [Header("Ground & Physics")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Audio Clips")]
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public AudioClip deathClip;

    [Header("Player Health")]
    public int health = 100;

    [Header("UI Elements")]
    public TextMeshProUGUI survivalTimeText; // ✅ Assign this in the Inspector

    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool isDead = false;
    private float survivalTime = 0f;
    private bool isCountingTime = true; // Timer control

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Ensure Rigidbody settings are correct
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Start the survival timer
        StartCoroutine(CountSeconds());
    }

    void Update()
    {
        if (isDead) return;

        // Check if player is on "Water" (ground)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle Movement
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * rotationSpeed;

        Vector3 movement = transform.forward * move;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        transform.Rotate(0, turn * Time.deltaTime, 0);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            audioSource.PlayOneShot(jumpClip);
        }

        // Play Footstep Sound
        if (isGrounded && move != 0 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footstepClip);
        }

        // Animator Updates
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("isGrounded", isGrounded);

        // Simulate health decrease (For Testing Purposes - Remove this in actual game)
        if (Input.GetKeyDown(KeyCode.K)) // Press 'K' to reduce health manually
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        audioSource.PlayOneShot(deathClip);

        // Stop the timer
        isCountingTime = false;

        // Update TextMeshPro to show final survival time
        survivalTimeText.text = "Survival Time: " + survivalTime + "s";

        Debug.Log("Final Survival Time: " + survivalTime + " seconds");

        // Stop movement completely
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        Debug.Log("Player died. Waiting to load Game Over scene...");
        StartCoroutine(WaitForDeathFreeze());
    }

    private IEnumerator WaitForDeathFreeze()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForSeconds(2f);

        if (Mathf.Approximately(lastPosition.x, transform.position.x) &&
            Mathf.Approximately(lastPosition.z, transform.position.z))
        {
            Debug.Log("Player is still. Loading Game Over scene...");
            LoadGameOverScene();
        }
        else
        {
            Debug.Log("Player moved. Scene won't change.");
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(1); // ✅ Ensure this scene is added to Build Settings
    }

    private IEnumerator CountSeconds()
    {
        while (isCountingTime)
        {
            yield return new WaitForSeconds(1f);
            survivalTime++;
            survivalTimeText.text = "Survival Time: " + survivalTime + "s"; // ✅ Update UI
            Debug.Log("Survival Time: " + survivalTime + " seconds");
        }
    }
}
