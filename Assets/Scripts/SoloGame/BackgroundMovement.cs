using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.03f;

    Vector2 moveVector = new Vector2(0, 1);
    Vector3 underPosition = new Vector3(0f, -9f, 1f);
    Vector3 upperPosition = new Vector3(0f, 1f, 1f);
    private Rigidbody2D rigBody;
    private Vector2 moveVelocity;
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveVelocity = moveVector * speed;
    }

    void FixedUpdate()
    {   
        if(transform.position.y >=10.99f)
        {
            if(EventController.eventRadio == 1)
            {
                EventController.eventRadio = 0;
                EventController.eventIsActivated = false;
                
                this.transform.position = underPosition;
            }
            else
            {
                EventController.eventIsActivated = true;
                
                this.transform.position = underPosition;
            }
        }
        if(transform.position.y>1.02f && transform.position.y<1.03f)
        {
           this.transform.position = upperPosition;

        }
        if (EventController.eventRadio == 2 * (PlayerPrefs.GetInt("map")+1) || EventController.eventRadio == 2 * (PlayerPrefs.GetInt("map")+1)+1)
            if (!EventController.eventIsActivated)
                rigBody.MovePosition(rigBody.position + moveVelocity);
        if (EventController.eventRadio == 1)
            rigBody.MovePosition(rigBody.position + moveVelocity);
      

    }

}
