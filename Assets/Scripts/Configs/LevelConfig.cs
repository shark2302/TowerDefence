using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
 

[CreateAssetMenu(menuName = "Level config")]
public class LevelConfig : ScriptableObject
{

    public int Money;
    
    public bool CanSetCanon;
    public GameObject CanonPrefab;
    public int CanonCost;
    
    public bool CanSetFence;
    public GameObject FencePrefab;
    public int FenceCost;

    public bool CanSetTavern;
    public GameObject TavernPrefab;
    public int TavernCost;
    public int UnitsInTavern;
    
    public bool CanSetSuperCanon;
    public GameObject CanonSuperPrefab;
    public int CanonSuperCost;

    public int GetMoneyForLevel()
    {
        return Money;
    }

    public bool IsAvailibleToSetCanon()
    {
        return CanSetCanon;
    }


    public bool IsAvailibleToSetFence()
    {
        return CanSetFence;
    }

    public GameObject GetCanonPrefab()
    {
        return CanonPrefab;
    }

    public GameObject GetFencePrefab()
    {
        return FencePrefab;
    }

    public int GetCanonCost()
    {
        return CanonCost;
    }

    public int GetFenceCost()
    {
        return FenceCost;
    }
    public bool IsAvailibleToSetTavern()
    {
        return CanSetTavern;
    }

    public GameObject GetTavernPrefab()
    {
        return TavernPrefab;
    }

    public int GetTavernCost()
    {
        return TavernCost;
    }
    public int GetUnitsInTavern()
    {
        return UnitsInTavern;
    }
    
    public bool IsAvailibleToSetSuperCanon()
    {
        return CanSetSuperCanon;
    }

    public GameObject GetSuperCanonPrefab()
    {
        return CanonSuperPrefab;
    }

    public int GetSuperCanonCost()
    {
        return CanonSuperCost;
    }
    
    
}