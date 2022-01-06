using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
	public int score = 0;
	private int scoreAddingCounter = 0;

	public Text scoreText;

	void Update()
	{
		UpdateScoreText();
	}

	void FixedUpdate()
	{
		scoreAddingCounter++;
		if (scoreAddingCounter >= 20)
		{
			score++;
			scoreAddingCounter = 0;
		}
	}

	public void UpdateScoreText()
	{
		scoreText.text = "Score: " + score;
	}
}
