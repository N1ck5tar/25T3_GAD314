using UnityEngine;

public class TestSpike : MonoBehaviour
{
    [SerializeField] private int damageDeal;
    [SerializeField] private PlayerController player;


    // PURELY TEST - checking if the modify player health


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }

            player.ModifyPlayerHealth(damageDeal);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }

            player.ModifyPlayerHealth(damageDeal);

        }
    }

}
