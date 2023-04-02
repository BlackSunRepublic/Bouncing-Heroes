using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Workshop;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<LevelData> _levelsData;
    private int _currentLevel = 0;
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
    }

    public void StartLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetPlayerFromLevel(Player player)
    {
        _currentPlayer = player;
        _currentPlayer.OnPlayerStop += LevelStop;
    }

    private void LevelStop()
    {

    }

}
