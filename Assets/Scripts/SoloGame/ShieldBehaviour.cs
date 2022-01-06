using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
	private HeroController HController;

	public GameObject shieldControlObject;

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
			HController.isUsingShield = true;
			Object.Instantiate(shieldControlObject, new Vector3(0f, 0f, 0f), Quaternion.identity);

			Destroy(gameObject);
		}
	}
}
