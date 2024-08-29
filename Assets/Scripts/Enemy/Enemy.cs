using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public GameObject diamondPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    protected bool isHit = false;
    protected bool isDead = false;
    protected int currentTargetIndex = 0;
    protected float distanceFromTarget = 0.3f;
    private int situationCounter = 1;
    private bool isAttacking = false;
    private Coroutine attackCR;


    [SerializeField]
    protected Transform[] patrolPoints;
    protected Transform currentTarget;


    protected Animator anim;


    protected SpriteRenderer sprite;


    protected Player player;


    protected EnemyState currentState;


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
            if (currentState == EnemyState.Patrolling)
                OnTargetPointReached();
            else if (!isAttacking)
            {
                isAttacking = true;
                attackCR = StartCoroutine(AttackIE());

            }

        }
        else
        {
            if(currentState == EnemyState.Incombat && isAttacking)
            {
                isAttacking = false;
                StopCoroutine(attackCR);
            }

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

    public virtual void SetCurrentState(EnemyState state)
    {
        currentState = state;

        switch (currentState)
        {
            case EnemyState.Idle:
                anim.SetInteger("AnimIndex", (int)AnimationType.Idle);
                break;
            case EnemyState.Dead:
                anim.SetInteger("AnimIndex", (int)AnimationType.Death);
                break;
            case EnemyState.Patrolling:
                anim.SetInteger("AnimIndex", (int)AnimationType.Walk);
                break;
        }
    }

    public void SetCurrentTarget()
    {
        if (patrolPoints.Length == 0 && patrolPoints == null)
            return;

        currentTarget = patrolPoints[++currentTargetIndex % patrolPoints.Length];

        SetCurrentState(EnemyState.Patrolling);

        FlipImage();
    }


    public void OnPlayerInteracted()
    {
        if (player != null && !player.IsDead())
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            currentTarget = new GameObject().transform;
            currentTarget.position = targetPosition;

            //flip the enemy
            if (currentTarget.position != null)
            {
                sprite.flipX = currentTarget.position.x < transform.position.x;
            }


            if (isHit)
            {
                anim.SetInteger("AnimIndex", (int)AnimationType.Hit);
            }
            else
            {
                StartCoroutine(AttackIE());
            }
        }
        
    }

    IEnumerator AttackIE()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger("AnimIndex", (int)AnimationType.Attack);
    }

    public void OnPlayerExited()
    {
        SetCurrentState(EnemyState.Patrolling);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player trigger'a girdiðinde
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null && !player.IsDead())
            {
                OnPlayerInteracted();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Player trigger'dan çýktýðýnda
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null && !player.IsDead())
            {
                OnPlayerExited();
            }
        }
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
        if (isDead)
        {
            if (situationCounter == 1)
            {
                StartCoroutine(DestroyAfterDeath());
                situationCounter = 0;
            }
        }
    }

    IEnumerator SituationIE()
    {
        if (!isDead && currentState != EnemyState.Dead)
        {
            yield return new WaitForSeconds(1.5f);
            SetCurrentState(EnemyState.Incombat);
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        
        if(anim == null)
        {
            Debug.Log("Death animation is NULL");
        }
        else
        {
            anim.SetInteger("AnimIndex", (int)AnimationType.Death);
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    public enum EnemyState
    {
        Idle,
        Incombat,
        Dead,
        Patrolling,
    }

    public enum AnimationType
    {
        Idle = 0,
        Walk,
        Death,
        Attack,
        Hit,
        HitLeft
    }
}

