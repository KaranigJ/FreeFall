using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerMove : MonoBehaviour
{
    public float speed = 0.03f;

    private SpriteRenderer sprite;
    public Sprite[] sprites;
    Vector2 moveVector = new Vector2(0, 1);
    private Rigidbody2D rigBody;
    private Vector2 moveVelocity;
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = moveVector * speed;
    }

    void FixedUpdate()
    {
        rigBody.MovePosition(rigBody.position + moveVelocity);            
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hero")
        {
            int a = Random.Range(0, 2);
            
            if(a==0)
                EventController.eventRadio = 2 * (PlayerPrefs.GetInt("map") + 1);
            if(a==1)
                EventController.eventRadio = 2 * (PlayerPrefs.GetInt("map") + 1) + 1;
            Destroy(gameObject);
        }

    }
}
