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
        if(player != null && !player.IsDead())
        {
            // Sadece x ekseninde hareket etmek için, player'ýn y konumunu koruyarak yeni bir hedef pozisyon oluþturuyoruz
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            currentTarget = new GameObject().transform;  // Geçici bir GameObject kullanarak hedef oluþturuyoruz
            currentTarget.position = targetPosition;

            SetCurrentState(EnemyState.Incombat);
            StartCoroutine(AttackIE());
        }
       
    }

    //public void OnPlayerInteracted()
    //{
        
    //    currentTarget = player.transform;
    //    StartCoroutine(AttackIE());
        
    //}

    IEnumerator AttackIE()
    {
        yield return new WaitForSeconds(1.5f);
        SetCurrentState(EnemyState.Incombat);
        
        
    }

    public void OnPlayerExited()
    {
        if (!player.IsDead())
        {
            SetCurrentTarget();
        }
        else
        {
            SetCurrentState(EnemyState.Patrolling);
            SetCurrentTarget();
        }
        
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
            SetCurrentState(EnemyState.Death);
            StartCoroutine(DestroyAfterDeath());
            return;
        }

        if (isHit)
        {
            StartCoroutine(SituationIE());
        }
        isHit = false;
    }

    IEnumerator SituationIE()
    {
        SetCurrentState(EnemyState.Hit);  // Önce Hit durumuna geçiyoruz.
        yield return new WaitForSeconds(1.5f);
        SetCurrentState(EnemyState.Incombat);  // Hit animasyonu bittiðinde tekrar Incombat durumuna geçiyoruz.
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
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

