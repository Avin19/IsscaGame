using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    public static string _status = "Idle";
    public static Transform transform;
    public static Animator _animator;
    public static GameObject _pfAttack;
    public static GameObject _pfAttackExplosion;
    public static Room _currentRoom;

    public static CharacterController _characterController;
}
