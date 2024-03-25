using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public static class Level
{
    public static float _height = 500;
    public static float _width = 500;
    public static float _padding = 0.01f;
    public static float _scaleMap = 1f;
    public static float _scaleIcon = 0.06f;
    public static float _roomGenerationChance = 0.5f;
    public static Sprite _treasureRoomIcon;
    public static Sprite _bossRoomIcon;
    public static Sprite _shopRoomIcon;
    public static Sprite _defaultRoomIcon;
    public static Sprite _currentRoomIcon;
    public static Sprite _unexploredIcon;
    public static Sprite _secretRoomIcon;

    public static GameObject secertRoomExplosion;
    public static GameObject secertRoomDoor;

    public static GameObject xMark;

    public static int _roomlimit = 6;
    public static List<Room> roooms = new List<Room>();
    public static float roomChangeTime = 0.5f;




}

public class Room
{
    public int roomNumber = 0;
    public Vector2 location;

    public Sprite roomSprite;
    public Image roomImage;
    public bool reveledRoom;

    public bool exploredRoom;




}
