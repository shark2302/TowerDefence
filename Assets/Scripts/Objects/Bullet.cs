using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private float _speed = 10f;
    private int _damage;
    private GameObject _target;
    //private float _lifeTime = 3f;
    //private float _timer;


    private void OnEnable()
    {
       // _timer = _lifeTime;
    }
    private void Update()
    {
        /*_timer -= Time.deltaTime;
        if(_timer <= 0f) 
            Destroy(gameObject);
            */
    }
    private void FixedUpdate()
    {
        if (_target == null)
            Destroy(gameObject);
        else
        {
            Vector3 targetPos = new Vector3(_target.transform.position.x, _target.transform.position.y + 7, _target.transform.position.z);
            Vector3 delta = targetPos - transform.position;
            delta.Normalize();
            transform.position = transform.position + (delta  * _speed * Time.deltaTime);
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != _target) 
            Destroy(gameObject);
        var health = other.gameObject.GetComponent<HP>();
        if (health != null)
            health.ChangeHp(_damage);
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
