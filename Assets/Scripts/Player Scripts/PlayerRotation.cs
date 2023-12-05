using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        // Gets the position of the cursor.
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Gets the direction facing the cursor.
        Vector2 forwardDirection = cursorPosition - transform.position;

        // Get the angle of the direction the cursor is facing
        float angle = Vector2.SignedAngle(Vector2.up, forwardDirection);
        // Sets the players rotation to face the cursor.
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
