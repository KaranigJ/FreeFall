using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
	private HeroController HController;

    void Start()
    {
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
			HController.InteractionWithHp(-1);

			Destroy(gameObject);
		}
	}
}