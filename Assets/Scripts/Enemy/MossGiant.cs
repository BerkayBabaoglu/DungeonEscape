using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Vector3 _currentTarget;
    private Animator _anim;
    private SpriteRenderer _mossGiantSprite;

    private void Start()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _mossGiantSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }



    public override void Update()
    {

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        
        Movement();

    }

    void Movement()
    {
        if (_currentTarget == pointA.position)
        {
            _mossGiantSprite.flipX = true;
        }
        else
        {
            _mossGiantSprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }
}