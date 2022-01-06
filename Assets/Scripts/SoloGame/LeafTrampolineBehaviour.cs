using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafTrampolineBehaviour : MonoBehaviour
{
	private HeroController HController;

	private SpeedModifierController speedModifierController;

    private float timerTrampoline = 0f;
    private bool trampolineCheck = false;

	void Start()
	{
		HController = GameObject.Find("Hero").GetComponent<HeroController>();
		speedModifierController = GameObject.Find("Main Camera").GetComponent<SpeedModifierController>();
	}

	void FixedUpdate()
	{
        if (trampolineCheck)
        {
            timerTrampoline -= 0.05f;

            if (timerTrampoline < 0f)
            {
                HController.BecomeMortal();
                trampolineCheck = false;
                speedModifierController.objectSpeedModifier = speedModifierController.savemod;
                Destroy(gameObject);
            }
        }
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Hero")
		{
            HController.BecomeImmortal();
            speedModifierController.savemod = speedModifierController.objectSpeedModifier;
            speedModifierController.objectSpeedModifier = 10f;
            timerTrampoline = 2f;
            trampolineCheck = true;
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {	
    	if (col.gameObject.tag == "Hero")
    	{
    		HController.BecomeMortal();
            trampolineCheck = false;
            speedModifierController.objectSpeedModifier = speedModifierController.savemod;
            Destroy(gameObject);
    	}
    }
}
