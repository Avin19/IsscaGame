using System.Collections.Generic;
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
    [SerializeField] private Sprite _secretRoom;

    private void Awake()
    {
        Level._defaultRoomIcon = _emptyRoom;
        Level._bossRoomIcon = _bossRoom;
        Level._treasureRoomIcon = _treasureRoom;
        Level._currentRoomIcon = _currentRoom;
        Level._shopRoomIcon = _shopRoom;
        Level._unexploredIcon = _unexploredRoom;
        Level._secretRoomIcon =_secretRoom;
    }
    void Start()
    {

        //Drawing the start the first room
        Room startRoom = new Room();
        startRoom.location = new Vector2(0, 0);
        startRoom.exploredRoom =true;
        startRoom.reveledRoom =true;
        startRoom.roomSprite = Level._currentRoomIcon;
        startRoom.roomNumber = 0;
        Player._currentRoom = startRoom;

        DrawRoomOnMap(startRoom);
        //left
        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(-1, 0) + startRoom.location;
            room.roomSprite = Level._defaultRoomIcon;
            room.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(room.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(room.location, "Right"))
                {

                    GenerateRoom(room);
                }
            }

        }
        //Right
        if (Random.value > Level._roomGenerationChance)
        {

            Room room = new Room();
            room.location = new Vector2(1, 0) + startRoom.location;
            room.roomSprite = Level._defaultRoomIcon;
            room.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(room.location))
            {
                if (!CheckIfRoomAroundGeneratedRoom(room.location, "Left"))
                {

                    GenerateRoom(room);
                }
            }
        }
        //Up
        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(0, 1) + startRoom.location;
            room.roomSprite = Level._defaultRoomIcon;
            room.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(room.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(room.location, "Down"))
                {

                    GenerateRoom(room);
                }
            }

        }
        //Down
        if (Random.value > Level._roomGenerationChance)
        {
            Room room = new Room();
            room.location = new Vector2(0, -1) + startRoom.location;
            room.roomSprite = Level._defaultRoomIcon;
            room.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(room.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(room.location, "Up"))
                {
                    GenerateRoom(room);
                }
            }

        }
        GenerateBossRoom();
        bool teasure =GenerateSpecialRoom(Level._treasureRoomIcon, 3);
        bool shop = GenerateSpecialRoom(Level._shopRoomIcon, 2);
        bool secret = GenerateSpecialRoom(Level._secretRoomIcon ,4);

        if(!teasure || !shop || !secret)
        {
            RegenerateMap();
        }
        else
        {
            ChangeRoom.RevealRoom(startRoom);
            ChangeRoom.ReDrawRoomRealved();
        }

    }

    private void RegenerateMap()
    {
          for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                Destroy(child.gameObject);
            }
            Level.roooms.Clear();
            Start();


    }

    private bool CheckIfRoomExist(Vector2 v)
    {
        return (Level.roooms.Exists(x => x.location == v));
    }

    private bool GenerateSpecialRoom(Sprite mapIcon, int roomNumber)
    {

        List<Room> shuffedList = new List<Room>(Level.roooms);
        Room specialRoom = new Room();
        specialRoom.roomSprite = mapIcon;
        specialRoom.roomNumber = roomNumber;
        foreach (Room r in shuffedList)
        {
            Vector2 specialRoomLocation = r.location;
            bool foundAvailableLoaction = false;
            if (r.roomNumber < 6) { continue; }


            //left
            if (!CheckIfRoomExist(specialRoomLocation + new Vector2(-1, 0)))
            {
                if (!CheckIfRoomAroundGeneratedRoom(specialRoomLocation + new Vector2(-1, 0), "Right"))
                {
                    specialRoom.location = specialRoomLocation + new Vector2(-1, 0);
                    foundAvailableLoaction = true;
                }

            }
            //right
            else if (!CheckIfRoomExist(specialRoomLocation + new Vector2(1, 0)))
            {
                if (!CheckIfRoomAroundGeneratedRoom(specialRoomLocation + new Vector2(1, 0), "Left"))
                {
                    specialRoom.location = specialRoomLocation + new Vector2(1, 0);
                    foundAvailableLoaction = true;
                }

            }
            //down 
            else if (!CheckIfRoomExist(specialRoomLocation + new Vector2(0, 1)))
            {
                if (!CheckIfRoomAroundGeneratedRoom(specialRoomLocation + new Vector2(0, 1), "Down"))
                {
                    specialRoom.location = specialRoomLocation + new Vector2(0, 1);
                    foundAvailableLoaction = true;
                }

            }
            //up
            else if (!CheckIfRoomExist(specialRoomLocation + new Vector2(0, -1)))
            {
                if (!CheckIfRoomAroundGeneratedRoom(specialRoomLocation + new Vector2(0, -1), "Up"))
                {
                    specialRoom.location = specialRoomLocation + new Vector2(0, -1);
                    foundAvailableLoaction = true;
                }

            }
            if (foundAvailableLoaction)
            {
                DrawRoomOnMap(specialRoom);
                return true;
            }
        }
        return false;

    }
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    private void GenerateRoom(Room room)
    {


        // Debug.Log(failsafe);
        DrawRoomOnMap(room);
        //left
        if (Random.value > Level._roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(-1, 0) + room.location;
            newRoom.roomSprite = Level._defaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(newRoom.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Right"))
                {
                    if (Mathf.Abs(room.location.x) < Level._roomlimit && Mathf.Abs(room.location.y) < Level._roomlimit)
                        GenerateRoom(newRoom);
                }
            }
        }
        //Right
        if (Random.value > Level._roomGenerationChance)
        {

            Room newRoom = new Room();
            newRoom.location = new Vector2(1, 0) + room.location;
            newRoom.roomSprite = Level._defaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(newRoom.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Left"))
                {
                    if (Mathf.Abs(room.location.x) < Level._roomlimit && Mathf.Abs(room.location.y) < Level._roomlimit)

                        GenerateRoom(newRoom);
                }
            }
        }
        //Up
        if (Random.value > Level._roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(0, 1) + room.location;
            newRoom.roomSprite = Level._defaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(newRoom.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Down"))
                {
                    if (Mathf.Abs(room.location.x) < Level._roomlimit && Mathf.Abs(room.location.y) < Level._roomlimit)
                        GenerateRoom(newRoom);
                }
            }
        }
        //Down
        if (Random.value > Level._roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.location = new Vector2(0, -1) + room.location;
            newRoom.roomSprite = Level._defaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
            if (!CheckIfRoomExist(newRoom.location))
            {

                if (!CheckIfRoomAroundGeneratedRoom(newRoom.location, "Up"))
                {
                    if (Mathf.Abs(room.location.x) < Level._roomlimit && Mathf.Abs(room.location.y) < Level._roomlimit)
                        GenerateRoom(newRoom);
                }
            }
        }
    }

    private void DrawRoomOnMap(Room r)
    {
        string roomName = "Map Tile";
        if(r.roomNumber ==1) roomName = "Boss Room";
        if(r.roomNumber== 2 ) roomName ="Shop Room";
        if(r.roomNumber == 3) roomName = "Teasure Room";
        if(r.roomNumber == 4) roomName = "Secert Room";
        GameObject mapTile = new GameObject(roomName);
        Image roomSprite = mapTile.AddComponent<Image>();
        roomSprite.sprite = r.roomSprite;
        r.roomImage = roomSprite;
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
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x - 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y + 1)))
                        return true;
                    break;
                }
            case "Left":
                {

                    // checks down , right and up
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x + 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y + 1)))
                        return true;
                    break;
                }
            case "Up":
                {

                    // Check down , left, right
                    if (Level.roooms.Exists(x => x.location == new Vector2(v.x, v.y - 1)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x - 1, v.y)) ||
                        Level.roooms.Exists(x => x.location == new Vector2(v.x + 1, v.y)))
                        return true;
                    break;
                }
            case "Down":
                {

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           RegenerateMap();
        }
    }

    private void GenerateBossRoom()
    {
        float maxNumberx = 0;
        Vector2 farthestRoom = Vector2.zero;

        foreach (Room r in Level.roooms)
        {
            if (Mathf.Abs(r.location.x) + Mathf.Abs(r.location.y) >= maxNumberx)
            {
                maxNumberx = Mathf.Abs(r.location.x) + Mathf.Abs(r.location.y);
                farthestRoom = r.location;
            }


        }
       
        Room bossRoom = new Room();
        bossRoom.roomSprite = Level._bossRoomIcon;
        bossRoom.roomNumber = 1;

        //left
        if (!CheckIfRoomExist(farthestRoom + new Vector2(-1, 0)))
        {
            if (!CheckIfRoomAroundGeneratedRoom(farthestRoom + new Vector2(-1, 0), "Right"))
            {
                bossRoom.location = farthestRoom + new Vector2(-1, 0);
            }

        }
        //right
        else if (!CheckIfRoomExist(farthestRoom + new Vector2(1, 0)))
        {
            if (!CheckIfRoomAroundGeneratedRoom(farthestRoom + new Vector2(1, 0), "Left"))
            {
                bossRoom.location = farthestRoom + new Vector2(1, 0);
            }

        }
        //down 
        else if (!CheckIfRoomExist(farthestRoom + new Vector2(0, 1)))
        {
            if (!CheckIfRoomAroundGeneratedRoom(farthestRoom + new Vector2(0, 1), "Down"))
            {
                bossRoom.location = farthestRoom + new Vector2(0, 1);
            }

        }
        //up
        else if (!CheckIfRoomExist(farthestRoom + new Vector2(0, -1)))
        {
            if (!CheckIfRoomAroundGeneratedRoom(farthestRoom + new Vector2(0, -1), "Up"))
            {
                bossRoom.location = farthestRoom + new Vector2(0, -1);
            }

        }
        DrawRoomOnMap(bossRoom);
        Debug.Log(bossRoom.location);
    }


    private int RandomRoomNumber()
    {
        return Random.Range(6,GameObject.Find("Rooms").transform.childCount);
    }

}
