using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{

    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 7.0f;
    private bool _resetJump = false;
    private bool _grounded = false;
    [SerializeField]
    private float speed = 2.5f;
    private PlayerAnimations _anim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    [SerializeField]
    private int health = 5;
    private bool isHit = false;
    private bool isDead = false;
    public int diamondAmount = 0;

    public int Health { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimations>();

        
        _playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>(); //sorunu getchild(0) yaparak çözdüm.
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AttackSystem();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            flip(true);
            // Sað yöne hareket ediyor

        }
        else if (move < 0)
        {
            flip(false);
            // Sol yöne hareket ediyor

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

        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _anim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

            //transform.localScale = new Vector3(1, 1, 1);


        }
        else if (faceRight == false)
        {

            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;

            //transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void AttackSystem()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {

            _anim.Attack();

        }
    }


    IEnumerator resetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        health--;
        isHit = true;
        if(isHit == true)
        {
            _anim.Hit();
            isHit = false;
        }
        
        if(health < 1 && !isDead)
        {
            _anim.DeathPlayer();
            isDead = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

}