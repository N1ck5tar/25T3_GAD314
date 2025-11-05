using UnityEngine;

public class TestSpike : MonoBehaviour
{
    [SerializeField] private int damageDeal;
    [SerializeField] private PlayerController player;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player")) // is it the player?
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }

            player.ModifyPlayerHealth(damageDeal); // if so, damage

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // is it the player?
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }

            player.ModifyPlayerHealth(damageDeal); // if so, damage

        }
    }

}
