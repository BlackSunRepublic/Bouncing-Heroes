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
    [SerializeField]
    private int _currentGameLevelNumberNotIndex = 1;

    [Space(20)] [Header("LEVEL UI")]
    [SerializeField]
    private GameObject mainMenuObject;
    [SerializeField] private GameObject _uiInGameObject;

    private Player _currentPlayer;

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

        UpdateUIStartScene();
    }

    public void StartLevel(int index)
    {
        //TODO check
        _uiInGameObject.SetActive(true);
        mainMenuObject.SetActive(false);
        SceneManager.LoadScene(index);
    }

    public void SetPlayerFromLevel(Player player)
    {
        _currentPlayer = player;
        _currentPlayer.OnPlayerStop += LevelFinish;
    }


    private void LevelFinish()
    {
        Debug.Log("Level Finish");
    }

    private void UpdateUIStartScene()
    {
        if(_textMeshPro == null)
            return;
        _textMeshPro.text = _currentGameLevelNumberNotIndex.ToString();
    }

    public void StartCurrentLevel()
    {
        _uiInGameObject.SetActive(true);
        //TODO Clean ALL UI
        SceneManager.LoadScene(_currentGameLevelNumberNotIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BackToMenu()
    {
        mainMenuObject.SetActive(true);
        UpdateUIStartScene();
    }


}
