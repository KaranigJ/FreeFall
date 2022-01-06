using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static int eventRadio = 0;
    public static bool eventIsActivated = false;
    public GameObject[] backgrounds;

    private float timer = 10f;

    public enum  Events
    {
        Standard = 0,
        Back = 1,
        Space_Asteroids = 2,
        Space_Moon = 3,
        Jungle_Temple = 4,
        Jungle_GoldenTemple = 5,
        Mountain_Cave = 6,
        Mountain_Winds = 7
    }

    // Start is called before the first frame update
    void Start()
    {
        eventRadio = 0;
        eventIsActivated = false;
        backgrounds[0].SetActive(true);
        backgrounds[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (eventRadio == 2 * (PlayerPrefs.GetInt("map") + 1))
        {
           // backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
            if (eventIsActivated == true)
                timer -= 0.01f;
            if (timer <= 0f)
            {
                eventRadio = 1;
                timer = 10f;
            }
        }

        if (eventRadio == 2 * (PlayerPrefs.GetInt("map") + 1) + 1)
        {
            backgrounds[0].SetActive(false);
           // backgrounds[1].SetActive(true);
            if (eventIsActivated == true)
                timer -= 0.01f;
            if (timer <= 0f)
            {
                eventRadio = 1;
                timer = 10f;
            }
        }
        if(eventRadio==0)
        {
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(true);
        }
    }
}   
