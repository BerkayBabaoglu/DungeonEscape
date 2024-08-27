using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _anim;
    private Animator _swordAnim;



    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jump", jumping);
    }


    public void Attack()
    {

        
        if (transform.localScale.x> 0)
        {
            _anim.SetTrigger("Attack");
        }
        else if(transform.localScale.x < 0)
        {
            
            _anim.SetTrigger("Attack_Left");
        }

        //_anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");

    }

    public void DeathPlayer()
    {
        _anim.SetTrigger("Death");
    }

    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }

}
