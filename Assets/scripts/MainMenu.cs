using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore").ToString();
        //SceneManager.LoadScene("Game");
    }


    public void Play()
    {
        SceneManager.LoadScene("Game");
        return;
    }
}
