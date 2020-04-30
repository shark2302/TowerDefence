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
    

  

   
    
    
    
    
}