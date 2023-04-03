using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public GameObject stringPrefab;
    public Transform stringParent;
    [SerializeField] private int maxCharAmount = 17;

    [SerializeField] private int minCharAmount = 15;

    const string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";

    private string IDstring = "";

    // Start is called before the first frame update
    void Start()
    {
        IDstring = GenerateIDString();
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = IDstring,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private string GenerateIDString()
    {
        string result = "";
        int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
        for(int i=0; i<charAmount; i++)
        {
            result += glyphs[Random.Range(0, glyphs.Length)];
        }
        return result;
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error whole logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Race_of_Thrones",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Race_of_Thrones",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in stringParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(stringPrefab, stringParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
