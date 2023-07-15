using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject _bulletSpawn;
    [SerializeField] private int _ammoCount = 10;
    private bool _canFire = true;
    private bool _reloading = false;
    
    void Start()
    {
        // Initailize the player input actions.
        _input = new PlayerInputActions();
        // Enable the Player action map.
        _input.Player.Enable();
        _input.Player.Fire.performed += Fire_performed;
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Fire();
    }

    void Update()
    {
        // Calls the movement function.
        Movement();
    }

    private void Movement()
    {
        // gets the xy value between -1 and 1 for player movement.
        var move = _input.Player.Movement.ReadValue<Vector2>();
        // Moves the player.
        transform.Translate(move * Time.deltaTime * _speed);
    }

    private void Fire()
    {
        if (_ammoCount > 0)
        {
            _ammoCount--;
            // Gets a bullet from the bullet pool.
            GameObject bullet = PoolManager.Instance.RequestBullet();
            // Sets the position of the bullet to infront of player.
            bullet.transform.position = _bulletSpawn.transform.position;
            // Sets rotation of bullet moving forward from player.
            bullet.transform.rotation = this.gameObject.transform.GetChild(0).rotation;
        }

        if (_ammoCount == 0 && _canFire != false)
        {
            _canFire = false;
            if (_reloading == false)
            {
                _reloading = true;
                Debug.Log("Reloading....");
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3.0f);
        _ammoCount = 10;
        _canFire = true;
        _reloading = false;
        Debug.Log("Ready to fire......");
        StopCoroutine(Reload());
    }
}
