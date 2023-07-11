using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _speed = 1.0f;
    void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            _player = GameObject.FindWithTag("Player");
        }
        else
        {
            Debug.LogError("Player was not found!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    private void Chase()
    {
        transform.right = _player.transform.position - transform.position;
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
