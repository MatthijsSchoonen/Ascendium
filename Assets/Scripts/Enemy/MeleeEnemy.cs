using System.Collections;
using UnityEngine;

public class MeleeEnemy : Character
{
    [Header("Movement")]
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float chaseDistance; // 0 = infinite

    [Header("Combat")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange = 0.2f;
    [SerializeField] private Transform sprite;

    private Vector3 lastPosition;
    private Vector3 startPos;
    private Vector3 targetPos;
    private GameObject player;

    private State curState;
    private bool onAttackCooldown;

    private bool hasAggro;

    private enum State
    {
        Idle,
        Chase
    }

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        player = FindFirstObjectByType<Player>().gameObject;
        curState = State.Idle;
        lastPosition = transform.position;
    }

    void Update()
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
        }

        HandleFlip();
    }

    private void HandleFlip()
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


    private void Idle()
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

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            moveSpeed * Time.deltaTime
        );
    }

    private void UpdateState()
    {
        if (onAttackCooldown)
        {
            curState = State.Idle;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool inChaseRange = chaseDistance == 0 || distance <= chaseDistance;

        bool playerAbove = player.transform.position.y >= transform.position.y;

        if (!hasAggro && playerAbove && inChaseRange)
        {
            hasAggro = true;
            curState = State.Chase;
            return;
        }

        if (hasAggro && inChaseRange)
        {
            curState = State.Chase;
            return;
        }

        hasAggro = false;
        curState = State.Idle;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || onAttackCooldown)
            return;

        Player playerScript = collision.GetComponent<Player>();
        if (playerScript == null)
            return;

        playerScript.OnTakeDamage(attackDamage);
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onAttackCooldown = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 from = Application.isPlaying ? startPos : transform.position;
        Vector3 to = from + moveOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawWireSphere(from, 0.2f);
        Gizmos.DrawWireSphere(to, 0.2f);
    }
}
