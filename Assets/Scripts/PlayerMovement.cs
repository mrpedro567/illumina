using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal, vertical, speed = 6;
    public int m_facingDirection = 1;
    public bool isEKeyPressed = false;
    public Rigidbody2D rb;

    public Vector2 direction = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setInput(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void setInteractable(InputAction.CallbackContext context)
    {
        isEKeyPressed |= context.ReadValue<bool>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction.x = horizontal;
        direction.y = vertical;

        rb.MovePosition(rb.position + (direction * speed * Time.deltaTime));

        if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
    }
}
