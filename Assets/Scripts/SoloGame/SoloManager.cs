using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloManager : MonoBehaviour
{
	public GameObject pauseCanvas;
	public GameObject deathCanvas;

	private HeroController HController;
	private GameObject hero;

	void Start()
	{
		pauseCanvas.SetActive(false);
		deathCanvas.SetActive(false);

		hero = GameObject.Find("Hero");
		HController = hero.GetComponent<HeroController>();
	}

	public void pause()
	{
		if (!HController.isDead)
		{
			pauseCanvas.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	public void resume()
	{
		pauseCanvas.SetActive(false);
		Time.timeScale = 1f;
	}

	public void menu()
	{
		Application.LoadLevel(0);
	}

	public void Revive()
	{
		HController.InteractionWithHp(-1);
		HController.isDead = false;
		deathCanvas.SetActive(false);
		Time.timeScale = 1f;
	}


}
