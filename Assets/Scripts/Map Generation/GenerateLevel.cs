
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private Sprite _currentRoom;
    [SerializeField] private Sprite _bossRoom;

    [SerializeField] private Sprite _emptyRoom;

    [SerializeField] private Sprite _shopRoom;
    [SerializeField] private Sprite _treasureRoom;

    [SerializeField] private Sprite _unexploredRoom;
    void Start()
    {
        Level._defaultRoomIcon = _emptyRoom;
        Level._bossRoomIcon = _bossRoom;
        Level._treasureRoomIcon = _treasureRoom;
        Level._currentRoomIcon = _currentRoom;
        Level._shopRoomIcon = _shopRoom;
        Level._unexploredIcon = _unexploredRoom;
        //Drawing the start the first room
        Room startRoom = new Room();
        startRoom.location = new Vector2(0, 0);
        startRoom.roomImage = Level._currentRoomIcon;

        DrawRoomOnMap(startRoom);

    }
    private bool CheckIfRoomExist(Vector2 location)
    {
        return (Level.roooms.Exists(x => x.location == location));
    }
    int failsafe = 0;
    private void GenerateRoom(Room roomR)
    {

        failsafe++;
        DrawRoomOnMap(roomR);
        if (failsafe > 50)
        {

            return;
        }

        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(-1, 0) + room.location;
            room.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(room.location))
            { GenerateRoom(room); }
        }        //Right
        if (Random.value > Level._roomGenerationChance)
        {

            Room room = new Room();
            room.location = new Vector2(1, 0) + room.location;
            room.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(room.location))
            {
                if (CheckIfRoomAroundGeneratedRoom(room.location, "Right"))
                    GenerateRoom(room);
            }
        }
        //Up
        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(0, 1) + room.location;
            room.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(room.location))
            { GenerateRoom(room); }
        }
        //Down
        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(0, -1) + room.location;
            room.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(room.location))
            { GenerateRoom(room); }
        }
    }

    private void DrawRoomOnMap(Room r)
    {
        GameObject mapTile = new GameObject("Map Tile");
        Image roomSprite = mapTile.AddComponent<Image>();
        roomSprite.sprite = r.roomImage;
        RectTransform roomRectTransfomr = mapTile.GetComponent<RectTransform>();
        roomRectTransfomr.sizeDelta = new Vector2(Level._height, Level._width) * Level._scaleIcon;
        roomRectTransfomr.position = r.location * (Level._scaleIcon * Level._height * Level._scaleMap + (Level._padding * Level._height * Level._scaleMap));
        roomSprite.transform.SetParent(transform, false);
        Level.roooms.Add(r);
    }

    private bool CheckIfRoomAroundGeneratedRoom(Vector2 v, string direction)
    {
        switch (direction)
        {
            case "Right":
                { 
                    //Check down, left and up
                    if(Level.roooms.Exists(x=> x.location == new Vector2(v.x-1,v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x,v.y-1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x-1,v.y+1)))
                        return true;
                    break; 
                }
                case "Left":
                {
                    // checks down , right and up
                    if(Level.roooms.Exists(x=>x.location == new Vector2(v.x+1,v.y))||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x ,v.y-1))||
                        Level.roooms.Exists(x => x.location ==new Vector2(v.x , v.y+1)))
                        return true;
                    break;
                }
                case "Up":
                {
                    // Check down , left, right
                    if(Level.roooms.Exists( x => x.location ==new Vector2(v.x,v.y-1))||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x-1, v.y))||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x+1 , v.y)))
                     return true;
                    break;
                }
                case "Down":
                {
                    // check up, left ,right
                    if(Level.roooms.Exists( x => x.location ==new Vector2(v.x,v.y+1))||
                        Level.roooms.Exists(x => x.location ==new Vector2(v.x-1,v.y))||
                        Level.roooms.Exists(x => x.location ==new Vector2(v.x+1 , v.y)))
                        return true;
                    break;
                }
        }
        return false;
    }
}
