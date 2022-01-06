using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour
{
	private HeroController HController;
	private ScoreCounter scoreCounter;

	private int maxScore;

	private int delay = 50; // 50 * 0.02 = 1s

	void Start()
	{
		HController = GameObject.Find("Hero").GetComponent<HeroController>();
		scoreCounter = GameObject.Find("Main Camera").GetComponent<ScoreCounter>();
		maxScore = scoreCounter.score + 50;
	}

	void FixedUpdate()
	{
		if (scoreCounter.score >= maxScore)
		{
			Time.timeScale = 1f;
			if (delay > 0)
			{
				delay--;
			}
			else
			{
				HController.BecomeMortal();
				Destroy(gameObject);
			}
		}
	}
}
