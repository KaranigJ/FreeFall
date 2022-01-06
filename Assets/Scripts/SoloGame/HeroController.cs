using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class HeroController : MonoBehaviour
{
	private Rigidbody2D rb;
	public float speed;

	public GameObject[] hearts;
	private int healthPoints = 3;
	private float windSpeed = 0f;

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	private bool isUsingJetpack = false;
	public bool isUsingShield = false;
	public bool isDead = false;

	public GameObject deathCanvas;

	private CircleCollider2D col;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		
		spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("map")];

	}

	void FixedUpdate()
	{
		Vector3 acceleration = Input.acceleration;

		rb.velocity = new Vector3(acceleration.x + windSpeed, 0f, 0f) * speed;
	}

	private void Death()
	{
		Time.timeScale = 0f;
		isDead = true;
		deathCanvas.SetActive(true);
	}

	public void InteractionWithHp(int smth)
	{
		if (!isUsingJetpack)
			healthPoints -= smth;

		if (healthPoints <= 0)
		{
			healthPoints = 0;
			HeartControl();
			Death();
		}

		if (healthPoints > 6)
		{
			healthPoints = 6;
		}

		HeartControl();
	}

	public void HeartControl()
	{
		for (int i = 0; i < healthPoints; i++)
		{
			hearts[i].SetActive(true);
		}

		for (int i = healthPoints; i < 6; i++)
		{
			hearts[i].SetActive(false);
		}
	}

	public int GetHealth()
	{
		return healthPoints;
	}

	public void SetWindSpeed(float a)
    {
    	if (!isUsingJetpack)
    	{
    		windSpeed = a;
    	}
    }

    public void BecomeImmortal()
    {
    	isUsingJetpack = true;
    	windSpeed = 0f;
    }

    public void BecomeMortal()
    {
    	isUsingJetpack = false;
    }

    public bool IsUsingJetpack()
    {
    	return isUsingJetpack;
    }

}
