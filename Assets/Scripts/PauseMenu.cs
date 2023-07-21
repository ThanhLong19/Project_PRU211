using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseBoard;

    

    public void clickPauseButton()
    {
        if (Time.timeScale == 0f) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        // Thực hiện các hành động khác bạn muốn khi trò chơi tạm dừng
        // Ví dụ: Hiển thị menu tạm dừng, vô hiệu hóa đầu vào người chơi, vv.\
        pauseBoard.SetActive(true);
        //Hien bang

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        // Thực hiện các hành động khác bạn muốn khi trò chơi tiếp tục
        // Ví dụ: Ẩn menu tạm dừng, kích hoạt đầu vào người chơi, vv.
        pauseBoard.SetActive(false);
        //Xoa bang
    }

    public AudioMixer audioMixer;

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void Cancel()
    {
        SceneManager.LoadScene(0);
    }

}
