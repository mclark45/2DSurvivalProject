using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject _player;
    [SerializeField] protected float _speed = 1.0f;

    public virtual void Init()
    {
        SetTarget();
    }

    private void Start()
    {
        // Sets the enemies target to the player.
        Init();
    }

    protected virtual void Update()
    {
        // Updates the enemies position to chase player.
        Chase();
    }

    protected virtual void SetTarget()
    {
        // Set the player gameobject if the player is found or give error that player is not found.
        if (GameObject.FindWithTag("Player") != null)
            _player = GameObject.FindWithTag("Player");

        else
            Debug.LogError("Player was not found!!!");
    }

    protected virtual void Chase()
    {
       // Sets the enemies forward position to face the player.
        transform.right = _player.transform.position - transform.position;
        // Updates the enemies position to move toward the player.
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
