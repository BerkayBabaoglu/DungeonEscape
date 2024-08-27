using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform[] patrolPoints;

    protected Transform currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    

    protected Player player;
    protected bool isDead = false;

    protected EnemyState currentState;

    protected int currentTargetIndex = 0;

    protected float distanceFromTarget = 0.3f;


    public virtual void Init()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        SetCurrentTarget();
    }

    private void Start()
    {
        Init();
    }

    public void Update()
    {
        if (currentState != EnemyState.Idle && !isDead)
        {
            Movement();
        }

        Situation();

    }

    public virtual void Movement()
    {
        if (currentTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        if (distance < 0.1f)
        {
            OnTargetPointReached();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
    }

    void OnTargetPointReached()
    {
        

        SetCurrentState(EnemyState.Idle);
        StartCoroutine(WaitInterPatrolIE());
    }

    private IEnumerator WaitInterPatrolIE()
    {
        yield return new WaitForSeconds(3f);
        SetCurrentTarget();
    }

    public void SetCurrentState(EnemyState state)
    {
        currentState = state;

        switch (currentState)
        {
            case EnemyState.Idle:
                anim.SetInteger("AnimIndex", (int)AnimationType.Idle);
                break;
            case EnemyState.Death:
                anim.SetInteger("AnimIndex", (int)AnimationType.Death);
                break;
            case EnemyState.Incombat:
                anim.SetInteger("AnimIndex", (int)AnimationType.Attack);
                break;
            case EnemyState.Patrolling:
                anim.SetInteger("AnimIndex", (int)AnimationType.Walk);
                break;
            case EnemyState.Hit:
                anim.SetInteger("AnimIndex", (int)AnimationType.Hit);
                break;
        }
    }

    public void SetCurrentTarget()
    {
        if (patrolPoints.Length == 0)
            return;

        currentTarget = patrolPoints[++currentTargetIndex % patrolPoints.Length];

        SetCurrentState(EnemyState.Patrolling);

        FlipImage();
    }

    public void OnPlayerInteracted()
    {
        
        currentTarget = player.transform;
        StartCoroutine(AttackIE());
        
    }

    IEnumerator AttackIE()
    {
        yield return new WaitForSeconds(1.5f);
        SetCurrentState(EnemyState.Incombat);
        
    }

    public void OnPlayerExited()
    {
        SetCurrentTarget();
    }

    void FlipImage()
    {
        if (currentTarget != null && patrolPoints.Length > 1)
        {
            sprite.flipX = currentTarget.position.x < transform.position.x;
        }
    }

    void Situation()
    {
        if (isDead == true)
        {
            SetCurrentState(EnemyState.Death);
            return;
        }

        if (isHit == true)
        {

            StartCoroutine(situationIE());
        }
        isHit = false;
    }

    IEnumerator situationIE()
    {
        yield return new WaitForSeconds(1.5f);
        SetCurrentState(EnemyState.Hit);
    }


    public enum EnemyState
    {
        Idle,
        Incombat,
        Death,
        Patrolling,
        Hit
    }

    public enum AnimationType
    {
        Idle = 0,
        Walk,
        Death,
        Attack,
        Hit
    }
}

