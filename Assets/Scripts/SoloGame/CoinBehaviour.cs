using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
	private int money;
	private CoinCounter coinCounter;
	private ScoreCounter scoreCounter;

    void Start()
    {
        coinCounter = GameObject.Find("Main Camera").GetComponent<CoinCounter>();
        scoreCounter = GameObject.Find("Main Camera").GetComponent<ScoreCounter>();

        if (scoreCounter.score < 50)
		{
			money = 1;
		}
		else if (scoreCounter.score >= 50 && scoreCounter.score < 125)
		{
			money = Random.Range(1, 3);
		}
		else if (scoreCounter.score >= 125 && scoreCounter.score < 250)
		{
			money = Random.Range(1, 4);
		}
		else if (scoreCounter.score >= 250 && scoreCounter.score < 400)
		{
			money = Random.Range(1, 5);
		}
		else
		{
			money = Random.Range(1, 6);
		}
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Hero")
		{
			coinCounter.amountOfCoins += money;
			coinCounter.UpdateCoinsText();

			Destroy(gameObject);
		}
	}
}