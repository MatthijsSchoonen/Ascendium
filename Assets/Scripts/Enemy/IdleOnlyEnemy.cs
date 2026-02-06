using UnityEngine;

public class IdleOnlyEnemy : Enemy
{
    protected override void UpdateState()
    {
        curState = State.Idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        collision.GetComponent<Player>()?.OnTakeDamage(attackDamage);
    }
}
