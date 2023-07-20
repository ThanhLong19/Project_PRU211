using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucnangMenu : MonoBehaviour
{
    




    public void Choimoi()
    {
        SceneManager.LoadScene(1);
    }
    public void Thoat()
    {
        Application.Quit();
    }

}
