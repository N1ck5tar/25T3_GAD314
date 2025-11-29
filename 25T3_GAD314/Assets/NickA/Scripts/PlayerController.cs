using System.Collections;
using Unity.VisualScripting;
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
    public Rigidbody2D rbPlayer;

    [SerializeField] private Transform respawnPoint; // object transform to determine where player respawns on death

    [Header("Movement")]
    [SerializeField] private float jumpPower; // press force
    [SerializeField] private float jumpHoldPower; // holding force (continuous)
    [SerializeField] private float movementSpeed;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float downFallingForce;

    [SerializeField] private float maxSpeed; // max moving speed for the player

    public bool canPlayerMove = true; // false = no, true = yes

    [SerializeField] private bool isJumping = false;
    [SerializeField] private float maxJumpTime; // max jump hold time
    [SerializeField] private float jumpTimer; // how long player has held jump for

    [Header("Player Health")]
    [SerializeField] private float currentHealth;
    public float maximumHealth; // max health the player can have - increased from health upgrades
    [SerializeField] private Image HealthBarUI; // UI to specifically show the player's health

    private Animator anim; 


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

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
                // A or D || Left arrow or Right arrow
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) // A - left
                {
                    horizontal = -1f;
                    anim.SetBool("IsRunning", true); // controls running animation when moving left
                }
                else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) // D - right
                {
                    horizontal = 1f;
                    anim.SetBool("IsRunning", true); // controls running animation when moving right
                }
                else // no input
                {
                    horizontal = 0f;
                    anim.SetBool("IsRunning", false); // turns off running animation when stationary
                }
                // Debug.Log(horizontal);
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

        #region Jump Press & Release
        // 'spacebar' 'w' 'up arrow' 'z'
        if (((Keyboard.current.spaceKey.wasPressedThisFrame) && IsGrounded() || (Keyboard.current.wKey.wasPressedThisFrame) && IsGrounded() || (Keyboard.current.upArrowKey.wasPressedThisFrame) && IsGrounded() || (Keyboard.current.zKey.wasPressedThisFrame)) && IsGrounded()) // W or Space - jump
        {
            // STARTING JUMP
            //Debug.Log("jump press");

            isJumping = true;
            jumpTimer = 0f; // reset time

            rbPlayer.linearVelocity = Vector3.zero;
            rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame || Keyboard.current.wKey.wasReleasedThisFrame || Keyboard.current.upArrowKey.wasReleasedThisFrame || Keyboard.current.zKey.wasReleasedThisFrame)
        {
            // STOP JUMP
            //Debug.Log("stop jump");

            isJumping = false;
        }
        #endregion
    }

    private void FixedUpdate()
    {

        #region Jump Hold 
        if (isJumping && Keyboard.current.spaceKey.isPressed || isJumping && Keyboard.current.wKey.isPressed || isJumping && Keyboard.current.upArrowKey.isPressed || isJumping && Keyboard.current.zKey.isPressed)
        {
            if (jumpTimer < maxJumpTime)
            {
                //Debug.Log("jump hold");

                jumpTimer += Time.deltaTime;
                rbPlayer.AddForce(Vector2.up * jumpHoldPower, ForceMode2D.Impulse);
            }
            else
            {
                //Debug.Log("out of jump hold");
            }
        }
        #endregion

        if (canPlayerMove == true)
        {

            rbPlayer.linearVelocity = new Vector2(horizontal * movementSpeed, rbPlayer.linearVelocity.y);

            Vector2 velocity = rbPlayer.linearVelocity;

            if (velocity.magnitude > maxSpeed) // check if over limit
            {
                velocity = velocity.normalized * maxSpeed;
                rbPlayer.linearVelocity = velocity; // add clamp limit
            }
        }

        if (!IsGrounded())
        {
            rbPlayer.AddForce(Vector3.down * downFallingForce, ForceMode2D.Force); // fall faster
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

    public void PlayerKnockback() // used to push the player in the opposite facing direction
    {
        //Debug.Log("PlayerKnockback");
        float pushAmp = 5f;

        if (isFacinRight)
        {
           // Debug.Log("left " + pushAmp + (Vector2.left * pushAmp));
            StartCoroutine(StopPlayerMovement(0.4f));
            rbPlayer.linearVelocityY = rbPlayer.linearVelocityY * 0.5f;
            rbPlayer.linearVelocityX = 0f;
            rbPlayer.AddForce((Vector2.left * pushAmp) + new Vector2(0, 1), ForceMode2D.Impulse);
        }
        else
        {
            //Debug.Log("right " + pushAmp);
            StartCoroutine(StopPlayerMovement(0.4f));
            rbPlayer.linearVelocityY = rbPlayer.linearVelocityY * 0.5f;
            rbPlayer.linearVelocityX = 0f;
            rbPlayer.AddForce((Vector2.right * pushAmp) + new Vector2(0, 1), ForceMode2D.Impulse);
        }

    }

    public IEnumerator StopPlayerMovement(float effectTime)
    {
        //Debug.Log("player stopped");
        canPlayerMove = false;

        yield return new WaitForSeconds(effectTime);

        //Debug.Log("player resumed");
        canPlayerMove = true;
    }

    #endregion

    #region Health

    public void ModifyPlayerHealth(int healthValue)
    {
        currentHealth += healthValue; // Health: + OR - 

        if (healthValue < 0)
        {
            PlayerKnockback();
        }

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
