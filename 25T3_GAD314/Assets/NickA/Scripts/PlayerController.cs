using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // TO-DO list

    // bool deciding if player can move


    public float horizontal; // direction to move left and right
    public bool isFacinRight;
    public bool canFlip;
    private Rigidbody2D rbPlayer;

    [SerializeField] private Transform respawnPoint; // object transform to determine where player respawns on death

    [Header("Movement")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float movementSpeed;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float downFallingForce;

    [SerializeField] private float maxSpeed;

    public bool canPlayerMove = true; // false = no, true = yes

    [Header("Player Health")]
    [SerializeField] private float currentHealth;
    public float maximumHealth; // max health the player can have - increased from health upgrades
    [SerializeField] private Image HealthBarUI; // UI to specifically show the player's health


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();

        if (rbPlayer == null)
        {
            Debug.Log("Player Rigidbody could not be found");
        }
    }

    void Update()
    {
        if (canPlayerMove == true)
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

            if (((Keyboard.current.spaceKey.wasPressedThisFrame) || (Keyboard.current.wKey.wasPressedThisFrame)) && IsGrounded()) // W or Space - jump
            {
                // JUMP
                rbPlayer.linearVelocity = Vector3.zero;
                rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }       

        }

        FlipSprite();

        if (HealthBarUI != null)
        {
            HealthBarUpdate();
            IsPlayerDead();
        }

        else
        {
            Debug.Log("No Health bar UI connected");
            return;
        }

    }

    private void FixedUpdate()
    {
        if (canPlayerMove == true)
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
        
        else
        {
            rbPlayer.linearVelocity = new Vector2(0, 0);
        }

    }

    #region Sprite & Movement Stuff

    private void FlipSprite()
    {
        if (canFlip)
        {
            if (isFacinRight && horizontal < 0f || !isFacinRight && horizontal > 0f) // check facing diration
            {

                //Debug.Log("flip");

                //flip the sprite
                isFacinRight = !isFacinRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }

    public bool IsGrounded()
    {

        Vector2 playerBase = transform.position - new Vector3(0, 0.5f, 0);

        float offset = 0.295f; // player width - WILL CHANGE WITH NEW PLAYER SIZE
        Vector2[] rays = {playerBase + new Vector2(-offset, 0), playerBase, playerBase + new Vector2(offset, 0)}; // manually add rays in the arrary

        Vector2 rayDir = Vector2.down; // aim down
        float rayLength = 0.1f;

        foreach (Vector2 ray in rays)
        {
            //Debug.DrawRay(ray, rayDir * rayLength, Color.green); // visual

            RaycastHit2D hit = Physics2D.Raycast(ray, rayDir, rayLength, groundLayer); // shoot ray
            if (hit.collider != null)
            {
                return true; // grounded
            }
        }

        return false; // not grounded
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
            if (respawnPoint != null)
            {
                gameObject.transform.position = respawnPoint.position; // back to start
            }

            currentHealth = maximumHealth; // reset health to their max
            rbPlayer.linearVelocity = Vector3.zero; // stop movement
        }
    }


    public void HealthBarUpdate()
    {
        HealthBarUI.fillAmount = currentHealth / maximumHealth; // 0-1 decimal / 0-100% HP
    }

    #endregion

}
