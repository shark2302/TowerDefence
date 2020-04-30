using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
 

[CreateAssetMenu(menuName = "Level config")]
public class LevelConfig : ScriptableObject
{

    [SerializeField]private int Money;
    
    [SerializeField] private bool CanSetCanon;
    [SerializeField] private GameObject CanonPrefab;
    [SerializeField] private int CanonCost;
    
    [SerializeField] private bool CanSetFence;
    [SerializeField] private GameObject FencePrefab;
    [SerializeField] private int FenceCost;

    [SerializeField] private bool CanSetTavern;
    [SerializeField] private GameObject TavernPrefab;
    [SerializeField] private int TavernCost;
    [SerializeField] private int UnitsInTavern;
    
    [SerializeField] private bool CanSetSuperCanon;
    [SerializeField] private GameObject CanonSuperPrefab;
    [SerializeField] private int CanonSuperCost;


    public int Money1
    {
        get => Money;
        set => Money = value;
    }

    public bool CanSetCanon1
    {
        get => CanSetCanon;
        set => CanSetCanon = value;
    }

    public GameObject CanonPrefab1
    {
        get => CanonPrefab;
        set => CanonPrefab = value;
    }

    public int CanonCost1
    {
        get => CanonCost;
        set => CanonCost = value;
    }

    public bool CanSetFence1
    {
        get => CanSetFence;
        set => CanSetFence = value;
    }

    public GameObject FencePrefab1
    {
        get => FencePrefab;
        set => FencePrefab = value;
    }

    public int FenceCost1
    {
        get => FenceCost;
        set => FenceCost = value;
    }

    public bool CanSetTavern1
    {
        get => CanSetTavern;
        set => CanSetTavern = value;
    }

    public GameObject TavernPrefab1
    {
        get => TavernPrefab;
        set => TavernPrefab = value;
    }

    public int TavernCost1
    {
        get => TavernCost;
        set => TavernCost = value;
    }

    public int UnitsInTavern1
    {
        get => UnitsInTavern;
        set => UnitsInTavern = value;
    }

    public bool CanSetSuperCanon1
    {
        get => CanSetSuperCanon;
        set => CanSetSuperCanon = value;
    }

    public GameObject CanonSuperPrefab1
    {
        get => CanonSuperPrefab;
        set => CanonSuperPrefab = value;
    }

    public int CanonSuperCost1
    {
        get => CanonSuperCost;
        set => CanonSuperCost = value;
    }
}