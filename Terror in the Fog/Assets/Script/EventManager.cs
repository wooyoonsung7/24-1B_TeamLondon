using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour //�̺�Ʈ����
{
    public static EventManager instance;

    [Header("Tutorial Event")]
    [SerializeField] private Door roomdoor;
    [SerializeField] private Door housedoor;
    [SerializeField] private Enemy enemy;

    [Header("AtStreet")]
    [SerializeField] private Door targetHousedoor;

    [Header("AtHome")]
    [SerializeField] private Bed bed;
    [SerializeField] private Door homeDoor;

    [Header("DayOne Event")]
    [SerializeField] private Safe safe;
    [SerializeField] private Door day1Door;

    [Header("DayTwo Event")]
    [SerializeField] private Door day2Door;
    [SerializeField] private Toy toy;

    [Header("DayThree Event")]
    [SerializeField] private Door day3Door;

    private bool EndEvent = false;
    private bool EndEvent_2 = false;

    private int count = 0;
    private void Awake()
    {
        instance = this;
    }

    public void TutoEvent()
    {
        if (roomdoor != null && !EndEvent)
        {
            if (roomdoor.isCanUse)
            {
                enemy.gameObject.SetActive(true);
                ResearchManager_Simple.instance.StartCoroutine("Tuto");
                EndEvent = true;
            }
        }
        if (ResearchManager_Simple.instance != null)
        {
            if (ResearchManager_Simple.instance.EventEnd)
            {
                ExitTargetHouse();
            }
        }
    }

    private void ExitTargetHouse() //Ÿ���� ������ ������
    {
        housedoor.isCanUse = true;
        if (housedoor.isOpen)
        {
            GameManager.currentMap = 2;   //�Ÿ��� �̵�
            SceneManager.LoadScene("Street_1");
        }
    }

    public void GotoTargetHouse(int day)
    {
        if (targetHousedoor != null)
        {
            if (targetHousedoor.isOpen)
            {
                GameManager.currentMap = 3;
                SceneManager.LoadScene("GameScene_" + day);
            }
        }
    }

    public void GoToStreet()
    {
        if (homeDoor != null)
        {
            if (homeDoor.isOpen)
            {
                GameManager.currentMap = 2;
                SceneManager.LoadScene("Street_2");
            }
        }
    }
    public void CheckIventoryItem(string name)
    {
        Debug.Log("�ȴ�2");
        if (GameManager.Days == 1)
        {
            if (name == "�������") day1Door.isCanUse = true; Debug.Log("�ȴ�");
        }

        if (GameManager.Days == 2)
        {
            if (name == "��ū2") count++; if (count == 2) day2Door.isCanUse = true;
            if (name == "��ū1") count++; if (count == 2) day2Door.isCanUse = true;
        }

        if (GameManager.Days == 3)
        {
            if (name == "���̾ƹ���") day1Door.isCanUse = true;
        }
    }

    public void DayOneEvent()
    {
        if (ResearchManager_Simple.instance != null)
        {
            if (!EndEvent)
            {
                ResearchManager_Simple.instance.StartSafeCoroutine();
                EndEvent = true;
            }

            if (safe == null || day1Door == null) return;

            if (safe.isUnLocked && !EndEvent_2)
            {
                ResearchManager_Simple.instance.isEnd_2 = true;
                EndEvent_2 = true;
            }
            if (day1Door.isOpen)
            {
                GameManager.currentMap = 4;   //�Ÿ��� �̵�
                SceneManager.LoadScene("Street_1");
            }
        }
    }

    public void DayTwoEvent()
    {
        if (ResearchManager_Simple.instance != null)
        {
            if (!EndEvent)
            {
                ResearchManager_Simple.instance.StartSafeCoroutine();
                EndEvent = true;
            }

            if (toy == null || day2Door == null) return;

            if (toy.isCanUse && !EndEvent_2)
            {
                EndEvent_2 = true;
            }
            if (day2Door.isOpen)
            {
                GameManager.currentMap = 4;         //�Ÿ��� �̵�
                SceneManager.LoadScene("Street_1");
            }
        }
    }

    public void DayThreeEvent()
    {
        if (ResearchManager.instance != null)
        {
            if (day3Door.isOpen)
            {
                GameManager.currentMap = 4;          //�Ÿ��� �̵�
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day3�� ��, Street_1���� �ش� ��ũ��Ʈ �ֱ�
        {

        }
    }

    private void DayFourEvent()
    {

    }

    private void DayFiveEvent()
    {

    }
}
