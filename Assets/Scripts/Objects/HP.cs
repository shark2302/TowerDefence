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
        Debug.Log(_healthBar);
        _healthBar.SetMaxHealth(_health);
    }

    public void ChangeHp(int Damage)
    {
        _health -= Damage;
        _healthBar.SetHealth(_health);
        if (_health <= 0)
        {
            if (_animator != null)
            {
                _animator.SetTrigger("Death");
                Destroy(gameObject, 2);
            }
            else Destroy(gameObject);
        }
    }

    public int GetHP()
    {
        return _health;
    }
}
