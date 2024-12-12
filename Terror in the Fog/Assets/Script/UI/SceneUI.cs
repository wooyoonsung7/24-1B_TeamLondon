using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    public GameObject settingsMenu;
    private static bool isGameStart = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
        if(settingsMenu != null) settingsMenu.SetActive(false);

        if (!isGameStart)
        {
            UnlockAchievements();
            //Debug.Log("�۵��Ѵ٤���");
            isGameStart = true;
        }

        PlayerPrefs.SetInt("FirstStart", 1);
    }

    private void Update()
    {
        if (settingsMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settingsMenu.activeSelf)
                {
                    settingsMenu.SetActive(false);
                }
            }
        }

        if (ButtonUI.isEnd)
        {
            GoToMain();
            ButtonUI.isEnd = false;
        }
    }
    public void LoadTestScene()
    {
        SceneManager.LoadScene("LoadingUI");
    }

   
    public void LoadAchievementsScene()
    {
        
        PlayerPrefs.SetString("NextScene", "AchievementUI");
        
        SceneManager.LoadScene("AchievementUI");
    }


    public void GameExit()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void UnlockAchievements()
    {
        // ���� ���� �� ���� �޼�
        PlayerPrefs.SetInt("FirstStart", 1);  // ù ���� ���� ���� �޼�
        PlayerPrefs.SetInt("FirstSleep", 0);  // ���ڴ� ������ ���� �޼����� ����
        PlayerPrefs.SetInt("GameEnding", 0);  // ���� ������ ���� �޼����� ����

        // ���� ������ �̵�
        
    }

    
}
