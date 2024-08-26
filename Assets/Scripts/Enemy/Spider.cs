using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spider : Enemy , IDamagable
{
    [SerializeField]
    private GameObject acidEffectPrefab;
    private SpriteRenderer _spiderSprite;
    

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
        health--;

        if (health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    public void Attack()
    {
        
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
        
    }

    public bool Flip(bool isLeft)
    {
        isLeft = _spiderSprite.flipX;
        return isLeft;
    }

    
}
