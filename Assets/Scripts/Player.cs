using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField] private float _speed = 5.0f;
    
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();
        transform.Translate(move * Time.deltaTime * _speed);
    }
}
