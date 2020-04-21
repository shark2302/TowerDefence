using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
   [SerializeField] private Spawner[] _spawners;
   private GameObject _target;
   private NavMeshAgent _navAgent;
   private float _attackRange = 8;
   [SerializeField] private float _speed = 5f;
   private Animator _animator;
   private bool _isMoving;
   private HP _targetHP;
   [SerializeField] private int _damage;
   private float _reloadCooldown = 1f;
   private float _reloadTimer;
   private HP _hp;
   private void Awake()
   {
      _navAgent = GetComponent<NavMeshAgent>();
      _navAgent.updateRotation = false;
      _navAgent.speed = _speed;
      _animator = GetComponent<Animator>();
      _isMoving = true;
      _reloadTimer = _reloadCooldown;
      _hp = GetComponent<HP>();
   }
   private void Update()
   {
      if (_target == null)
      {
         _target = FindNearestEnemy();
         if(_target == null)
            _animator.SetTrigger("WinTrigger");
      }
      else
      {
         if (Vector3.Distance(transform.position, _target.transform.position) <= _attackRange && _isMoving)
         {
            _animator.SetTrigger("AttackTrigger");
            _navAgent.SetDestination(transform.position);
            _targetHP = _target.GetComponent<HP>();
            _isMoving = false;
         }
         else if(_isMoving)
         {
            RotateToTarget();
            _animator.SetTrigger("RunTrigger");
            _navAgent.SetDestination(_target.transform.position);
            _isMoving = true;
         }
         else Attack();
       
      }
      
   }

   private GameObject FindNearestEnemy()
   {
      List<GameObject> enemies = CombineSpawnedEnemy();
      int distance = 10000000;
      GameObject nearestEnemy = null;
      foreach (var enemy in enemies)
      {
         if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) < distance)
            nearestEnemy = enemy;
      }

      return nearestEnemy;
   }
   
   private void RotateToTarget()
   {
      var deltaRotation = _target.transform.position - transform.position;
      var rotation = Quaternion.LookRotation(deltaRotation);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
   }

   private List<GameObject> CombineSpawnedEnemy()
   {
      List<GameObject> res = new List<GameObject>();
      foreach (var spawner in _spawners)
      {
         if(spawner != null)
            res.AddRange(spawner.GetEnemyList());
      }

      return res;
   }
   private void Attack()
   {
      RotateToTarget();
      if (_targetHP != null && _hp.GetHP() > 0 && _targetHP.GetHP() > 0)
      {
         if (_reloadTimer > 0) _reloadTimer -= Time.deltaTime;
         if (_reloadTimer < 0) _reloadTimer = 0;
         if (_reloadTimer == 0)
         {
            _targetHP.ChangeHp(_damage);
            _reloadTimer = _reloadCooldown;
            if (_targetHP.GetHP() <= 0)
            {
               _target = null;
               _isMoving = true;
            }
              
         }
      }
   }

   public void SetSpawners(Spawner[] spawners)
   {
      _spawners = spawners;
   }
   
}
