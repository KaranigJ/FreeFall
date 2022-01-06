using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecelerationController : MonoBehaviour
{
	private int framesHold; // 250 * 0.02 = 5s
	private int maxFramesHold = 250;
	private int decelerationStages = 0;
	private bool decelerationIsActive = false;

	private bool staminaInUse = false;

	public Slider slider;
	private Image fillRect;

	void Start()
	{
		fillRect = slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>();

		slider.maxValue = maxFramesHold;
		framesHold = maxFramesHold;
		slider.value = framesHold;
	}

	void FixedUpdate()
	{
		CountStages();

		if (framesHold == 0)
		{
			fillRect.color = new Color(0.953125f, 0f, 0f, 1f);
		}
		else if (framesHold == maxFramesHold)
		{
			fillRect.color = new Color(0.0859375f, 1f, 0.0703125f, 1f);
		}
		else
		{
			fillRect.color = new Color(1f, 0.96875f, 0f, 1f);
		}
		slider.value = framesHold;

		if (Input.GetKey("space"))
		{
			staminaInUse = true;
			framesHold--;
		}
		else
		{
			staminaInUse = false;
		}
	}

	private void CountStages()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began && framesHold == maxFramesHold)
			{
				staminaInUse = true;
				decelerationIsActive = true;
				framesHold--;
			}
			else if ((Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary) && staminaInUse && framesHold > 0)
			{
				framesHold--;
			}
			else if (Input.GetTouch(i).phase == TouchPhase.Ended || framesHold <= 0)
			{
				staminaInUse = false;
				decelerationIsActive = false;
			}
		}
		if (!staminaInUse && framesHold < maxFramesHold)
		{
			framesHold++;
		}

		framesHold = Mathf.Max(0, framesHold);

		if (decelerationIsActive && decelerationStages < 20)
		{
			decelerationStages++;
		}
		if (!decelerationIsActive && decelerationStages > 0)
		{
			decelerationStages--;
		}
	}

	public int GetDecelerationStages()
	{
		return decelerationStages;
	}

	public void MaximizeFramesHold()
	{
		framesHold = maxFramesHold;
	}
}