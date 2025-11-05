using UnityEngine;
using System.Collections;

public class PatrolBehavior : MonoBehaviour
{
    //Lines that are commented out are a attempt to have patrol work within a coroutine so other animations could be played like attack


    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    //private bool attack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        //anim.SetBool("attackNow", false);
        //StartCoroutine(Patrol());
        rb.AddForce(Vector2.up*2000);
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0); // point b needs to be on the right
        }
        else
        {
            rb.linearVelocity = new Vector2 (-speed, 0); // point a needs to be on the left
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
        
    }

    private void flip() // changes sprite direction so it faces the way it is walking
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos() // shows where patrol is in scene view
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
    /*
    bool waitCon()
    {
        return attack = true;
    }

    private IEnumerator Patrol()
    {
        
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0); // point b needs to be on the right
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0); // point a needs to be on the left
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
        yield return new WaitWhile(attack);
        
    } */
    
}
