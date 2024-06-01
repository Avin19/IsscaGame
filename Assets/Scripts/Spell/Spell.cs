using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

  private void OnCollisionEnter(Collision other)
  {
    GameObject.Destroy(Instantiate(Player._pfAttackExplosion, transform.position, Quaternion.identity), 3f);

    if (other.collider.name.Contains("DrawX"))
    {
      Level.secertRoomExplosed = true;
      GameObject.Destroy(Instantiate(Level.secertRoomExplosion, other.transform.position, Quaternion.identity), 5f);
      Transform T = GameObject.Find("Rooms").transform.Find(Player._currentRoom.roomNumber.ToString()).Find("Doors").transform;

      GameObject scrcetDoor = Instantiate(Level.secertRoomDoor, other.transform.position, other.transform.rotation, T);
      string direction = (other.transform.position.x > 10f) ? "RightDoor" : (other.transform.position.x < -10f) ? "LeftDoor" :
                        (other.transform.position.y > 10f) ? "TopDoor" : (other.transform.position.y < -10f) ? "BottomDoor" : "Center";

      scrcetDoor.name = direction;
      GameObject.Destroy(other.gameObject);
    }
    GameObject.Destroy(gameObject);

  }
}
