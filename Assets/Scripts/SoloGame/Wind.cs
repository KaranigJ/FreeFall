using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

	public float speed = 0.03f;

	Vector2 moveDirection = new Vector2(0, 1);
	HeroController HController;

	private bool checkTimer_HeroInWind=false;
	private float timer = 10;
	private float speedForHero = 0.3f;
	private Rigidbody2D rigBody;
	private Vector2 moveVelocity;

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;
	private SpriteRenderer upspriteRenderer;
	private SpriteRenderer downspriteRenderer;

	public GameObject upwind;
	public GameObject downwind;

	void Start()
	{
		rigBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		upspriteRenderer = upwind.GetComponent<SpriteRenderer>();
		downspriteRenderer = downwind.GetComponent<SpriteRenderer>();

		GameObject hero = GameObject.Find("Hero");
		HController = hero.GetComponent<HeroController>();

		spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("map")];
		upspriteRenderer.sprite = sprites[PlayerPrefs.GetInt("map")];
		downspriteRenderer.sprite = sprites[PlayerPrefs.GetInt("map")];
	}

	void Update()
	{
		moveVelocity = moveDirection * speed;
	}

	void FixedUpdate()
	{
		rigBody.MovePosition(rigBody.position + moveVelocity);
	}

	/*void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Hero")
		{
			HController.SetWindSpeed(speedForHero);
			checkTimer_HeroInWind = true;
		}
	}
*/
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Hero")
		{
			HController.SetWindSpeed(0f); 
		}
		if (col.gameObject.tag == "TOP")
		{
			Destroy(this.gameObject); 
		}
	}
	

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Hero")
		{
			HController.SetWindSpeed(speedForHero);
		}
	}


	public void Initialize(float timer, float b)
	{
		speedForHero = b;
		this.timer = timer;
		if(speedForHero<0)
			transform.localRotation = Quaternion.Euler(0, 180, 0);
	}
}
