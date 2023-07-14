using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;
    private void OnEnable()
    {
        Invoke("Hide", 1f);
    }


    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
