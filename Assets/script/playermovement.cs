using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public LayerMask interactableLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTime;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            FlipCharacter();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FlipCharacter()
    {
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Dash()
    {
        isDashing = true;
        dashTime = dashDuration;
        float dashDirection = moveInput != 0 ? moveInput : Mathf.Sign(transform.localScale.x);
        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);
        Invoke("StopDash", dashDuration);
    }

    void StopDash()
    {
        isDashing = false;
    }

void Interact()
{
    Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, 1f, interactableLayer);
    Debug.Log("Objects found: " + interactables.Length); // Check if any objects are detected

    foreach (Collider2D interactable in interactables)
    {
        if (interactable.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Interacting with: " + interactable.name);
            interactable.GetComponent<IInteractable>().Interact();
        }
        else
        {
            Debug.Log("No IInteractable script found on " + interactable.name);
        }
    }
}
}

public interface IInteractable
{
    void Interact();
}