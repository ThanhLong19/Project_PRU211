using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void playAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("testMenu");
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
