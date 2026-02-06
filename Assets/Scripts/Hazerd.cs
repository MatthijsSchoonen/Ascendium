using UnityEngine;

public class Hazerd : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Player.instance.OnTakeDamage(damage);
    }

    public void SetDamage(int damageOverride)
    {

        if (damageOverride > 0)
            damage = damageOverride;
    }

}
