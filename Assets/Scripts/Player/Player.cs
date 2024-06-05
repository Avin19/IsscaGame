using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public static string _status = "Idle";
    public static bool invincible = false;
    public static GameObject _healthPanel;
    public static Transform transform;
    public static Animator _animator;
    public static float health = 15.5f;
    public static int maxHealth = 20;
    public static GameObject _pfAttack;
    public static GameObject _pfAttackExplosion;
    public static GameObject _pfBomb;
    public static GameObject _pfBombExplosion;
    public static GameObject _playerStaff;
    public static Room _currentRoom;
    public static CharacterController _characterController;
    public static GameObject _diePanel;
    public static Transform _transform;
}
