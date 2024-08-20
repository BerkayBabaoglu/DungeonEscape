using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Transform targetPoint;
    private SpriteRenderer mossGiantSprite;
    private bool isFacingRight = true;

    
    
    void Start()
    {
        targetPoint = pointA;
        speed = 1;
        mossGiantSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);


        if(Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
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

    
    
}
