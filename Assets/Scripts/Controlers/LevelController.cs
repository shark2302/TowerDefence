﻿using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    private const int FRUSTRUM = 143;

    [SerializeField] private LevelConfig _config;
    [SerializeField] private Camera _camera;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Text _textForMoney;
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private GameObject _mainTower;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Text _endGameText;
    [SerializeField] private Button _nextLevelButton;
    private GameObject _currentObject;
    private BoxCollider _currentBoxCollider;
    private Animator _currentObjectAnimator;
    private int _money;
    private bool _endGame;
    private bool _win = false;
    private int _sceneIndex;
    
    void Start()
    {
        if (_config.CanSetCanon)
        {
            _buttons[0].gameObject.SetActive(true);
            _buttons[0].GetComponentInChildren<Text>().text = _config.GetCanonPrefab().name + "\n" + "Cost : " + _config.GetCanonCost();
        }

        if (_config.CanSetFence)
        {
            _buttons[1].gameObject.SetActive(true);
            _buttons[1].GetComponentInChildren<Text>().text = _config.GetFencePrefab().name + "\n" + "Cost : " + _config.GetFenceCost();
        }

        _textForMoney.text = "Money : " + _config.GetMoneyForLevel() + "$";
        _money = _config.GetMoneyForLevel();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        if (_endGame)
        {
            StartCoroutine(EndGame());
        }
        if (_mainTower == null)
        {
            _endGame = true;
            _win = false;
        }
        if (CheckWin())
        {
            _endGame = true;
            _win = true;
        }
        if (_currentObject != null)
        { 
                Vector3 mouseposition = Input.mousePosition;
                mouseposition.z = FRUSTRUM;
                mouseposition = _camera.ScreenToWorldPoint(mouseposition);
                _currentObject.transform.position = new Vector3(mouseposition.x, 0, mouseposition.y);
                if (Input.GetMouseButtonDown(0))
                {
                    _currentObjectAnimator.SetBool("Active", true);
                    _currentObject = null;
                    MakeButtonsActive();
                    CheckMoney();
                    if (_currentBoxCollider != null)
                    {
                        _currentBoxCollider.enabled = true;
                        _currentBoxCollider = null;
                    }
                }
        }
    }


    public void SetCanon()
    {
        _currentObject = Instantiate(_config.GetCanonPrefab(), new Vector3(-1000, 0, 0), Quaternion.identity);
        _currentObjectAnimator = _currentObject.GetComponent<Animator>();
        _currentObjectAnimator.SetBool("Active", false);
        _money -= _config.GetCanonCost();
        _textForMoney.text = "Money : " + _money + "$";
        MakeButtonsInactive();
    }

    public void SetFence()
    {
        _currentObject = Instantiate(_config.GetFencePrefab(), new Vector3(-1000, 0, 0), _config.GetFencePrefab().transform.rotation);
        _currentObjectAnimator = _currentObject.GetComponent<Animator>();
        _currentObjectAnimator.SetBool("Active", false);
        _currentBoxCollider = _currentObject.GetComponent<BoxCollider>();
        _currentBoxCollider.enabled = false;
        _money -= _config.GetFenceCost();
        _textForMoney.text = "Money : " + _money + "$";
        MakeButtonsInactive();
    }

    private void MakeButtonsInactive()
    {
        foreach (var but in _buttons)
        {
            but.interactable = false;
        }
    }

    private void MakeButtonsActive()
    {
        foreach (var but in _buttons)
        {
            but.interactable = true;
        }
    }

    private void CheckMoney()
    {
        if (_money < _config.GetCanonCost())
        {
            _buttons[0].interactable = false;
        }

        if (_money < _config.GetFenceCost())
        {
            _buttons[1].interactable = false;
        }
    }

    private List<GameObject> CombineSpawnedEnemy()
    {
        List<GameObject> res = new List<GameObject>();
        foreach (var spawner in _spawners)
        {
            if(spawner != null)
            res.AddRange(spawner.GetEnemyList());
        }

        return res;
    }

    private bool CheckWin()
    {
        foreach (var spawner in _spawners)
        {
            if (!spawner.AllSpawnedObjectsDestroyed())
                return false;
        }

        return true;
    }
    

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        _endGamePanel.SetActive(true);
        if (_win)
        {
            _endGameText.text = "Вы победили!";
            if(_sceneIndex + 1 > PlayerPrefs.GetInt("LevelsCompleted"))
                PlayerPrefs.SetInt("LevelsCompleted", _sceneIndex + 1);
        }
        if (!_win)
        {
            _endGameText.text = "Вы проиграли!";
            _nextLevelButton.gameObject.SetActive(false);
        }
        enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_sceneIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}