using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text coins;
    public Text score;
    public void achievements()
    {
        Application.LoadLevel(3);
    }

    public void shop()
    {
        Application.LoadLevel(2);
    }

    public void play()
    {
        Application.LoadLevel(1);
    }

    private void Update()
    {
        coins.text = "coins: " + PlayerPrefs.GetInt("coins");
        score.text = "score: " + PlayerPrefs.GetInt("score");

    }
}
