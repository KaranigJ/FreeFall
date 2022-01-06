using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
	private float defaultSpeed = 0.05f;
	private float startSpeed = 0.05f;
	private float speed = 0.05f;

	private Transform firstTransform;
	private Transform secondTransform;

	private Vector3 tempVector;
	private Vector3 moveDirection = new Vector3(0, 1, 0);
	private Vector3 moveVelocity = new Vector3();

	private float deceleration;
	private DecelerationController decelerationController;
	private SpeedModifierController speedModifierController;

	void Start()
	{
		firstTransform = GameObject.Find("FrontFirst").GetComponent<Transform>();
		secondTransform = GameObject.Find("FrontSecond").GetComponent<Transform>();

		decelerationController = GameObject.Find("Main Camera").GetComponent<DecelerationController>();
		speedModifierController = GameObject.Find("Main Camera").GetComponent<SpeedModifierController>();

		startSpeed = defaultSpeed * speedModifierController.objectSpeedModifier;
		deceleration = startSpeed * 0.025f;
		speed = startSpeed - (deceleration * decelerationController.GetDecelerationStages());
	}

	void Update()
	{
		moveVelocity = speed * moveDirection;

		if (firstTransform.position.y > 16f)
		{
			tempVector = secondTransform.position;
			tempVector.y -= 20f;
			firstTransform.position = tempVector;
		}
		else if (secondTransform.position.y > 16f)
		{
			tempVector = firstTransform.position;
			tempVector.y -= 20f;
			secondTransform.position = tempVector;
		}

		startSpeed = defaultSpeed * speedModifierController.objectSpeedModifier;
		deceleration = startSpeed * 0.025f;
		speed = startSpeed - (deceleration * decelerationController.GetDecelerationStages());
	}

	void FixedUpdate()
	{
		firstTransform.position += moveVelocity;
		secondTransform.position += moveVelocity;
	}
}
