using System;
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
    private bool _hasInstalled;

    private void OnEnable()
    {
        _hasInstalled = false;
    }

    private void OnMouseDown()
    {
        if (_hasInstalled)
        {
            var obj = Instantiate(_prefab, gameObject.transform.position, Quaternion.identity);
            obj.GetComponent<UnitController>().SetSpawners(_spawners);
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
    }

    public void SetHasInstall(bool b)
    {
        _hasInstalled = b;
        if (b)
            _text.text = "Осталось рыцарей : " + _countOfSpawnableUnits;
    }
}
