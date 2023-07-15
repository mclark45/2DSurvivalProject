using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base._health;
    }

    public void Damage()
    {
        Health--;
        Debug.Log("Health: " + Health);

        if (Health < 1)
            gameObject.SetActive(false);
    }
}
