using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private Text _endGameText;
    [SerializeField] private Button _nextLevelButton;
    private GameObject _currentObject;
    private BoxCollider _currentBoxCollider;
    private Animator _currentObjectAnimator;
    private NavMeshObstacle _currentObstacle;
    private int _money;
    private bool _endGame;
    private bool _win = false;
    private int _sceneIndex;
    private Coroutine _endGameRoutine;
    private bool _paused;
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

        if (_config.CanSetTavern)
        {
            _buttons[2].gameObject.SetActive(true);
            _buttons[2].GetComponentInChildren<Text>().text =
                _config.GetTavernPrefab().name + "\n" + "Cost : " + _config.GetTavernCost() + 
                "\n(" + _config.GetUnitsInTavern() + " units)";
        }

        if (_config.CanSetSuperCanon)
        {
            _buttons[3].gameObject.SetActive(true);
            _buttons[3].GetComponentInChildren<Text>().text = _config.GetSuperCanonPrefab().name + "\n" + "Cost : " + _config.GetSuperCanonCost();
        }

        _textForMoney.text = "Money : " + _config.GetMoneyForLevel() + "$";
        _money = _config.GetMoneyForLevel();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    private void OnDisable()
    {
        if(_endGameRoutine != null)
            StopCoroutine(_endGameRoutine);
        _endGameRoutine = null;
    }


    void Update()
    {
        if (_endGame)
        {
           _endGameRoutine = StartCoroutine(EndGame());
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
                    _currentObjectAnimator?.SetBool("Active", true);
                    _currentObject = null;
                    MakeButtonsActive();
                    CheckMoney();
                    if (_currentBoxCollider != null)
                    {
                        _currentBoxCollider.enabled = true;
                        _currentBoxCollider = null;
                    }

                    if (_currentObstacle != null)
                    {
                        _currentObstacle.enabled = true;
                        _currentObstacle = null;
                    }
                }

                float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
                if (_currentObject != null && _currentObject.tag == "Fence" && mouseWheel !=0)
                {
                    if (mouseWheel > 0) {
                        _currentObject.transform.Rotate(Vector3.up * 1f, Space.Self);
                    }
                    if (mouseWheel < 0) {
                        _currentObject.transform.Rotate(Vector3.down * 1f, Space.Self);
                    }
                }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    public void SetCanon()
    {
        _currentObject = Instantiate(_config.GetCanonPrefab(), new Vector3(-1000, 0, 0), Quaternion.identity);
        _currentObjectAnimator = _currentObject.GetComponent<Animator>();
        _currentObjectAnimator.SetBool("Active", false);
        _currentObstacle = _currentObject.GetComponent<NavMeshObstacle>();
        _currentBoxCollider = _currentObject.GetComponent<BoxCollider>();
        _currentBoxCollider.enabled = false;
        _currentObstacle.enabled = false;
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
        _currentObstacle = _currentObject.GetComponent<NavMeshObstacle>();
        _currentObstacle.enabled = false;
        _currentBoxCollider.enabled = false;
        _money -= _config.GetFenceCost();
        _textForMoney.text = "Money : " + _money + "$";
        MakeButtonsInactive();
    }
    
    public void SetTavern()
    {
        _currentObject = Instantiate(_config.TavernPrefab, new Vector3(-1000, 0, 0), _config.GetTavernPrefab().transform.rotation);
        _currentObjectAnimator = _currentObject.GetComponent<Animator>();
        _currentObjectAnimator.SetBool("Active", false);
        UnitSpawner unitSpawnerScript = _currentObject.GetComponent<UnitSpawner>();
        unitSpawnerScript.SetSpawners(_spawners);
        unitSpawnerScript.SetCount(_config.GetUnitsInTavern());
        unitSpawnerScript.SetTower(_mainTower);
        _currentBoxCollider = _currentObject.GetComponent<BoxCollider>();
        _currentBoxCollider.enabled = false;
        _money -= _config.GetTavernCost();
        _textForMoney.text = "Money : " + _money + "$";
        MakeButtonsInactive();
    }
    
    public void SetSuperCanon()
    {
        _currentObject = Instantiate(_config.GetSuperCanonPrefab(), new Vector3(-1000, 0, 0), Quaternion.identity);
        _currentObjectAnimator = _currentObject.GetComponent<Animator>();
        _currentObjectAnimator.SetBool("Active", false);
        _currentObstacle = _currentObject.GetComponent<NavMeshObstacle>();
        _currentObstacle.enabled = false;
        _currentBoxCollider = _currentObject.GetComponent<BoxCollider>();
        _currentBoxCollider.enabled = false;
        _money -= _config.GetSuperCanonCost();
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
        if (_money < _config.GetTavernCost())
        {
            _buttons[2].interactable = false;
        }
        if (_money < _config.GetSuperCanonCost())
        {
            _buttons[3].interactable = false;
        }
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
    

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        _endGamePanel.SetActive(true);
        _gamePanel.SetActive(false);
        Destroy(_currentObject);
        if (_win)
        {
            _endGameText.text = "Вы победили!";
            if(_sceneIndex + 1 > PlayerPrefs.GetInt("LevelsCompleted") && _sceneIndex + 1 <=3)
                PlayerPrefs.SetInt("LevelsCompleted", _sceneIndex + 1);
            if(_sceneIndex == 3) 
                _nextLevelButton.gameObject.SetActive(false);
        }
        if (!_win)
        {
            _endGameText.text = "Вы проиграли!";
            _nextLevelButton.gameObject.SetActive(false);
        }
        enabled = false;
    }

    private void Pause()
    {
        if (!_paused)
        {
            _gamePanel.SetActive(false);
            _endGamePanel.SetActive(true);
            _nextLevelButton.gameObject.SetActive(false);
            Time.timeScale = 0;
            _paused = true;
        }
        else if (_paused)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _gamePanel.SetActive(true);
            _endGamePanel.SetActive(false);
            Time.timeScale = 1;
            _paused = false;
        }
    }

    public void RestartLevel()
    {
        if (_paused)
            Time.timeScale = 1;
        SceneManager.LoadScene(_sceneIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_sceneIndex + 1);
    }

    public void GoToMainMenu()
    {
        if (_paused)
            Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
    
    
}
