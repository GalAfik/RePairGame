using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject[] triggeredObjects; // The object to be triggered
    public bool triggered; // Is the player touching this object?
    public float velocity; // How fast the trigger moves when stepped on

    private Vector3 initialPosition; // The starting position of the cube
    private Vector3 triggeredPosition; // The ending position of the cube when it is triggered
    private Transform triggerSwitch; // The physical object to move when triggered

    // Start is called before the first frame update
    void Start()
    {
        // The player should not be touching this object to begin with
        triggered = false;

        // Get the physical object to move
        triggerSwitch = transform.Find("TriggerObject");

        // Get the initial position of this object
        initialPosition = triggerSwitch.position;

        // Set the triggered position based on the height of this object
        triggeredPosition = initialPosition + new Vector3(0, -0.08f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Trigger::triggered = " + triggered);
        // If the cube is triggered, move it towards it's triggered position. Otherwise, move it back to its initial position.
        if (triggered)
        {
            triggerSwitch.position = Vector3.MoveTowards(triggerSwitch.position, triggeredPosition, velocity * Time.deltaTime);
        }
        else
        {
            triggerSwitch.position = Vector3.MoveTowards(triggerSwitch.position, initialPosition, velocity * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Trigger both this and the other object when the player enters this trigger collider
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            foreach (GameObject triggeredObject in triggeredObjects)
            {
                triggeredObject.GetComponent<Triggerable>().Trigger();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger both this and the other object when the player enters this trigger collider
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = false;
            foreach (GameObject triggeredObject in triggeredObjects)
            {
                triggeredObject.GetComponent<Triggerable>().UnTrigger();
            }
        }
    }
}
