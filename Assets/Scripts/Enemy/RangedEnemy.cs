using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform muzzle;

    protected override void UpdateState()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool inChaseRange = chaseDistance == 0 || distance <= chaseDistance;

        if (transform.position.y > player.transform.position.y && !hasAggro)
        {
            return;
        }

        if (!hasAggro && inChaseRange)
            hasAggro = true;

        if (!hasAggro || !inChaseRange)
        {
            curState = State.Idle;
            return;
        }

        curState = onAttackCooldown && hasAggro ? State.Idle : State.Attack;
    }


    protected override void Attack()
    {
        if (onAttackCooldown)
            return;

        SpawnProjectile();
        StartCoroutine(AttackCooldownRoutine());
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            muzzle.position,
            Quaternion.identity
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(
            Mathf.Sign(sprite.localScale.x) * projectileSpeed,
            0
        );

        projectile.GetComponent<Projectile>().SetDamage(attackDamage);
    }
}
