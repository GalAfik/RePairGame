using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0, 1)]
    public float shakeIntensityMultiplier; // How fast the shaking effect should intensify
    [Range(0, 20)]
    public float shakeStartTime; // When should the shaking begin?

    private Timer timer; // The game timer that determines the intensity of the camera shake effect
    private float currentShakeIntensity; // How intense the shaking effect is right now
    private Vector3 initialPosition; // Used to store the starting position of the camera

    // Start is called before the first frame update
    void Start()
    {
        // Get the timer for easier access to properties
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        // Get the initial position of the camera
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the shaking about halfway through the timer
        if (timer.timeRemaining <= shakeStartTime && timer.timeRemaining > 0)
        {
            // Shake the camera
            transform.position = initialPosition + Random.insideUnitSphere * shakeIntensityMultiplier / 100;

            // Increase the shaking effect
            if (shakeIntensityMultiplier < 10)
                shakeIntensityMultiplier += 1f * Time.deltaTime;
        }
    }

    public void Reset()
    {
        initialPosition = transform.position;
    }
}
