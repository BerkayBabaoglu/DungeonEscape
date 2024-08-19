using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    private bool _grounded = false;
    [SerializeField]
    private float speed = 2.5f;
    private PlayerAnimations _anim;
    


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            // Sað yöne hareket ediyor
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            // Sol yöne hareket ediyor
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(resetJumpRoutine());
            _anim.Jump(true);
        }
        
        _rigid.velocity = new Vector2(move * speed, _rigid.velocity.y);

        _anim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if(hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                _anim.Jump(false);
                return true;
            }
        }
        return false;
    }

    
    
    

    IEnumerator resetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}
