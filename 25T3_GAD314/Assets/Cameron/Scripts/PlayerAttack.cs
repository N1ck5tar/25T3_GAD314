using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public GameObject lightAttackHitbox;
    public GameObject HeavyAttackHitbox;

    public int lightAttackDamage; 
    public int heavyAttackDamage;


    void Start()
    {
        //lightAttackDamage = 5; 
        //heavyAttackDamage = 15;
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current != null)
        {

            if(Keyboard.current.oKey.isPressed) // O - Light Attack
            {
                StartCoroutine(LightAttack());
            }
            else if (Keyboard.current.pKey.isPressed) // P - Heavy Attack
            {
                StartCoroutine(HeavyAttack());
            }
        }

    }

    public IEnumerator LightAttack()
    {
        lightAttackHitbox.SetActive(true);

        yield return new WaitForSecondsRealtime(0.16f);
        lightAttackHitbox.SetActive(false);
    }

    public IEnumerator HeavyAttack()
    {
        HeavyAttackHitbox.SetActive(true);

        yield return new WaitForSecondsRealtime(0.48f);
        HeavyAttackHitbox.SetActive(false);
    }

}
