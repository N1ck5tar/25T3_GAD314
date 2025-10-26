using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public GameObject lightAttackHitbox;
    public GameObject HeavyAttackHitbox;

    public GameObject HeavyAttackSwivel; 

    public int lightAttackDamage; 
    public int heavyAttackDamage;

    bool attackOnCooldown; 

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

            if(Keyboard.current.oKey.isPressed && !attackOnCooldown) // O - Light Attack
            {
                StartCoroutine(LightAttack());
            }
            else if (Keyboard.current.pKey.isPressed && !attackOnCooldown) // P - Heavy Attack
            {
                StartCoroutine(HeavyAttack());
            }
        }

    }

    public IEnumerator LightAttack()
    {
        StartCoroutine(AttackCooldown(0.32f));
        
        yield return new WaitForSecondsRealtime(0.16f);
        lightAttackHitbox.SetActive(true);

        yield return new WaitForSecondsRealtime(0.16f);
        lightAttackHitbox.SetActive(false);
    }

    public IEnumerator HeavyAttack()
    {
        StartCoroutine(AttackCooldown(0.90f));
        HeavyAttackSwivel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -115f);

        yield return new WaitForSecondsRealtime(0.16f);
        HeavyAttackHitbox.SetActive(true);

        for(float r = -115; r < 20; r += 5)
        {
            HeavyAttackSwivel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, r);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        //yield return new WaitForSecondsRealtime(0.48f);
        HeavyAttackHitbox.SetActive(false);
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        attackOnCooldown = true; 

        yield return new WaitForSecondsRealtime(cooldown);
        attackOnCooldown = false; 
    }

}
