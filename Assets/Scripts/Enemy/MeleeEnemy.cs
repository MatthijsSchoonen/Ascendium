using UnityEngine;
using System.Collections;
public class MeleeEnemy : Enemy
{
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


}
