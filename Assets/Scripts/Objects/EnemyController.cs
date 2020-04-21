using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    private HP _targetHP;
    private float _reloadCooldown = 1f;
    private float _reloadTimer;
    private bool _isMoving;
    private GameObject _tower;
    private bool _endGame;

    private HP _hp;
    // Start is called before the first frame update
    void OnEnable()
    {
        _animator.SetTrigger("RunTrigger");
        _isMoving = true;
       // _tower = GameObject.Find("MainTower").gameObject;
        _target = _tower;
        _reloadTimer = _reloadCooldown;
        _hp = GetComponent<HP>();
    }

    private void FixedUpdate()
    {
        if (_endGame)
            return;
        if (_tower == null)
        {
            _animator.SetTrigger("WinAnimation");
            Destroy(gameObject, 1.5f);
            _endGame = true;
        }
        else if (_target == null && !_isMoving)
        {
            _target = _tower;
            _isMoving = true;
            _animator.SetTrigger("RunTrigger");
        }
        else if (Vector3.Distance(transform.position, _target.transform.position) <= _attackRange && _isMoving)
        {
            _isMoving = false;
            _animator.SetTrigger("AttackTrigger");
            _targetHP = _target.GetComponent<HP>();
        }
        else if (_isMoving)
        {
            MoveToTarget();
            RotateToTarget();
        }

        else Attack();
      
    }
    private void OnCollisionEnter(Collision other)
    {    
        if(other.gameObject.tag == "Enemy")
        {
           AvoidCollisionWithAnotherMob(other.gameObject);
        }
        if (other.gameObject.tag == "Fence" || other.gameObject.tag == "Canon" || other.gameObject.tag == "Unit")
        {
            _isMoving = false;
            _target = other.gameObject;
            _animator.SetTrigger("AttackTrigger");
            _targetHP = other.gameObject.GetComponent<HP>();
        }
        
    }
    
    private void Attack()
    {
        RotateToTarget();
        if (_targetHP != null && _hp.GetHP() > 0)
        {
            if (_reloadTimer > 0) _reloadTimer -= Time.deltaTime;
            if (_reloadTimer < 0) _reloadTimer = 0;
            if (_reloadTimer == 0)
            {
                _targetHP.ChangeHp(_damage);
                _reloadTimer = _reloadCooldown;
            }
        }
    }

    private void RotateToTarget()
    {
        var deltaRotation = _target.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(deltaRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void MoveToTarget()
    {
        Vector3 delta = _target.transform.position - transform.position;
        delta.Normalize();
        float moveSpeed = _speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }

    private void AvoidCollisionWithAnotherMob(GameObject other)
    {
        if(other.transform.position.x > transform.position.x) 
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);
        else 
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);
    }

    public void SetTower(GameObject tower)
    {
        _tower = tower;
        _target = _tower;
    }
}