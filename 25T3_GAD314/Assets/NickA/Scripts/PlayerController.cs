using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // TO-DO list

    // bool deciding if player can move


    private float horizontal; // direction to move left and right
    private bool isFacinRight;
    private Rigidbody2D rbPlayer;


    [Header("Movement")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float movementSpeed;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float downFallingForce;

    [SerializeField] private float maxSpeed;

    [Header("Player Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maximumHealth;
    [SerializeField] private Image HealthBarUI;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

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
            // Debug.Log(horizontal);
        }

        if (((InputSystem.GetDevice<Keyboard>().spaceKey.isPressed) || (InputSystem.GetDevice<Keyboard>().wKey.isPressed)) && IsGrounded()) // W or Space - jump
        {
            // JUMP
            rbPlayer.linearVelocity = Vector3.zero;
            rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); 
        }

        FlipSprite();
        HealthBarUpdate();
        IsPlayerDead();

    }

    private void FixedUpdate()
    {
        rbPlayer.linearVelocity = new Vector2(horizontal * movementSpeed, rbPlayer.linearVelocity.y);

        if (!IsGrounded())
        {
            rbPlayer.AddForce(Vector3.down * downFallingForce, ForceMode2D.Force); // fall faster
        }

        Vector2 velocity = rbPlayer.linearVelocity; 

        if (velocity.magnitude > maxSpeed) // check if over limit
        {
            velocity = velocity.normalized * maxSpeed; 
            rbPlayer.linearVelocity = velocity; // add clamp limit
        }
    }

    #region Sprite & Ground Check

    private void FlipSprite()
    {
        if (isFacinRight && horizontal < 0f || !isFacinRight && horizontal > 0f) // check facing diration
        {

            //Debug.Log("flip");

            //flip
            isFacinRight = !isFacinRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    bool IsGrounded()
    {

        Vector2 rayPos = transform.position - new Vector3(0, 0.5f, 0); // IMPORTANT NOTE - 0.5 is half the cubes length, this will change with a different object as the player
        Vector2 rayDir = Vector2.down; 

        float rayLength = 0.1f;

        //Debug.DrawRay(rayPos, rayDir * rayLength, Color.green);


        RaycastHit2D hit = Physics2D.Raycast(rayPos, rayDir, rayLength, groundLayer); // grab whatever it hit, if it did

   
        return hit.collider != null;
    }

    #endregion

    #region Health

    public void ModifyPlayerHealth(int healthValue)
    {
        currentHealth += healthValue; // Health: + OR - 

        HealthLimitCheck();
    }

    public void HealthLimitCheck() // clamp health
    {
        if (currentHealth > maximumHealth)
        {
            //Debug.Log("over limit");
            currentHealth = maximumHealth;
        }

        IsPlayerDead();

    }

    public void IsPlayerDead()
    {
        if (currentHealth <= 0)
        {
            //Debug.Log("player dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload current scene if died
        }
    }


    public void HealthBarUpdate()
    {
        HealthBarUI.fillAmount = currentHealth / maximumHealth; // 0-1 decimal / 0-100% HP
    }

    #endregion

}
