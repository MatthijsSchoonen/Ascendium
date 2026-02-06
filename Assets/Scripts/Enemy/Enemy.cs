using System.Collections;
using UnityEngine;

public abstract class Enemy : Character
{
    [Header("Movement")]
    [SerializeField] protected Vector3 moveOffset;
    [SerializeField] protected float chaseDistance; // 0 = infinite
    [SerializeField] protected Transform sprite;
    [SerializeField] protected GameObject[] dropItems;

    [Header("Combat")]
    [SerializeField] protected float attackCooldown;
    protected bool onAttackCooldown;

    protected Vector3 startPos;
    protected Vector3 targetPos;
    protected Vector3 lastPosition;

    protected GameObject player;
    protected bool hasAggro;

    protected enum State
    {
        Idle,
        Chase,
        Attack

    }

    protected State curState;

    protected virtual void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        lastPosition = transform.position;
        player = FindFirstObjectByType<Player>().gameObject;
        curState = State.Idle;
    }

    protected virtual void Update()
    {
        UpdateState();

        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;

        }

        HandleFlip();
    }

    protected virtual void Idle()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            targetPos = targetPos == startPos
                ? startPos + moveOffset
                : startPos;
        }
    }

    protected virtual void Chase()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            moveSpeed * Time.deltaTime
        );
    }

    protected virtual void Attack()
    {
        // write logic in specific classses
    }

    protected virtual void UpdateState()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool inChaseRange = chaseDistance == 0 || distance <= chaseDistance;
        bool playerAbove = player.transform.position.y >= transform.position.y;

        if (!hasAggro && playerAbove && inChaseRange)
            hasAggro = true;

        if (!hasAggro || !inChaseRange)
        {
            curState = State.Idle;
            return;
        }

        curState = CanChasePlayer() ? State.Chase : State.Idle;
    }


    protected void HandleFlip()
    {
        float deltaX = transform.position.x - lastPosition.x;

        if (Mathf.Abs(deltaX) > 0.001f)
        {
            Vector3 scale = sprite.localScale;
            scale.x = Mathf.Abs(scale.x) * (deltaX > 0 ? 1 : -1);
            sprite.localScale = scale;
        }

        lastPosition = transform.position;
    }

    protected virtual void OnDrawGizmos()
    {
        Vector3 from = Application.isPlaying ? startPos : transform.position;
        Vector3 to = from + moveOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawWireSphere(from, 0.2f);
        Gizmos.DrawWireSphere(to, 0.2f);
    }

    protected virtual bool CanChasePlayer()
    {
        return true;
    }

    public override void Die()
    {
        if (dropItems.Length != 0)
        {
            GameObject toSpawn = dropItems[Random.Range(0, dropItems.Length)];
            Instantiate(toSpawn, transform.position, Quaternion.identity);
        }

        base.Die();
    }

    protected virtual IEnumerator AttackCooldownRoutine()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onAttackCooldown = false;
    }

}
