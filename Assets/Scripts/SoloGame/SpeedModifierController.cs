using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifierController : MonoBehaviour
{
	//[HideInInspector]
	public float objectSpeedModifier = 1f;
    public float savemod;

    private bool oneTimeChange = false;
    private bool oneTimeChange2 = false;

    private float lastSpeedModifierChange;
    public bool maxIsReached = false;

    void Start()
    {
        lastSpeedModifierChange = Time.time;
    }

    void Update()
    {
        if (!maxIsReached)
        {
            if (Time.time - lastSpeedModifierChange >= 10f)
            {
                objectSpeedModifier += 0.5f;
                if (objectSpeedModifier >= 5f) // тут выставляется максимальное значение множителя скорости
                {
                    maxIsReached = true;
                }
                lastSpeedModifierChange = Time.time;
            }
        }
        
        if (EventController.eventRadio != 2 || EventController.eventRadio != 0 )
        {
            if (!oneTimeChange)
            {
                savemod = objectSpeedModifier;
                oneTimeChange = true;
                oneTimeChange2 = false;
            }
            
        }
        else
        {
            if(!oneTimeChange2 && oneTimeChange)
            {
                objectSpeedModifier = savemod;
                oneTimeChange2 = true;
                oneTimeChange = false;
            }
        }
    }
}
