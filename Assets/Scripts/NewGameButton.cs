using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public Button button;
    public Text highScoreText;
    public Text pointText;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ClickAction);

        int currentScore = GameController.Instance.GetScore();
        pointText.text = "Score: " + currentScore.ToString();

        int highScore = GameController.Instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();

    }

    private void ClickAction()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
