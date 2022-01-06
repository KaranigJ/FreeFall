using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
	private HeroController HController;

	private int timer = 0;

    void Start()
    {
        HController = GameObject.Find("Hero").GetComponent<HeroController>();
    }

    void FixedUpdate()
    {
        timer++;
        if (timer >= 350) // 350 * 0.02 = 7s
        {
        	HController.isUsingShield = false;
        	Destroy(gameObject);
        }
    }
}
