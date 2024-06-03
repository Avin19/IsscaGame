using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public static string _status = "Idle";
    public static Transform transform;
    public static Animator _animator;
    public static float health = 2.5f;
    public static int maxHealth = 5;
    public static GameObject _pfAttack;
    public static GameObject _pfAttackExplosion;
    public static GameObject _pfBomb;
    public static GameObject _pfBombExplosion;
    public static GameObject _playerStaff;
    public static Room _currentRoom;

    public static CharacterController _characterController;
}
