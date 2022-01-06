using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackBehaviour : MonoBehaviour
{
	private HeroController HController;

	public GameObject jetpackControlObject;

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
			HController.BecomeImmortal();
			Object.Instantiate(jetpackControlObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
			Time.timeScale = 5f;

			Destroy(gameObject);
		}
	}
}
