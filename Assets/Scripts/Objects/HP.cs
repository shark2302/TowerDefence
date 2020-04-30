using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int _health;
    
    [SerializeField] private HealthBar _healthBar;
    private Animator _animator;
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _healthBar.SetMaxHealth(_health);
    }

    public void ChangeHp(int Damage)
    {
        _health -= Damage;
        _healthBar.SetHealth(_health);
        if (_health <= 0)
        {
             if (_animator != null && !(gameObject.tag == "Fence"|| gameObject.tag == "Canon"))
            {
                _animator.SetTrigger("Death");
                Destroy(gameObject, 1.3f);
            }
            else 
                Destroy(gameObject);
        }
    }

    public int GetHP()
    {
        return _health;
    }
}
