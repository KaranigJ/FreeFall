using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject[] buttons;
    public Sprite[] check;
    private Image[] views;
    public Image preview;
    private int SkinID;
    private int MapID;


    void Start(){
        views = new Image[4];
        for(int i=0; i<4; i++)
            views[i] = buttons[i].GetComponent<Image>();
        views[0].sprite = check[1];
        
        for(int i = 0; i<4; i++){
            if(i == 0)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }    

    }

    void Update(){
         
    }
    
    void Page(int a){
        for(int i = 0; i<4; i++)
        {
            views[i].sprite = check[0];
            if(i == a){
                views[i].sprite = check[1];
                continue;                
            }
        }

        for(int i = 0; i<4; i++)
        {
            if(i == a)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }

        switch (a)
        {
            case 0:
                preview.sprite = GameObject.Find("SpaceMan").GetComponent<Image>().sprite;
                preview.name = "SpaceMan";
                break;
            case 1:
                preview.sprite = GameObject.Find("Space").GetComponent<Image>().sprite;
                preview.name = "Space";
                break;
            case 2:
                preview.sprite = GameObject.Find("Jat").GetComponent<Image>().sprite;
                preview.name = "Jat";
                break;
            case 3:
                preview.sprite = GameObject.Find("Jat").GetComponent<Image>().sprite;
                preview.name = "Jat";
                break;
        }
    }

    public void Switch(int page)
    {
        Page(page);
    }
    
    public void View(string name)
    {
        preview.sprite = GameObject.Find(name).GetComponent<Image>().sprite;
        preview.name = name;
    }

    public void Select()
    {
        if (preview.name == "Space")
            PlayerPrefs.SetInt("map", 0);
        else if (preview.name == "Mountain")
            PlayerPrefs.SetInt("map", 2);
        else if (preview.name == "Jungle")
            PlayerPrefs.SetInt("map", 1);
    }
    public void menu()
    {
        Application.LoadLevel(0);
    }
}
