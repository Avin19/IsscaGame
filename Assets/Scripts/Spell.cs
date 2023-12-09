using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
   private void OnCollisionEnter(Collision other) {
        GameObject.Destroy(Instantiate(Player._pfAttackExplosion , transform.position,Quaternion.identity),3f);     
        GameObject.Destroy(gameObject);

   }
}
