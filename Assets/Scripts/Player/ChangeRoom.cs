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
        Debug.Log(roomchangeTime);
        if (roomchangeTime || hit.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {// Haven't added the floor hit check .
            return;
        }
        else
        {
            roomchangeTime = true;

            if (hit.gameObject.name == "LeftDoor")
            {


                CheckRoom(new Vector2(-1, 0), "RightDoor", new Vector3(-doorSpawnOffset, 0f, 0f));
            }
            if (hit.gameObject.name == "RightDoor")
            {
                CheckRoom(new Vector2(1, 0), "LeftDoor", new Vector3(doorSpawnOffset, 0f, 0f));
            }
            if (hit.gameObject.name == "TopDoor")
            {
                CheckRoom(new Vector2(0, 1), "BottomDoor", new Vector3(0f, 0f, doorSpawnOffset));
            }
            if (hit.gameObject.name == "BottomDoor")
            {
                CheckRoom(new Vector2(0, -1), "TopDoor", new Vector3(0f, 0f, -doorSpawnOffset));
            }
            roomchangeTime = false;
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
        SetDoorToTheRoom(dRoom, r, new Vector2(-1, 0), "LeftDoor");
        //right
        SetDoorToTheRoom(dRoom, r, new Vector2(1, 0), "RightDoor");
        //up

        SetDoorToTheRoom(dRoom, r, new Vector2(0, 1), "TopDoor");
        //Down
        SetDoorToTheRoom(dRoom, r, new Vector2(0, -1), "BottomDoor");


    }
    private void SetDoorToTheRoom(Transform dRoom, Room r, Vector2 location, string name)
    {
        Vector2 newLocation = r.location + location;
        if (Level.roooms.Exists(x => x.location == newLocation))
        {
            dRoom.Find(name).gameObject.SetActive(true);
        }

    }
    private void CheckRoom(Vector2 newLocation, string direction, Vector3 roomOffset)
    {
        Vector2 location = Player._currentRoom.location;
        //where are we going  ?

        location = location + newLocation;
        if (Level.roooms.Exists(x => x.location == location))
        {
            Room r = Level.roooms.First(x => x.location == location);
            roomParent.Find(Player._currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
            Transform nextRoom = roomParent.Find(r.roomNumber.ToString());
            nextRoom.gameObject.SetActive(true);
            Player._characterController.enabled = false;
            Player.transform.position = nextRoom.Find("Doors").Find(direction).position + roomOffset;
            Player._characterController.enabled = true;
            ChangeRoomImage(Player._currentRoom, r);
            Player._currentRoom = r;
            EnableDoor(r);

            RevealRoom(r);
            ReDrawRoomRealved();


        }
    }
    public static void RevealRoom(Room r)
    {
        foreach (Room room in Level.roooms)
        {
            //left
            if (room.location == r.location + new Vector2(-1, 0))
            {
                room.reveledRoom = true;
            }

            //right
            if (room.location == r.location + new Vector2(1, 0))
            {
                room.reveledRoom = true;
            }

            //Up
            if (room.location == r.location + new Vector2(0, 1))
            {
                room.reveledRoom = true;
            }

            //Down
            if (room.location == r.location + new Vector2(0, -1))
            {
                room.reveledRoom = true;
            }

        }
    }
    public static void ReDrawRoomRealved()
    {
        foreach (Room room in Level.roooms)
        {
            if (!room.reveledRoom && !room.exploredRoom)
            {
                room.roomImage.color = new Color(1, 1, 1, 0);
            }
            if (room.reveledRoom && !room.exploredRoom && room.roomNumber > 5)
            {
                room.roomImage.sprite = Level._unexploredIcon;
            }
            if (room.exploredRoom && room.roomNumber > 5)
            {
                room.roomImage.sprite = Level._defaultRoomIcon;

            }
            if (room.exploredRoom || room.reveledRoom)
            {
                room.roomImage.color = new Color(1, 1, 1, 1);

            }
            Player._currentRoom.roomImage.sprite = Level._currentRoomIcon;
        }
    }
}
