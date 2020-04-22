using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private float _reloadTimer;
    [SerializeField] private float _reloadCooldown;
    [SerializeField] private Transform _posToSpawnBullet;
    private float _lastShotTime;
    [SerializeField] private int _damage;
    private GameObject _target;
    private Animator _animator;
    private HP _targetHP;


    private void OnEnable()
    {
        _reloadTimer = _reloadCooldown;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!_animator.GetBool("Active"))
            return;
        if (_target != null)
        {
            RotateToTarget();
            if (_reloadTimer > 0) _reloadTimer -= Time.deltaTime;
            if (_reloadTimer < 0) _reloadTimer = 0;
            if (_reloadTimer == 0)
            {
                GameObject bullet = Instantiate(_bulletPrefab, 
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10, gameObject.transform.position.z), Quaternion.identity);
                _reloadTimer = _reloadCooldown;
                Bullet b = bullet.GetComponent<Bullet>();
                b.SetTarget(_target);
                b.SetDamage(_damage);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && _target == null)
        {
            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _target)
            _target = null;
    }
    private void RotateToTarget()
    {
        var deltaRotation = _target.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(deltaRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
}
