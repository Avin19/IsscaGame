using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void FinishAttacking()
    {
        Player._status = "Idle";
        

    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Player._status =="Idle")
        {
            Player._status = "Attacking";
            string attackPatternNumber = Random.Range(1,5).ToString();
            Player._animator.Play("Attack"+attackPatternNumber);
        }
    }
}
