using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
   
   private void OnCollisionEnter(Collision other) {
        GameObject.Destroy(Instantiate(Player._pfAttackExplosion , transform.position,Quaternion.identity),3f);     
      Debug.Log(other.collider.name);
        if(other.collider.name.Contains("DrawX"))
        {

          GameObject.Destroy(Instantiate(Level.secertRoomExplosion, other.transform.position,Quaternion.identity),5f);
          GameObject.Destroy(other.gameObject);
        }
        GameObject.Destroy(gameObject);

   }
}
