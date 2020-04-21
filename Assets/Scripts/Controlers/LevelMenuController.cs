using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{

   [SerializeField] private Button[] _levelButtons;

   private void Start()
   {
      if(PlayerPrefs.GetInt("LevelsCompleted") == 0)
         PlayerPrefs.SetInt("LevelsCompleted", 1);
      int levelsComplited = PlayerPrefs.GetInt("LevelsCompleted");
      Debug.Log(levelsComplited);
      for (int i = levelsComplited; i < _levelButtons.Length; i++)
      {
         _levelButtons[i].interactable = false;
      }
   }

   public void LoadTo(int index)
   {
      SceneManager.LoadScene(index);
   }

   public void GoToMainMenu(Canvas canvasToShow)
   {
      gameObject.SetActive(false);
      canvasToShow.gameObject.SetActive(true);
   }
   
}
