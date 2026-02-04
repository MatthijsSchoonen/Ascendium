using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;


    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Player.instance.OnTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damageOveride)
    {
        if (damageOveride > damage)
            damage = damageOveride;
    }
}
