using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
	public Text coinsText;
	// [HideInInspector]
	public int amountOfCoins = 0;
	private int allcoins;

	void Start()
	{
		coinsText.text = "Coins: " + amountOfCoins;
		allcoins = PlayerPrefs.GetInt("coins");
	}

	public void UpdateCoinsText()
	{
		coinsText.text = "Coins: " + amountOfCoins;
		allcoins += amountOfCoins;
		PlayerPrefs.SetInt("coins", allcoins);
	}
}
