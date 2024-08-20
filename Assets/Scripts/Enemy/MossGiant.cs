using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Transform targetPoint;
    private SpriteRenderer mossGiantSprite;
    private bool isFacingRight = true;
    private Animator _anim;
    private bool isWaiting = true;
    
    
    void Start()
    {
        targetPoint = pointA;
        speed = 1;
        mossGiantSprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        if(isWaiting == true)
            StartCoroutine(WaitForIdleAnimation());
        else
            Movement();
    }
    
    private void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
            //yonu guncelle
            UpdateFacingDirection();
        }
    }

    private void UpdateFacingDirection()
    {
        //yonu hedef noktasina göre guncelle
        if(targetPoint == pointA)
        {
            mossGiantSprite.flipX = true;
            isFacingRight = false;
        }
        else if(targetPoint == pointB)
        {
            mossGiantSprite.flipX = false;
            isFacingRight = true;
        }
    }

    IEnumerator WaitForIdleAnimation()
    {
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        float idleDuration = stateInfo.length - stateInfo.normalizedTime * stateInfo.length;

        yield return new WaitForSeconds(idleDuration);
        isWaiting = false;
    }
    
}
