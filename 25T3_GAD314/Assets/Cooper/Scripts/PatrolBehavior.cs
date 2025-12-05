using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PatrolBehavior : MonoBehaviour
{

    public enum EnemyState { Idle, Patrol, Chase, Attack }
    public EnemyState currentState; // change this Var into one of states above to affect enemy behaviour
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D enemyRB;
    //private Animator anim;
    private Transform currentPoint;
    public float speed; // affects enemy speed
    public bool playerDetected;
    //public bool stunned = false;
    public PlayerController playerController;// needed for player local transform
    public Transform target;
    public Vector2 moveDirection;
    public SpriteRenderer enemySprite; 

    void Start()
    {
        target = GameObject.Find("- Player -").transform;
        playerDetected = false;
        currentState = EnemyState.Patrol; // Enemies start in Patrol mode
        enemyRB = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        //anim.SetBool("isRunning", true);
        
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle: // Enemy stands still

            break;

            case EnemyState.Patrol: // Enemy moves back and forth between two set points

                Vector2 point = currentPoint.position - transform.position;
                if (currentPoint == pointB.transform)
                {
                    enemyRB.linearVelocity = new Vector2(speed, 0); // point b needs to be on the right
                }
                else
                {
                    enemyRB.linearVelocity = new Vector2(-speed, 0); // point a needs to be on the left
                }

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                {
                    flip();
                    currentPoint = pointA.transform;
                }

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                {
                    flip();
                    currentPoint = pointB.transform;
                }

                if (playerDetected)
                {
                    currentState = EnemyState.Chase;
                }

            break;

            case EnemyState.Chase: // Enemy move towards the player

                if (playerDetected)
                {
                    Vector2 player = target.position - transform.position;
                    moveDirection = player;
                    enemyRB.linearVelocity = new Vector2(moveDirection.x, 0);
                    if (enemyRB.transform.localScale.x > player.x) // makes the enemy face the player when chasing them
                    {
                        Vector2 localScale = enemySprite.transform.localScale;
                        localScale.x = -2.258549f;
                        enemySprite.transform.localScale = localScale;
                    }
                    else if (enemyRB.transform.localScale.x > -player.x)
                    {
                        Vector2 localScale = enemySprite.transform.localScale;
                        localScale.x = 2.258549f;
                        enemySprite.transform.localScale = localScale;
                    }

                    
                }

            break;

        }
  
    }

    public void KnockBack() // Controls knockback to enemies
    {
        currentState = EnemyState.Idle; // So enemy transform is only effected by AddForce in this moment
        Debug.Log("Knocked Back");
        if (playerController.rbPlayer.transform.localScale.x > 0) // Takes into account which direction the player is facing when applying force
        {
            if (enemyRB.transform.localScale.x > 0) // depending on which way the enemy faces different amount of force are applied
            {
                enemyRB.AddForceX(-5, ForceMode2D.Impulse);
            }
            else if (enemyRB.transform.localScale.x < 0)
            {
                enemyRB.AddForceX(-2, ForceMode2D.Impulse);
            }      

        }
        else if (playerController.rbPlayer.transform.localScale.x < 0) 
        {
            if (enemyRB.transform.localScale.x > 0)
            {
                enemyRB.AddForceX(2, ForceMode2D.Impulse);
            }
            else if (enemyRB.transform.localScale.x < 0)
            {
                enemyRB.AddForceX(5, ForceMode2D.Impulse);
            }

        }
        
        StartCoroutine(KnockWait(0.5f));     
    }

    private void flip() // changes sprite direction so it faces the way it is walking
    {
        Vector3 localScale = enemySprite.transform.localScale;
        localScale.x *= -1;
        enemySprite.transform.localScale = localScale;
    }

    private void OnDrawGizmos() // shows where patrol is in scene view
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public IEnumerator KnockWait(float wait) // Makes the enemy wait for a given time so that the knockback can take effect also sets desired state
    {
        //stunned = true;
        yield return new WaitForSecondsRealtime(wait);
        //stunned = false;
        playerDetected = true;
        currentState = EnemyState.Chase;
    }

}
