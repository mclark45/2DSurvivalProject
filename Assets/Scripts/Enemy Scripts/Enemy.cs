using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject _player;
    [SerializeField] protected float _speed = 1.0f;
    [SerializeField] protected int _health = 3;

    public virtual void Init()
    {
        // Sets the enemies target to the player.
        SetTarget();
    }

    private void Start()
    {
        // Calls the Init function.
        Init();
    }

    protected virtual void Update()
    {
        // Updates the enemies position to chase player.
        Chase();
    }

    /**
     * gets the GameObject with the tag player
     * and sets it to the _player GameObject variable
     **/ 
    protected virtual void SetTarget()
    {
        // Set the player gameobject and gives an error if player is null.
        _player = GameObject.FindWithTag("Player");

        if (_player == null)
            Debug.LogError("Player is Null.....");
    }

    /**
     * Sets the enemy to face the player and then move toward them
     **/ 
    protected virtual void Chase()
    {
       // Sets the enemies forward position to face the player.
        transform.right = _player.transform.position - transform.position;
        // Updates the enemies position to move toward the player.
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
