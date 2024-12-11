using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour
{
    public Image firstStartImage;       // ���� �̹���
    public Image firstSleepImage;       // ���� �̹���
    public Image gameEndingImage;      // ���� �̹���
    public Image lockImageFirstStart;  // ��� �̹��� (ù ���� ����)
    public Image lockImageFirstSleep;  // ��� �̹��� (ù ��)
    public Image lockImageGameEnding; // ��� �̹��� (���� ����)

    public Text firstStartText;         // �ؽ�Ʈ
    public Text firstSleepText;         // �ؽ�Ʈ
    public Text gameEndingText;        // �ؽ�Ʈ

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart", 0) == 1)
        {
            firstStartImage.gameObject.SetActive(true);
            firstStartText.text = "ù ���� ����!";
            lockImageFirstStart.gameObject.SetActive(false);  // ��� �̹��� �����
        }
        else
        {
            lockImageFirstStart.gameObject.SetActive(true);  // ��� �̹��� ���̱�
        }

        if (PlayerPrefs.GetInt("FirstSleep", 0) == 1)
        {
            firstSleepImage.gameObject.SetActive(true);
            firstSleepText.text = "ù ��!";
            lockImageFirstSleep.gameObject.SetActive(false);  // ��� �̹��� �����
        }
        else
        {
            lockImageFirstSleep.gameObject.SetActive(true);  // ��� �̹��� ���̱�
        }

        if (PlayerPrefs.GetInt("GameEnding", 0) == 1)
        {
            gameEndingImage.gameObject.SetActive(true);
            gameEndingText.text = "���� ����!";
            lockImageGameEnding.gameObject.SetActive(false);  // ��� �̹��� �����
        }
        else
        {
            lockImageGameEnding.gameObject.SetActive(true);  // ��� �̹��� ���̱�
        }
    }
    void Update()
    {
        // ESC Ű�� ������ �� ���� ȭ������ ���ư���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // "MainMenu" ������ �ε� (MainMenu �� �̸��� �°� ����)
            SceneManager.LoadScene("MainScene");
        }
    }
}




