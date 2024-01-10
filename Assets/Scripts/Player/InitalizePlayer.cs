using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalizePlayer : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject _playerAttackPf;
    [SerializeField] private GameObject _playerAttackExplosionPf;
    [SerializeField]private Transform _transfrom;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject serectRoomExplosion ; 

    private void Awake() {
        _animator = GetComponent<Animator>();
       
    }
    void Start()
    {
        Player._animator =_animator;
        Player._pfAttack = _playerAttackPf;
        Player._pfAttackExplosion = _playerAttackExplosionPf;
        Player.transform = _transfrom;
        Player._characterController = characterController;
        Level.secertRoomExplosion = serectRoomExplosion;
    }

   
}
