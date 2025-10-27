using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int WallHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WallHP <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        WallHP -= damage;
    }
}
