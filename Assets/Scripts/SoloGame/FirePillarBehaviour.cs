using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarBehaviour : MonoBehaviour
{
	private HeroController HController;

	void Start()
	{
		HController = GameObject.Find("Hero").GetComponent<HeroController>();

        if (transform.position.x >= 0f)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
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
