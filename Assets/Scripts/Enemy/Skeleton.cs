using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy , IDamagable
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

        if (isDead == true)
            return;
        

        Debug.Log("Skeleton::Damage()");
        health--;
        
        isHit = true;
       

        if(health < 1)
        {
            isDead = true;
            
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }

    }

}
