using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [Header("Stats")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpsAvailable;
    [SerializeField] private int maxJumps = 1;

    [Header("Components")]
    [SerializeField] private GameObject sprite;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject cam;

    private float curMoveInput;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hpTextVertical;
    [SerializeField] private TextMeshProUGUI hpTextHorizontal;

    public static Player instance;

    private void Awake()
    {
        if (instance != this && instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        hpTextVertical.text = $"{curHP}/{maxHP}";
        hpTextHorizontal.text = hpTextVertical.text;
    }
    private void FixedUpdate()
    {
        Move();

        if (transform.position.y < -10)
            Die();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        curMoveInput = Mathf.RoundToInt(context.ReadValue<float>());
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            anim.SetTrigger("Attack");
            TryAttack();
        }
    }

    void TryAttack()
    {
        Vector2 direction = sprite.transform.right; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.2f, enemyLayer);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Character>()?.OnTakeDamage(attackDamage);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (jumpsAvailable > 0)
            {
                jumpsAvailable--;
                Jump();
            }
        }
    }

    void Move()
    {
        rig.linearVelocity = new Vector2(curMoveInput * moveSpeed, rig.linearVelocity.y);

        if (curMoveInput < 0)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
        else if (curMoveInput > 0)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, -0, 0);
        }
    }

    void Jump()
    {
        rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
        rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.point.y < transform.position.y)
            {
                jumpsAvailable = maxJumps;
            }
        }
    }



    public override void Die()
    {
        cam.transform.parent = null;
        sprite.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(1).buildIndex);
    }

    public override void OnTakeDamage(int damage)
    {
        base.OnTakeDamage(damage);
        hpTextVertical.text = $"HP {curHP} / {maxHP}";
        hpTextHorizontal.text = hpTextVertical.text;
    }

    public override void OnHeal(int hp)
    {
        base.OnHeal(hp);
        hpTextVertical.text = $"HP {curHP} / {maxHP}";
        hpTextHorizontal.text = hpTextVertical.text;
    }
}
