using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Workshop;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<LevelData> _levelsData;
    [Space(20)]
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TextMeshProUGUI _textGlobalScore;
    [SerializeField] private TextMeshProUGUI _textGlobalScoreInLeaderBoard;
    [SerializeField]
    private int _currentGameLevelNumberNotIndex = 1;

    [Space(20)] [Header("LEVEL UI")]
    [SerializeField]
    private GameObject mainMenuObject;
    [SerializeField] private GameObject _uiInGameObject;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _playUI;
    [Space(20)]
    [SerializeField] private TextMeshProUGUI _textInLevelPlayUICoins;
    private int _currentLevelCoins;
    [SerializeField] private TextMeshProUGUI _textInLevelPlayUIRabs;
    private int _currentLevelRabs;
    [Space(20)] [Header("LEVEL WIN PANEL")]
    [SerializeField] private TextMeshProUGUI _textInLevelWinPanelCoins;
    [SerializeField] private TextMeshProUGUI _textInLevelWinPanelScore;

    [SerializeField] private TextMeshProUGUI _textInLevelWinPanelRabs;
    [SerializeField] private TextMeshProUGUI _textInLevelWinPanelRabsScore;

    [SerializeField] private TextMeshProUGUI _textInLevelWinPanelGlobalScore;

    private Player _currentPlayer;

    [SerializeField]
    private int _multiplierCoinsToScore = 20;



    void Awake()
    {

        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateUIStartScene();
        // Invoke(nameof(SendToLeaderBoard), 4f);
    }

    public void StartLevel(int index)
    {
        //TODO check is level exist in LevelData !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        _uiInGameObject.SetActive(true);
        mainMenuObject.SetActive(false);
        _winPanel.SetActive(false);
        CleanInPlayUi();
        if (_levelsData.Count < index)
        {
            index = _levelsData.Count;
        }

        Ligin();
        Invoke(nameof(SendToLeaderBoard), 3f);
        SceneManager.LoadScene(index);
    }

    public void SetPlayerFromLevel(Player player)
    {
        _currentPlayer = player;
        _currentPlayer.OnPlayerStop += LevelFinish;
    }


    private void LevelFinish()
    {
        _winPanel.SetActive(true);
        _playUI.SetActive(false);
        //TODO Update UI pnanel
        AddToLevelDataResultOfLevel();
        UpdateUiInWinPanel();
        Debug.Log("Level Finish");
        SendToLeaderBoard();
    }

    private void UpdateUIStartScene()
    {
        if(_textMeshPro == null)
            return;

        _textMeshPro.text = _currentGameLevelNumberNotIndex.ToString();
        _textGlobalScore.text = (GetAllCoinsCountFromAllLevels() * _multiplierCoinsToScore).ToString();
        _textGlobalScoreInLeaderBoard.text = (GetAllCoinsCountFromAllLevels() * _multiplierCoinsToScore).ToString();
    }

    public void StartCurrentLevel()
    {
        Ligin();
        Invoke(nameof(SendToLeaderBoard), 4f);
        //TODO check is level exist in LevelData !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        _uiInGameObject.SetActive(true);
        _winPanel.SetActive(false);
        //TODO Clean ALL UI
        CleanInPlayUi();
        SceneManager.LoadScene(_currentGameLevelNumberNotIndex);
    }

    public void LoadMenu()
    {
        UpdateUIStartScene();
        SceneManager.LoadScene(0);
    }

    public void BackToMenu()
    {
        mainMenuObject.SetActive(true);
        UpdateUIStartScene();
    }

    private void UpdateCoinsAdnRabsUi()
    {
        _textInLevelPlayUICoins.text = _currentLevelCoins.ToString();
        _textInLevelPlayUIRabs.text = _currentLevelRabs.ToString();
    }

    public void AddCoins()
    {
        _currentLevelCoins++;
        UpdateCoinsAdnRabsUi();
    }

    private void CleanInPlayUi()
    {
        _currentLevelCoins = 0;
        _currentLevelRabs = 0;
        UpdateCoinsAdnRabsUi();
    }

    private void AddToLevelDataResultOfLevel()
    {
        int levelCoinsResult = _currentLevelCoins - _currentLevelRabs;
        if (levelCoinsResult < 0)
            levelCoinsResult = 0;
        if (_levelsData[_currentGameLevelNumberNotIndex - 1].MaximumPointsEarnedInLevel < levelCoinsResult)
        {
            _levelsData[_currentGameLevelNumberNotIndex - 1].MaximumPointsEarnedInLevel = levelCoinsResult;
        }
    }

    private int GetAllCoinsCountFromAllLevels()
    {
        int result = 0;
        foreach (var level in _levelsData)
        {
            result += level.MaximumPointsEarnedInLevel;
        }

        return result;
    }

    private void UpdateUiInWinPanel()
    {
        _textInLevelWinPanelCoins.text = _currentLevelCoins.ToString();
        _textInLevelWinPanelScore.text = (_currentLevelCoins * _multiplierCoinsToScore).ToString();
        _textInLevelWinPanelRabs.text = (-_currentLevelRabs).ToString();
        _textInLevelWinPanelRabsScore.text = (-_currentLevelRabs * _multiplierCoinsToScore).ToString();

        _textInLevelWinPanelGlobalScore.text = (GetAllCoinsCountFromAllLevels() * _multiplierCoinsToScore).ToString();
    }

    public void SendToLeaderBoard()
    {
        int result = GetAllCoinsCountFromAllLevels() * _multiplierCoinsToScore;
        gameObject.GetComponent<PlayFabManager>().SendLeaderboard(result);
    }

    public void Ligin()
    {
        gameObject.GetComponent<PlayFabManager>().Login();
    }

    public void IncreaseCurrentLevelNumber()
    {
        _currentGameLevelNumberNotIndex++;
        if (_levelsData.Count <= _currentGameLevelNumberNotIndex)
        {
            _currentGameLevelNumberNotIndex = _levelsData.Count;
        }
    }

}
