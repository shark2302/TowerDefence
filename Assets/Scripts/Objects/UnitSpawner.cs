﻿using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{

    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private int _countOfSpawnableUnits;
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Animator _animator;
    private GameObject _mainTower;

    private void OnMouseDown()
    {
        if (_animator.GetBool("Active") && _countOfSpawnableUnits > 0)
        {
            var obj = Instantiate(_prefab, gameObject.transform.position, Quaternion.identity);
            UnitController uc = obj.GetComponent<UnitController>();
            uc.SetSpawners(_spawners);
            uc.SetTower(_mainTower);
            _countOfSpawnableUnits--;
            _text.text = "Осталось рыцарей : " + _countOfSpawnableUnits;
        }
    }

    public void SetSpawners(Spawner[] spawners)
    {
        _spawners = spawners;
    }

    public void SetCount(int count)
    {
        _countOfSpawnableUnits = count;
        _text.text = "Осталось рыцарей : " + _countOfSpawnableUnits;
    }

    public void SetTower(GameObject tower)
    {
        _mainTower = tower;
    }
    
}
