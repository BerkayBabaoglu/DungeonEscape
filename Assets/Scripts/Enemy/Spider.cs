using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy , IDamagable
{

    

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        //sit still

    }

    public void Damage()
    {

    }
}
