using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
	// public Sprite[] sprites;
	// private SpriteRenderer spriteRenderer;

	private HeroController HController;

	void Start()
	{
		// spriteRenderer = GetComponent<SpriteRenderer>();
		// spriteRenderer.sprite = sprites[Random.Range(PlayerPrefs.GetInt("map")*2, PlayerPrefs.GetInt("map")*2+2)];

		HController = GameObject.Find("Hero").GetComponent<HeroController>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Hero")
		{
			if (!HController.isUsingShield)
			{
				HController.InteractionWithHp(1);
			}			

			Destroy(gameObject); 
		}
	}
}