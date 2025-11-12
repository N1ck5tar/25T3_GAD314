using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int EnemyHP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
    }
}
