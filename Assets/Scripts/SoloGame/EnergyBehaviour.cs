using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBehaviour : MonoBehaviour
{
	private DecelerationController decelerationController;

    void Start()
    {
        decelerationController = GameObject.Find("Main Camera").GetComponent<DecelerationController>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Hero")
		{
			decelerationController.MaximizeFramesHold();

			Destroy(gameObject);
		}
	}
}