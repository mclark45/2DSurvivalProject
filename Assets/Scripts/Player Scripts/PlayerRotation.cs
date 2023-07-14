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
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 forwardDirection = cursorPosition - transform.position;

        float angle = Vector2.SignedAngle(Vector2.up, forwardDirection);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
