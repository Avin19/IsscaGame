
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
        //left
        if (Random.value > 0.5f)
        {
            Room room = new Room();
            room.location = new Vector2(-1, 0) + startRoom.location;
            room.roomImage = Level._defaultRoomIcon;
            GenerateRoom(room);

        }
        //Right
        if (Random.value > 0.5f)
        {

            Room room = new Room();
            room.location = new Vector2(1, 0) + startRoom.location;
            room.roomImage = Level._defaultRoomIcon;

            GenerateRoom(room);
        }
        //Up
        if (Random.value > 0.5f)
        {
            Room room = new Room();
            room.location = new Vector2(0, 1) + startRoom.location;
            room.roomImage = Level._defaultRoomIcon;

            GenerateRoom(room);

        }
        //Down
        if (Random.value > 0.5f)
        {
            Room room = new Room();
            room.location = new Vector2(0, -1) + startRoom.location;
            room.roomImage = Level._defaultRoomIcon;

            GenerateRoom(room);

        }

    }

    private bool CheckIfRoomExist(Vector2 v)
    {
        return (Level.roooms.Exists(x => x.location == v));
    }
    int failsafe = 0;
    private void GenerateRoom(Room room)
    {

        failsafe++;

        if (failsafe > 50)
        {

            return;
        }
        // Debug.Log(failsafe);
        DrawRoomOnMap(room);
        //left
        if (Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(-1, 0) + room.location;
            newRoom.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(newRoom.location))
            {
                // GenerateRoom(newRoom);
                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Right"))
                { GenerateRoom(newRoom); }
            }
        }
        //Right
        if (Random.value > 0.5f)
        {

            Room newRoom = new Room();
            newRoom.location = new Vector2(1, 0) + room.location;
            newRoom.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(newRoom.location))
            {
                // GenerateRoom(newRoom);
                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Left"))
                { GenerateRoom(newRoom); }
            }
        }
        //Up
        if (Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(0, 1) + room.location;
            newRoom.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(newRoom.location))
            {
                // GenerateRoom(newRoom);
                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Down"))
                { GenerateRoom(newRoom); }
            }
        }
        //Down
        if (Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(0, -1) + room.location;
            newRoom.roomImage = Level._defaultRoomIcon;
            if (!CheckIfRoomExist(newRoom.location))
            {
                // GenerateRoom(newRoom);
                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Up"))
                { GenerateRoom(newRoom); }
            }
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
                    Debug.Log("right");
                    //Check down, left and up
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x - 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y + 1)))
                        return true;
                    break;
                }
            case "Left":
                {
                    Debug.Log("Left");
                    // checks down , right and up
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x + 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y + 1)))
                        return true;
                    break;
                }
            case "Up":
                {
                    Debug.Log("Up");
                    // Check down , left, right
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x - 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x + 1, v.y)))
                        return true;
                    break;
                }
            case "Down":
                {
                    Debug.Log("Down");
                    // check up, left ,right
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y + 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x - 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x + 1, v.y)))
                        return true;
                    break;
                }
        }
        return false;
    }
}
