using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button quitButton;



    private void ClickActionQuit()
    {

        Application.Quit();
    }


    void Start()
    {
        
        quitButton.onClick.AddListener(ClickActionQuit);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
