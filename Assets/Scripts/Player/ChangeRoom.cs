using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private Transform roomParent;
    [SerializeField] private float doorSpawnOffset = 15f;

    private Sprite perviousRoomImage;

    private void Start()
    {
        perviousRoomImage = Level._defaultRoomIcon;
        EnableDoor(Player._currentRoom);
    }

    private void ChangeRoomImage(Room currentRoom, Room nextRoom)
    {
        currentRoom.roomImage.sprite = perviousRoomImage;
        perviousRoomImage = nextRoom.roomImage.sprite;
        nextRoom.roomImage.sprite = Level._currentRoomIcon;
    }
    bool roomchangeTime = false;

  

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(roomchangeTime );
        if (roomchangeTime || hit.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {// Haven't added the floor hit check .
            return;
        }
        else
        {
            roomchangeTime = true;
            
        if (hit.gameObject.name == "LeftDoor")
        {


            //where are we ?
            Vector2 location = Player._currentRoom.location;
            //where are we going  ?

            location = location + new Vector2(-1, 0);

            if (Level.roooms.Exists(x => x.location == location))
            {

                Room r = Level.roooms.First(x => x.location == location);
                roomParent.Find(Player._currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                Transform nextRoom = roomParent.Find(r.roomNumber.ToString());
                nextRoom.gameObject.SetActive(true);
                Player._characterController.enabled = false;
                Player.transform.position = nextRoom.Find("Doors").Find("RightDoor").position + new Vector3(-doorSpawnOffset, 0f, 0f);
                Player._characterController.enabled = true;
                ChangeRoomImage(Player._currentRoom, r);
                Player._currentRoom = r;
                EnableDoor(r);

            }
        }
        if (hit.gameObject.name == "RightDoor")
        {
           Vector2 location = Player._currentRoom.location;
            //where are we going  ?

            location = location + new Vector2(1, 0);
            if (Level.roooms.Exists(x => x.location == location))
            {
                Room r = Level.roooms.First(x => x.location == location);
                roomParent.Find(Player._currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                Transform nextRoom = roomParent.Find(r.roomNumber.ToString());
                nextRoom.gameObject.SetActive(true);
                Player._characterController.enabled = false;
                Player.transform.position = nextRoom.Find("Doors").Find("LeftDoor").position + new Vector3(doorSpawnOffset, 0f, 0f);
                Player._characterController.enabled = true;
                ChangeRoomImage(Player._currentRoom, r);
                Player._currentRoom = r;
                EnableDoor(r);

            }
        }
        if (hit.gameObject.name == "TopDoor")
        {
           Vector2 location = Player._currentRoom.location;
            //where are we going  ?

            location = location + new Vector2(0, 1);
            if (Level.roooms.Exists(x => x.location == location))
            {
                Room r = Level.roooms.First(x => x.location == location);
                roomParent.Find(Player._currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                Transform nextRoom = roomParent.Find(r.roomNumber.ToString());
                nextRoom.gameObject.SetActive(true);
                Player._characterController.enabled = false;
                Player.transform.position = nextRoom.Find("Doors").Find("BottomDoor").position + new Vector3(0f, 0f, doorSpawnOffset);
                Player._characterController.enabled = true;
                ChangeRoomImage(Player._currentRoom, r);
                Player._currentRoom = r;
                EnableDoor(r);

            }
        }
        if (hit.gameObject.name == "BottomDoor")
        {
            Vector2 location = Player._currentRoom.location;
            //where are we going  ?

            location = location + new Vector2(0, -1);
            if (Level.roooms.Exists(x => x.location == location))
            {
                Room r = Level.roooms.First(x => x.location == location);
                roomParent.Find(Player._currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                Transform nextRoom = roomParent.Find(r.roomNumber.ToString());
                nextRoom.gameObject.SetActive(true);
                Player._characterController.enabled = false;
                Player.transform.position = nextRoom.Find("Doors").Find("TopDoor").position + new Vector3(0f, 0f, -doorSpawnOffset);
                Player._characterController.enabled = true;
                ChangeRoomImage(Player._currentRoom, r);
                Player._currentRoom = r;
                EnableDoor(r);

            }
        }
            roomchangeTime =false;
        }

    }

    private void EnableDoor(Room r)
    {
        Transform dRoom = roomParent.Find(r.roomNumber.ToString()).Find("Doors");
        for (int i = 0; i < dRoom.childCount; i++)
        {
            dRoom.GetChild(i).gameObject.SetActive(false);
        }
        // check all the next door to the rooms
        //left
        {
            Vector2 newLocation = r.location + new Vector2(-1, 0);
            if (Level.roooms.Exists(x => x.location == newLocation))
            {
                dRoom.Find("LeftDoor").gameObject.SetActive(true);
            }
        }
        //right
        {
            Vector2 newLocation = r.location + new Vector2(1, 0);
            if (Level.roooms.Exists(x => x.location == newLocation))
            {
                dRoom.Find("RightDoor").gameObject.SetActive(true);
            }
        }
        //up
        {
            Vector2 newLocation = r.location + new Vector2(0, 1);
            if (Level.roooms.Exists(x => x.location == newLocation))
            {
                dRoom.Find("TopDoor").gameObject.SetActive(true);
            }
        }
        //Down
        {
            Vector2 newLocation = r.location + new Vector2(0, -1);
            if (Level.roooms.Exists(x => x.location == newLocation))
            {
                dRoom.Find("BottomDoor").gameObject.SetActive(true);
            }
        }

    }
}
