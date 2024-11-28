using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject settingsMenu;
    public bool Paused = false; // ������ �Ͻ����� �������� Ȯ���ϴ� ����

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;

    void Start()
    {
        pausemenu.SetActive(false);
        settingsMenu.SetActive(false);
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
    }
    public void OpenSettingsMenu()
    {
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
        if (GameManager.Days == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            EventManager.playerdead = true;
            InsideInventory.Instance.ClearAllItem();
            SceneManager.LoadScene("Home");
        }
    }
}
