using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public int _coin = 0;
    public TextMeshProUGUI _coinCounter;

    private void Start() 
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _coinCounter.text = "Score :" + _coin;
    }

    public void AddCoin()
    {
        _coin += 1;
        UpdateCounter();
    }
}
