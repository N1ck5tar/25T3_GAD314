using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class EnemyPlayerDetection : MonoBehaviour
{
    [SerializeField] public PatrolBehavior PB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PB.playerDetected = true;
        Debug.Log("Player spotted " + collision.gameObject.name);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PB.playerDetected = false;
        Debug.Log("Player Lost " + collision.gameObject.name);
        StartCoroutine(PlayerSearch(3f));
    }

    public IEnumerator PlayerSearch(float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
        if (!PB.playerDetected)
        {
           PB.currentState = PatrolBehavior.EnemyState.Patrol;
        }
    }

}
