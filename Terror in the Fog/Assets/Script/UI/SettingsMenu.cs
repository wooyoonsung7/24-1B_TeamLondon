using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject settingsMenu;
    public bool Paused = false; // ������ �Ͻ����� �������� Ȯ���ϴ� ����

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;

    private Enemy enemy;
    /*
    private void Awake()
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if(achievementManager != null && GameObject.Find("alertPanel") != null) achievementManager.alertPanel = GameObject.Find("alertPanel");
        achievementManager.alertTitleText = GameObject.Find("alertTitleText").GetComponent<Text>();
        achievementManager.alertDescriptionText = GameObject.Find("alertDescriptionText").GetComponent<Text>();
        if(AchievementManager.instance.alertPanel != null) achievementManager.alertPanel.SetActive(false);
    }*/
    
    void Start()
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if (achievementManager != null && GameObject.Find("alertPanel") != null) achievementManager.alertPanel = GameObject.Find("alertPanel");
        achievementManager.alertTitleText = GameObject.Find("alertTitleText").GetComponent<Text>();
        achievementManager.alertDescriptionText = GameObject.Find("alertDescriptionText").GetComponent<Text>();
        if (AchievementManager.instance.alertPanel != null) achievementManager.alertPanel.SetActive(false);

        pausemenu.SetActive(false);
        settingsMenu.SetActive(false);
        if(FindAnyObjectByType<Enemy>() != null) enemy = FindAnyObjectByType<Enemy>();
    }

    void Update() 
    {
        if (EventManager.instance == null) return;

        if (EventManager.instance.isGameOver)
        {
            playerController.enabled = false;  //playerController ��ũ��Ʈ ��Ȱ��ȭ
            handController.enabled = false;
            Cursor.lockState = CursorLockMode.None;              //���콺���� Ȱ��ȭ
            Cursor.visible = true;

            if (ButtonUI.isEnd)
            {
                GameOver();
                ButtonUI.isEnd = false;
            }
            if (ButtonUI.isEnd_2)
            {
                ReturnToMainMenu();
                ButtonUI.isEnd_2 = false;
            }
            return;
        }

        if (Paused == true)
        {
            if (ButtonUI.isEnd)
            {
                CloseSettingsMenu();
                ButtonUI.isEnd = false;
            }
            if (ButtonUI.isEnd_2)
            {
                ReturnToMainMenu();
                ButtonUI.isEnd_2 = false;
            }
        }

        if(enemy != null) if (!enemy.isOneTime4) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
            }
            else
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
    }
    public void OpenSettingsMenu()
    {
        PauseSounds();
        Time.timeScale = 0f;
        pausemenu.SetActive(true);
        Paused = true;

        playerController.enabled = false;  //playerController ��ũ��Ʈ ��Ȱ��ȭ
        handController.enabled = false;    //handController ��ũ��Ʈ ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None;              //���콺���� Ȱ��ȭ
        Cursor.visible = true;
    }

    public void CloseSettingsMenu()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;

        playerController.enabled = true;   //playerController ��ũ��Ʈ Ȱ��ȭ
        handController.enabled = true;     //handController ��ũ��Ʈ Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked;              //���콺���� ��Ȱ��ȭ
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        //SoundManager.instance.PlaySound("Click");
        //SoundManager.instance.StopSound("GameBGM");
        //SoundManager.instance.StopSound("FeverBGM");
        // ���� ����
        Application.Quit();
    }


    public void ReturnToMainMenu()
    {
        //SoundManager.instance.PlaySound("Click");
        //SoundManager.instance.StopSound("GameBGM");
        //SoundManager.instance.StopSound("FeverBGM");
        // ���� �޴��� �̵�
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f; // ���� �ð� �簳
        }
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        //Debug.Log("���糯¥�� " + GameManager.Days);
        if (GameManager.Days == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            EventManager.playerdead = true;
            SceneManager.LoadScene("Home");
        }
    }

    private void PauseSounds()
    {

        string[] soundnames = {"Walk", "Run", "EnemyMove" };
        foreach (string str in soundnames)
        {
            SoundManager.instance.PauseSound(str);
        }
    }
}
