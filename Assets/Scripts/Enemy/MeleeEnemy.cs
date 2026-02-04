using UnityEngine;
using System.Collections;
public class MeleeEnemy : Enemy
{
    [Header("Combat")]
    [SerializeField] private float attackCooldown;
    private bool onAttackCooldown;

    protected override bool CanChasePlayer()
    {
        return !onAttackCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || onAttackCooldown)
            return;

        collision.GetComponent<Player>()?.OnTakeDamage(attackDamage);
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onAttackCooldown = false;
    }
}
