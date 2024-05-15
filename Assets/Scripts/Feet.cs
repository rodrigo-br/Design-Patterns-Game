using System.Collections;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] private float maxJumpVelocity = 5;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    private bool isJumpPressed = false;
    private Rigidbody2D myRigidBody2D;
    private bool isGoingUp = false;

    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isJumpPressed && isGoingUp)
        {
            myRigidBody2D.velocity = Vector2.up * maxJumpVelocity;
            if (myRigidBody2D.velocity.y >= maxJumpVelocity)
            {
                isGoingUp = false;
            }
        }

        Vector2 baseJumpVelocity = Vector2.up * Physics2D.gravity.y * Time.deltaTime;
        if (myRigidBody2D.velocity.y < 0)
        {
            myRigidBody2D.velocity += baseJumpVelocity * (fallMultiplier - 1);
        }
        else if (myRigidBody2D.velocity.y > 0 && !isJumpPressed)
        {
            isGoingUp = false;
            myRigidBody2D.velocity += baseJumpVelocity * (lowJumpMultiplier - 1);
        }
    }

    private bool IsOnGround()
    {
        return myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void OnEnable()
    {
        StartCoroutine(AssignInputs());
    }

    private IEnumerator AssignInputs()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.InputObserver.OnJump += SetJump;
        GameManager.Instance.InputObserver.OnJumpStarted += StartJump;
    }

    private void SetJump(bool isPressed)
    {
        isJumpPressed = isPressed;
    }

    private void StartJump()
    {
        if (IsOnGround())
        {
            isGoingUp = true;
        }
    }
}
