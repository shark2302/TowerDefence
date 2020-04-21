using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int _health;
    // Start is called before the first frame update
    private Animator _animator;
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ChangeHp(int Damage)
    {
        _health -= Damage;
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
