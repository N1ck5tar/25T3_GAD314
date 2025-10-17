using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // to do - comment
    // fix movement



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
        
        if ((InputSystem.GetDevice<Keyboard>().spaceKey.isPressed) && IsGrounded()) 
        {
            rbPlayer.angularVelocity = 0f; // test
            rbPlayer.linearVelocity = Vector3.zero; // test, better?
            rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); 
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

}
