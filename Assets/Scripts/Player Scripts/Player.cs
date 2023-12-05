using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField] private float _speed = 5.0f;

    [Header("Ammo")]
    [SerializeField] private int _mode = 0;
    [SerializeField] private int _ammoCount = 10;
    [SerializeField] private float _reloadTime = 3.0f;
    [SerializeField] private List<GameObject> _bulletSpawns;
    private int _currentAmmoCount;
    private int _currentSingleAmmoCount;
    private int _currentTripleAmmoCount;
    private bool _canFire = true;
    private bool _reloading = false;

    void Start()
    {
        // set default shooting type
        _mode = 0;

        // initialize ammo count
        _currentAmmoCount = _ammoCount;
        _currentSingleAmmoCount = _ammoCount;
        _currentTripleAmmoCount = _ammoCount;
        // Initailize the player input actions.
        _input = new PlayerInputActions();
        // Enable the Player action map.
        _input.Player.Enable();
        _input.Player.Fire.performed += Fire_performed;
        _input.Player.Reload.performed += Reload_performed;
        _input.Player.Swap1.performed += Swap1_performed;
        _input.Player.Swap2.performed += Swap2_performed;

        UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);
    }


    #region Weapon Swapping
    private void Swap1_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // set mode to 0
        _mode = 0;
        // set _currentAmmoCount to _currentSingleAmmoCount;
        _currentAmmoCount = _currentSingleAmmoCount;
        UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);
    }

    private void Swap2_performed(InputAction.CallbackContext context)
    {
        // set mode to 1
        _mode = 1;
        // set _currentAmmoCount to _currentTripleAmmoCount;
        _currentAmmoCount = _currentTripleAmmoCount;
        UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);
    }
    #endregion


    #region Weapon Mechanics
    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Calls the fire function
        Fire();
    }

    private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Starts the reload coroutine.
        StartCoroutine(Reload(_mode));
    }
    #endregion

    void Update()
    {
        // Calls the movement function.
        Movement();
    }

    /**
     * Controls the movement mechanics of the player
     **/
    private void Movement()
    {
        // gets the xy value between -1 and 1 for player movement.
        var move = _input.Player.Movement.ReadValue<Vector2>();
        // Moves the player.
        transform.Translate(move * Time.deltaTime * _speed);
    }

    #region Firing Mechanics
    /**
     * Controls the firing mechanics of the player
     **/ 
    private void Fire()
    {
        if (_currentAmmoCount > 0 && _canFire)
        {
            // initialize bullet gameobject
            GameObject bullet;
            // subtract 1 from _currentAmmoCount
            _currentAmmoCount--;
            UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);

            // switch statement to shoot appropriate bullet pattern
            switch (_mode)
            {
                case 0:
                    // subtract 1 from _currentSingleAmmoCount
                    _currentSingleAmmoCount--;
                    // get bullet from bullet pool
                    bullet = PoolManager.Instance.RequestBullet();
                    // set position and rotation of bullet
                    bullet.transform.SetPositionAndRotation(_bulletSpawns[0].transform.position, this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).rotation);
                    break;
                case 1:
                    // subtract 1 from _currentTripleAmmoCount
                    _currentTripleAmmoCount--;
                    // loop from bullet spawns
                    for (int i = 0; i < _bulletSpawns.Count; i++)
                    {
                        // get bullet from bullet pool
                        bullet = PoolManager.Instance.RequestBullet();
                        // set position and rotation of bullet
                        bullet.transform.SetPositionAndRotation(_bulletSpawns[i].transform.position, this.gameObject.transform.GetChild(0).GetChild(0).GetChild(i).rotation);
                    }
                    break;
                default:
                    // print Mode Not Selected to the console
                    Debug.LogError("Mode Not Selected");
                    break;
            }
        }

        if (_currentAmmoCount == 0 && _reloading == false)
        {
            // start the reload
            StartCoroutine(Reload(_mode));
        }
    }

    /**
     * Coroutine that controls the reload mechanics for the player
     * @param mode: the mode of the players current weapon
     **/ 
    private IEnumerator Reload(int mode)
    {
        // switch statement to reload proper weapon 
        switch (_mode)
        {
            case 0:
                Debug.Log("Reloading Single Shot....");
                // set _canFire to false
                _canFire = false;
                // set _reloading to true
                _reloading = true;
                // wait for the given amount of time
                yield return new WaitForSeconds(_reloadTime);
                // reset _currentSingleAmmoCount to the _ammoCount
                _currentSingleAmmoCount = _ammoCount;
                // set _currentAmmoCount to _currentSingleAmmoCount
                _currentAmmoCount = _currentSingleAmmoCount;
                // update UI ammo count
                UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);
                // set _canFire to true
                _canFire = true;
                // set _reloading to false
                _reloading = false;
                Debug.Log("Ready to fire......");
                break;
            case 1:
                Debug.Log("Reloading Triple Shot....");
                // set _canFire to false
                _canFire = false;
                // set _reloading to true
                _reloading = true;
                // wait for the given amount of time
                yield return new WaitForSeconds(_reloadTime);
                // reset _currentTripleAmmoCount to the _ammoCount
                _currentTripleAmmoCount = _ammoCount;
                // set _currentAmmoCount to _currentTripleAmmoCount
                _currentAmmoCount = _currentTripleAmmoCount;
                // update UI ammo count
                UIManager.Instance.UpdatePlayerAmmoCounter(_currentAmmoCount);
                // set _canFire to true
                _canFire = true;
                // set _reloading to false
                _reloading = false;
                Debug.Log("Ready to fire......");
                break;
            default:
                Debug.LogError("Mode Not Selected");
                break;
        }
    }
    #endregion
}
