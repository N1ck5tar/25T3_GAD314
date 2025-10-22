using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private float horizontal; // direction to move left and right
    private bool isFacinRight;

    public float jumpPower;
    public float movementSpeed;

    public LayerMask groundLayer;

    private Rigidbody2D rbPlayer;


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) // A - left
            {
                horizontal = -1f;
            }
            else if (Keyboard.current.dKey.isPressed) // D - right
            {
                horizontal = 1f;
            } 
            else // no input
            {
                horizontal = 0f;
            }
        }

        FlipSprite();

        if (((InputSystem.GetDevice<Keyboard>().spaceKey.isPressed) || (InputSystem.GetDevice<Keyboard>().wKey.isPressed)) && IsGrounded()) // W or Space - jump
        {
            rbPlayer.angularVelocity = 0f; // test
            rbPlayer.linearVelocity = Vector3.zero; // test, better?
            rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); 
        }
    }

    private void FixedUpdate()
    {
        rbPlayer.linearVelocity = new Vector2(horizontal * movementSpeed, rbPlayer.linearVelocity.y);
    }

    #region Sprite & Ground

    private void FlipSprite()
    {
        if (isFacinRight && horizontal < 0f || !isFacinRight && horizontal > 0f)
        {
            isFacinRight = !isFacinRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;


        }
    }

    bool IsGrounded()
    {

        Vector2 rayPos = transform.position - new Vector3(0, 0.5f, 0); 
        Vector2 rayDir = Vector2.down; 

        float rayLength = 0.1f;


        Debug.DrawRay(rayPos, rayDir * rayLength, Color.green);


        RaycastHit2D hit = Physics2D.Raycast(rayPos, rayDir, rayLength, groundLayer); 

   
        return hit.collider != null;
    }

    #endregion

}
