using System.Collections;
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

    Coroutine flashRoutine;

    public virtual void OnTakeDamage(int damage)
    {
        curHP -= damage;

        if (curHP < 0)
        {
            Die();
            curHP = 0;
        }

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(DamageFlash());
    }

    public virtual void OnHeal(int hp)
    {
        curHP += hp;
        
        if(curHP > maxHP)
            curHP = maxHP;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual IEnumerator DamageFlash(float duration = 0.15f)
    {
        Color originalColor = sR.color;
        sR.color = Color.red;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            sR.color = Color.Lerp(Color.red, originalColor, t / duration);
            yield return null;
        }

        sR.color = originalColor;
    }




}
