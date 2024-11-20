using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour  //���� ��ü������ <����Ʈ, �� ȸ���� ��>
{
    public static GameManager Instance;
    public static int Days = 0;
    public bool gotoHouse = false;
    public static int currentMap = 0;            //0�� Ʃ�丮��, 1���� ��, 2���� �Ÿ�, 3���� Ÿ���� ��
    private EVENTTYPE eventType;
    private enum EVENTTYPE
    {
        Tutorial,
        DayOne,
        DayTwo,
        DayThree,
        DayFour,
        DayFive,
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SETDAY()
    {
        switch (eventType)
        {
            case EVENTTYPE.Tutorial:
                Tuto();
                break;
            case EVENTTYPE.DayOne:
                DayOne();
                break;
            case EVENTTYPE.DayTwo:
                DayTwo();
                break;
            case EVENTTYPE.DayThree:
                DayThree();
                break;
            case EVENTTYPE.DayFour:
                DayFour();
                break;
            case EVENTTYPE.DayFive:
                DayFive();
                break;
        }
    }

    private void Start()
    {
        //CheckDays();   �ӽ÷� ������
    }

    private void Update()
    {
        CheckDays();
        SETDAY();
        transMap();
        MasterKey();
    }
    private void ChangeEvent(EVENTTYPE newType)
    {
        eventType = newType;
    }

    private void CheckDays()
    {
        if (Days == 0) ChangeEvent(EVENTTYPE.Tutorial);
        if (Days == 1) ChangeEvent(EVENTTYPE.DayOne);
        if (Days == 2) ChangeEvent(EVENTTYPE.DayTwo);
        if (Days == 3) ChangeEvent(EVENTTYPE.DayThree);
        if (Days == 4) ChangeEvent(EVENTTYPE.DayFour);
        if (Days == 5) ChangeEvent(EVENTTYPE.DayFive);
    }

    private void MasterKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(Days++);
        }
    }
    public void transMap()
    {
        if (currentMap == 2)
        {
            //��� �� ����
        }

        if (currentMap == 1)
        {
            
        }
        EventManager.instance.GotoTargetHouse(Days);
        EventManager.instance.GoToStreet();
    }

    public void PassDay()
    {
        Days++;
        Debug.Log("���� ��¥��" + Days + "�����Դϴ�");
    }

    private void Tuto()
    {
        EventManager.instance.TutoEvent();
    }

    private void DayOne()
    {
        EventManager.instance.DayOneEvent();
    }
    private void DayTwo()
    {

    }
    private void DayThree()
    {

    }
    private void DayFour()
    {

    }
    private void DayFive()
    {

    }
}

