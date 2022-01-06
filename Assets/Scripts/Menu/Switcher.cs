using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switcher : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    public GameObject[] menus;
    public int number = 0;


    void Start()
    {
        number = 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 0)
                number--;
            else
                number++;
        }
    }
    public void OnDrag(PointerEventData eventData)
    { }

    public void Update()
    {
        menus[0].SetActive(false);
        menus[1].SetActive(false);
        menus[2].SetActive(false);
        if (number < 0)
            number = 0;
        else if (number > 2)
            number = 2;
        menus[number].SetActive(true);
    }
}