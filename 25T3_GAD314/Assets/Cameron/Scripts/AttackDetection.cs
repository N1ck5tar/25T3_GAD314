using UnityEngine;

public class AttackDetection : MonoBehaviour
{

    public GameObject playerBody;
    public bool isLightAttack; 

    int AttackDamage; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isLightAttack == true)
        {
            AttackDamage = playerBody.gameObject.GetComponent<PlayerAttack>().lightAttackDamage; 
        }
        else
        {
            AttackDamage = playerBody.gameObject.GetComponent<PlayerAttack>().heavyAttackDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(AttackDamage);
            //Debug.Log("gottem");
        }
    }
}
