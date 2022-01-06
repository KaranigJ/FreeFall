using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
	[SerializeField]
	private float defaultSpeed; // 0.05f
	private float startSpeed;
	private float speed;
	private Vector2 moveDirection = new Vector2(0, 1);
	private Rigidbody2D rigBody;
	private Vector2 moveVelocity;

	private float deceleration;
	private DecelerationController decelerationController;
	private SpeedModifierController speedModifierController;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();

		decelerationController = GameObject.Find("Main Camera").GetComponent<DecelerationController>();
		speedModifierController = GameObject.Find("Main Camera").GetComponent<SpeedModifierController>();
		
		startSpeed = defaultSpeed * speedModifierController.objectSpeedModifier;
		deceleration = startSpeed * 0.025f;
		speed = startSpeed - (deceleration * decelerationController.GetDecelerationStages());
    }

    void FixedUpdate()
    {
    	moveVelocity = moveDirection * speed;

		startSpeed = defaultSpeed * speedModifierController.objectSpeedModifier;
		deceleration = startSpeed * 0.025f;
		speed = startSpeed - (deceleration * decelerationController.GetDecelerationStages());

    	rigBody.MovePosition(rigBody.position + moveVelocity);
    }
}
