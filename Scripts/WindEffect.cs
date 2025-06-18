using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float windStrength = 5f;  // Base wind strength
    public float windSpeed = 1f;     // Speed of wind change
    public float windChangeInterval = 3f; // Time interval to change direction

    private Rigidbody rb;
    private float targetWindForce;   // The target wind force
    private float currentWindForce;  // Current wind force (for smooth transition)
    private float nextWindChangeTime; // Time when wind direction changes

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeWindDirection(); // Set initial wind direction
    }

    void FixedUpdate()
    {
        // Smoothly transition the wind force
        currentWindForce = Mathf.Lerp(currentWindForce, targetWindForce, Time.deltaTime * windSpeed);

        // Apply wind force
        rb.AddForce(new Vector3(currentWindForce, 0, 0), ForceMode.Acceleration);

        // Change wind direction at intervals
        if (Time.time >= nextWindChangeTime)
        {
            ChangeWindDirection();
        }
    }

    void ChangeWindDirection()
    {
        // Randomly set wind direction (left or right)
        targetWindForce = Random.Range(-windStrength, windStrength);
        nextWindChangeTime = Time.time + windChangeInterval;
    }
}

