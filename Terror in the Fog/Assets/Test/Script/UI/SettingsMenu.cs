using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool Paused = false; // ������ �Ͻ����� �������� Ȯ���ϴ� ����
   

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {
                CloseSettingsMenu();
            }
            else
            {
                OpenSettingsMenu();
            }
        }

    }
    public void OpenSettingsMenu()
    {
        settingsPanel.SetActive(true); 
        Time.timeScale = 0f; 
        Paused = true; 
    }

    public void CloseSettingsMenu()
    {
        settingsPanel.SetActive(false); 
        Time.timeScale = 1f; 
        Paused = false;
    }



    public void QuitGame()
    {
        SoundManager.instance.PlaySound("Click");
        SoundManager.instance.StopSound("GameBGM");
        SoundManager.instance.StopSound("FeverBGM");
        // ���� ����
        Application.Quit();
    }


    public void ReturnToMainMenu()
    {
        SoundManager.instance.PlaySound("Click");
        SoundManager.instance.StopSound("GameBGM");
        SoundManager.instance.StopSound("FeverBGM");
        // ���� �޴��� �̵�
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f; // ���� �ð� �簳
        }
        SceneManager.LoadScene("TestScene3");
        Time.timeScale = 1f;
    }
}
