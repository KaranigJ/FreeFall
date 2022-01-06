using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
	private HeroController HController;

	private ScoreCounter scoreCounter;

    void Start()
    {
        HController = GameObject.Find("Hero").GetComponent<HeroController>();

		scoreCounter = GameObject.Find("Main Camera").GetComponent<ScoreCounter>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Hero")
		{
			if (!HController.isUsingShield && !HController.IsUsingJetpack())
			{
				if (scoreCounter.score < 100)
				{
					scoreCounter.score = 0;
				}
				else
				{
					scoreCounter.score -= 100;
				}
				scoreCounter.UpdateScoreText();
				HController.InteractionWithHp(1);
			}

			Destroy(gameObject);
		}
	}
}