using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEventMove : MonoBehaviour
{
    private float x,y,z;
    // Start is called before the first frame update
    private CircleCollider2D col;
    private Rigidbody2D rigBody;

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	private HeroController HController;

    void Start()
    {
        x = y = z = 0.1f;
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[Random.Range(PlayerPrefs.GetInt("map")*2, PlayerPrefs.GetInt("map")*2+2)];

        GameObject hero = GameObject.Find("Hero");
		HController = hero.GetComponent<HeroController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 opa = new Vector3(x, y, z);
        transform.localScale = opa;

        if (x>=0.7443f)
        {
            col.enabled = true;
            
        }
        if(x>=0.8)
        {
            Destroy(gameObject);
        }

        x += 0.0025f;
        y = x;
        z = x;
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
