using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Manager : MonoBehaviour
{
    private int coins;
    private TMPro.TextMeshProUGUI txt;
   

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        txt = GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        txt.text = "Coins: " + PlayerPrefs.GetInt("Coins", 0).ToString();
        //txt.SetText("Coins: " + this.gold.ToString());
        
    }
    public void addgold(int coins)
    {
        this.coins += coins;
        PlayerPrefs.SetInt("Coins", this.coins);
    }
}
