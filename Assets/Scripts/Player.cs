using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpsAvailable;
    [SerializeField] private GameObject sprite;
    public int maxJumps = 1;

    private float curMoveInput;

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        curMoveInput = Mathf.RoundToInt(context.ReadValue<float>());
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // attack
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
}
