using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    
    void Start()
    {
        
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("My name is: " + this.gameObject.name);
    }

    public override void Update()
    {
        Debug.Log("Spider is coming.");

    }

}
