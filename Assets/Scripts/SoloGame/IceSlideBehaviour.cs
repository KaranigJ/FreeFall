using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlideBehaviour : MonoBehaviour
{
	private HeroController HController;
	private SpeedModifierController speedModifierController;

	void Start()
	{
		HController = GameObject.Find("Hero").GetComponent<HeroController>();
		speedModifierController = GameObject.Find("Main Camera").GetComponent<SpeedModifierController>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Hero")
		{
            HController.BecomeImmortal();
            speedModifierController.savemod = speedModifierController.objectSpeedModifier;
            speedModifierController.objectSpeedModifier = 10f;
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {
		if (col.gameObject.tag == "Hero")
		{
            HController.BecomeMortal();
            speedModifierController.objectSpeedModifier = speedModifierController.savemod;
		}
        if (col.gameObject.tag == "TOP")
		{
			Destroy(gameObject);
		}
    }
}
