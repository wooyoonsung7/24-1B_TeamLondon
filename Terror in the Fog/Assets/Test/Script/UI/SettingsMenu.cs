using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool Paused = false; // ������ �Ͻ����� �������� Ȯ���ϴ� ����

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;
    [SerializeField]
    private Enemy enemy;


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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadTestScene2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EnemyStartMove();
        }

    }
    public void OpenSettingsMenu()
    {
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
        Paused = true;

        playerController.enabled = false;  //playerController ��ũ��Ʈ ��Ȱ��ȭ
        handController.enabled = false;    //handController ��ũ��Ʈ ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None;              //���콺���� Ȱ��ȭ
        Cursor.visible = true;
    }

    public void CloseSettingsMenu()
    {
        settingsPanel.SetActive(false); 
        Time.timeScale = 1f; 
        Paused = false;

        playerController.enabled = true;   //playerController ��ũ��Ʈ Ȱ��ȭ
        handController.enabled = true;     //handController ��ũ��Ʈ Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked;              //���콺���� ��Ȱ��ȭ
        Cursor.visible = true;
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

    public void LoadTestScene2()
    {
        SceneManager.LoadScene("TestScene2");
    }

    public void EnemyStartMove()
    {
        enemy.gameObject.SetActive(true);
    }
}
