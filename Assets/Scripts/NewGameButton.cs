using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public Button quit;
    public Button ButtomMenu;
    public Button button;
    public Text highScoreText;
    public Text pointText;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ClickNewGameAction);
        ButtomMenu.onClick.AddListener(ClickMenuAction);
        quit.onClick.AddListener(ClickQuitAction);
        int currentScore = GameController.Instance.GetScore();
        pointText.text = "Score: " + currentScore.ToString();
        int highScore = GameController.Instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void ClickNewGameAction()
    {
        SceneManager.LoadScene(1);
    }
    private void ClickMenuAction()
    {
        SceneManager.LoadScene(0);
    }
    private void ClickQuitAction()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
