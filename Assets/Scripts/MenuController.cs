using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Button menuButton;
    
    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(ClickAction);
    }

    private void ClickAction()
    {
        Debug.Log("Menu Cliked");
        SceneManager.LoadScene(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
