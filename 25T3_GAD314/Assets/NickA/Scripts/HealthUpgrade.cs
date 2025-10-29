using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float upgradeHealthValueAmount; // 20 each


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Health Upgraded");
            player.maximumHealth += upgradeHealthValueAmount;
            player.ModifyPlayerHealth((int)upgradeHealthValueAmount);
            Destroy(gameObject);
        }
    }

}
