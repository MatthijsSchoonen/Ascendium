using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int curHP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float moveSpeed;

    [Header("Components")]
    [SerializeField] protected SpriteRenderer sR;
    [SerializeField] protected Rigidbody2D rig;

    public virtual void OnTakeDamage(int damage)
    {
        curHP -= damage;

        if (curHP < 0)
            Die();
    }

    public virtual void OnHeal(int hp)
    {
        curHP += hp;
        
        if(curHP > maxHP)
            curHP = maxHP;
    }

    public virtual void Die()
    {
        Debug.Log("Died");
    }
}
