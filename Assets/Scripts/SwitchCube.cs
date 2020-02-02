using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCube : MonoBehaviour, Triggerable
{
    public enum Direction // The different directions that this object can move in
    {
        Forward
        , Back
        , Up
        , Down
        , Left
        , Right
    }
    public static readonly Vector3 Forward = new Vector3(0, 0, 1);
    public static readonly Vector3 Back = new Vector3(0, 0, -1);
    public static readonly Vector3 Up = new Vector3(0, 1, 0);
    public static readonly Vector3 Down = new Vector3(0, -1, 0);
    public static readonly Vector3 Left = new Vector3(-1, 0, 0);
    public static readonly Vector3 Right = new Vector3(1, 0, 0);


    public float velocity; // How fast the cube should move when triggered
    public bool triggered; // Is this cube currently triggered?
    public Direction moveDirection; // Where should this object move when triggered?
    private Vector3 initialPosition; // The starting position of the cube
    private Vector3 triggeredPosition; // The ending position of the cube when it is triggered

    // Start is called before the first frame update
    void Start()
    {
        // Get the initial position of the cube object
        initialPosition = transform.position;

        // Calculate the actual ending position using the relative triggered position vector
        Vector3 relativeTriggeredPosition;
        switch (moveDirection)
        {
            case Direction.Forward:
                relativeTriggeredPosition = Forward; break;
            case Direction.Back:
                relativeTriggeredPosition = Back; break;
            case Direction.Up:
                relativeTriggeredPosition = Up; break;
            case Direction.Down:
                relativeTriggeredPosition = Down; break;
            case Direction.Left:
                relativeTriggeredPosition = Left; break;
            case Direction.Right:
                relativeTriggeredPosition = Right; break;
            default:
                relativeTriggeredPosition = Vector3.zero; break;
        }
        // Multiplying the direction vector by 2 to account for cube size
        triggeredPosition = initialPosition + relativeTriggeredPosition * 1.9f;
    }

    // Update is called once per frame
    void Update()
    {
        // If the cube is triggered, move it towards it's triggered position. Otherwise, move it back to its initial position.
        if (triggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, triggeredPosition, velocity * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, velocity * Time.deltaTime);
        }
    }

    public void Trigger() { triggered = true; }
    public void UnTrigger() { triggered = false; }
}
