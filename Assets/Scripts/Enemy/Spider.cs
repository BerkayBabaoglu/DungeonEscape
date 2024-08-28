using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
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

    private void Start()
    {
        _spiderSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public override void Movement()
    {
        // Spider hareket etmiyor, yerinde duruyor.
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
        GameObject acid = Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
        AcidEffect acidEffect = acid.GetComponent<AcidEffect>();

        if (acidEffect != null)
        {
            acidEffect.SetDirection(_spiderSprite.flipX);  //asite yon bilgisi gonderiyor
        }
    }
}

