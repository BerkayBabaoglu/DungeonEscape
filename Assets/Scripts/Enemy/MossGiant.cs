using System.Collections;
using System.Collections.Generic;
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

        float distance = Vector3.Distance(player.transform.position, transform.position);

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if(direction.x > 0 && anim.GetBool("InCombat") == true) 
        {
            sprite.flipX = false;
        }
        else if(direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
        }
    }

    public void Damage()
    {

        health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (health < 1)
        {
            Destroy(this.gameObject);
        }

    }
}