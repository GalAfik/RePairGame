using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public GameObject gameMaster;
    public bool hasControl; // Do the players have control over this object?
    public int joystickNumber; // Which joystick is being used to control this player object?
    public float velocity; // How fast the player object should move. will not implement acceleration for now.
    private Vector3 move; // The movement vector for the player. input should manipulate this vector before being applied to the object.
    private ParticleSystem.EmissionModule heartsEmitter; // The particle system emitter that emits hearts when players win

    private void Start()
    {
        // Get the particle system
        heartsEmitter = transform.Find("Hearts").GetComponent<ParticleSystem>().emission;
        // Turn off the emitter to start off
        heartsEmitter.enabled = false;

        // Start without player control over the objects
        hasControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Zero out the movement vector
        move = Vector3.zero;

        // Only take controller input if the players have control over the player objects
        if (hasControl)
        {
            // Add joystick movement to the move vector
            move = new Vector3(
                Input.GetAxis("Horizontal" + joystickNumber.ToString())
                , 0
                , Input.GetAxis("Vertical" + joystickNumber.ToString())
                );
        }

        // Create a vector to check horizontal movement, eliminating elevation movement
        Vector2 horizontalMovement = new Vector2(move.x, move.z);
        // Set running animation
        transform.Find("Jammo_Player").GetComponent<Animator>().SetFloat("velocity", horizontalMovement.magnitude);
        // Turn to look in the running direction
        if (horizontalMovement != Vector2.zero)
        {
            transform.forward = move.normalized;
        }

        // Apply move vector to the transform
        transform.position += move.normalized * velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When the players collide with each other, stop the timer and start the hearts particle system
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameMaster != null)
            {
                if (gameMaster.GetComponent<GameMaster>().timer.GetComponent<Timer>().timeRemaining > 0)
                {
                    // Tell the master object to start the level winning sequence
                    StartCoroutine(gameMaster.GetComponent<GameMaster>().WinLevel());
                }
            }
            // Start the hearts particle system
            heartsEmitter.enabled = true;

            // Stop the players in place
            hasControl = false;
            move = Vector3.zero;
        }
    }

    // Reset the player back to its initial state
    public void ResetPlayer()
    {
        heartsEmitter.enabled = false;
        transform.Find("Hearts").GetComponent<ParticleSystem>().Clear();
    }
}
