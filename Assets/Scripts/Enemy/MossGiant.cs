using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();

    }

    public void Damage()
    {
        Debug.Log("MossGiant::Damage()");
        health--;
        isHit = true;
        if (health < 1)
        {
            isDead = true;
        }

    }

    
}