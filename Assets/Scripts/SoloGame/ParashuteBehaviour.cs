using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParashuteBehaviour : MonoBehaviour
{
	private SpeedModifierController speedModifierController;

    void Start()
    {
        speedModifierController = GameObject.Find("Main Camera").GetComponent<SpeedModifierController>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Hero")
		{
			speedModifierController.objectSpeedModifier *= 0.8f;
			speedModifierController.maxIsReached = false;

			Destroy(gameObject);
		}
	}
}
