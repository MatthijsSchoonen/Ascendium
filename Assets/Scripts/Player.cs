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

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 0.1f;

    [Header("Components")]
    [SerializeField] private GameObject sprite;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject cam;
    [SerializeField] private BoxCollider2D box;

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

        if(curHP <= 0)
            Die();
    }

    public override void OnHeal(int hp)
    {
        base.OnHeal(hp);
        hpTextVertical.text = $"HP {curHP} / {maxHP}";
        hpTextHorizontal.text = hpTextVertical.text;
    }


    private void Update()
    {
        if (IsGrounded())
        {
            jumpsAvailable = maxJumps;
        }
    }

    private bool IsGrounded()
    {
        Vector2 origin = new Vector2(
            box.bounds.center.x,
            box.bounds.min.y
        );

        Debug.DrawRay(origin, Vector2.down * rayLength, Color.magenta);
        return Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);
    }

}
