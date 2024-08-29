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
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        health--;
        isHit = true;
        Debug.Log("Spider::Damage()");

        if (health < 1)
        {
            isDead = true;
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public void Attack()
    {
        Debug.Log("Spider::Attack()");
        GameObject acid = Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
        AcidEffect acidEffect = acid.GetComponent<AcidEffect>();
        if (acidEffect != null)
        {
            acidEffect.SetDirection(_spiderSprite.flipX);  //asite yon bilgisi gonderiyor
        }
    }

    public override void SetCurrentState(EnemyState state)
    {
        if (state == EnemyState.Patrolling)
            return;

        base.SetCurrentState(state);
    }
}

