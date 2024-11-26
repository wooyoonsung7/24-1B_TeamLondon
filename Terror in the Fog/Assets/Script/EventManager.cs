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

    [Header("DayThree Event")]
    [SerializeField] private Door day3Door;

    [Header("DayFour Event")]
    [SerializeField] private Door day4Door;

    [Header("DayFive Event")]
    [SerializeField] private Door day5Door;

    private bool EndEvent = false;
    private bool EndEvent_2 = false;
    public static bool playerdead = false;

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
            //Ʃ�丮�� ����
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

    public void PlayerDead()
    {
        if (GameManager.Days == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            playerdead = true;
            InsideInventory.Instance.ClearAllItem();
            SceneManager.LoadScene("Home");
        }
    }
    public IEnumerator AfterPlayerDead()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        playerdead = false;
        yield return new WaitForSeconds(0.1f);
        bed.Use(player);
        Debug.Log("ħ�����");
        //�ڰ� �Ͼ�� �ִϸ��̼Ǹ� ������ ��
        //�ִϸ��̼��۵��� �ڵ���ġ�̵�!!
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
            if (name == "���̾ƹ���") day3Door.isCanUse = true;
        }

        if (GameManager.Days == 4)
        {
            if (name == "��ū3") count++; if (count == 2) day4Door.isCanUse = true;
            if (name == "��ū4") count++; if (count == 2) day4Door.isCanUse = true;
        }

        if (GameManager.Days == 5)
        {
            if(name == "�����̾�Ͱ���") day5Door.isCanUse = true;
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

            if (day2Door == null) return;
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

    public void DayFourEvent()
    {
        if (ResearchManager.instance != null)
        {
            if (day4Door.isOpen)
            {
                GameManager.currentMap = 4;          //�Ÿ��� �̵�
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day4�� ��, Street_1���� �ش� ��ũ��Ʈ �ֱ�
        {

        }
    }

    public void DayFiveEvent()
    {
        if (ResearchManager.instance != null)
        {
            if (day5Door.isOpen)
            {
                GameManager.currentMap = 4;          //�Ÿ��� �̵�
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day5�� ��, Street_1���� �ش� ��ũ��Ʈ �ֱ�
        {

        }
    }
}
