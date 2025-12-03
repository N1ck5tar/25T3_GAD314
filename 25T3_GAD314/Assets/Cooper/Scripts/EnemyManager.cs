using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    public int EnemyHP;
    public Slider HPSlider; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HPSlider.maxValue = EnemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }

        //HPSlider.value = EnemyHP;

    }

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
    }
}
