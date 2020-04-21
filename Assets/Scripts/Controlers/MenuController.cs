using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   public void StartGame()
   {
      SceneManager.LoadScene(PlayerPrefs.GetInt("LevelsCompleted"));
   }

   public void ShowLevelMenu(Canvas canvasToShow)
   {
      canvasToShow.gameObject.SetActive(true);
      gameObject.SetActive(false);
   }
}
