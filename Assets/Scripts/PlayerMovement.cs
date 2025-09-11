using UnityEngine;
using System.Collections;

public class PlayerMovementScript: MonoBehaviour{
    public float playerSpeed;
    public float sprintSpeed = 4f;
    public float walkSpeed = 1f;
    // public float mouseSensitivity = 2f;
    // public float jumpHeight = 3f;
    private bool isMoving = false;
    private int vertical = 0;
    private int horizontal = 0;
    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb;

    void Start(){
        vertical = 0; horizontal = 0;
        playerSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update(){
        isMoving = false;

        if (Input.GetKeyDown("w")){
            vertical += 1;
        }
        if (Input.GetKeyUp("w")){
            vertical -= 1;
        }

        if (Input.GetKeyDown("a")) {
            horizontal += 1;
        }
        if (Input.GetKeyUp("a")){
            horizontal -= 1;
        }

        if (Input.GetKeyDown("s")){
            vertical -= 1;
        }
        if (Input.GetKeyUp("s")){
            vertical += 1;
        }

        if (Input.GetKeyDown("d")){
            horizontal -= 1;
        }
        if (Input.GetKeyUp("d")){
            horizontal += 1;
        }

        isMoving = horizontal != 0 || vertical != 0;

        velocity.x = horizontal * playerSpeed;
        velocity.y = vertical * playerSpeed;

        rb.linearVelocity = velocity;
    }
}
