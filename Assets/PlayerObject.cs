using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int joystickNumber; // Which joystick is being used to control this player object?
    public float velocity; // How fast the player object should move. will not implement acceleration for now.
    private Vector3 move; // The movement vector for the player. input should manipulate this vector before being applied to the object.

    // Update is called once per frame
    void Update()
    {
        // Add joystick movement to the move vector
        move = new Vector3(
            Input.GetAxis("Horizontal" + joystickNumber.ToString())
            , 0
            , Input.GetAxis("Vertical" + joystickNumber.ToString())
            );

        // Apply move vector to the transform
        transform.position += move.normalized * velocity * Time.deltaTime;
    }
}
