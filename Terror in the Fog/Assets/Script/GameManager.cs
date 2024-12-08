using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour  //���� ��ü������ <����Ʈ, �� ȸ���� ��>
{
    public static GameManager Instance;
    public static int Days = 0;
    public static int currentMap = 0;            //0�� Ʃ�丮��, 1���� ��, 2���� ���°Ÿ�, 3���� Ÿ���� ��, 4���� ���ƿ��� �Ÿ�
    public GameObject GameOverCanvas;
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

        GameOverCanvas = GameObject.Find("GameOverCanvas").gameObject;
        if(GameOverCanvas != null) GameOverCanvas.SetActive(false);
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
        CheckDays();   //�ӽ÷� ������
        AfterPlayerDead();
        Debug.Log("���ӽ��� ��¥" + Days);
    }

    private void Update()
    {
        SETDAY();
        transMap();
        //MasterKey();
        Debug.Log(Days);
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
        Debug.Log("�ٲ� ��¥" + Days);
    }

    private void MasterKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Days = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Days = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Days = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Days = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Days = 5;
        }
    }
    public void transMap()
    {
        EventManager.instance.GotoTargetHouse(Days);
        EventManager.instance.CheckGoToStreet();
    }

    public void PassDay()
    {
        Days++;
        Debug.Log("���� ��¥��" + Days + "�����Դϴ�");
    }
    public void AfterPlayerDead()
    {
        if (EventManager.playerdead)
        {
            if (Days != 5)
            {
                EventManager.instance.StartCoroutine("AfterPlayerDead"); Days--; //Debug.Log("���̺�� ��¥�� " + Days);
            }
            else
            {
                EventManager.instance.StartCoroutine("AfterPlayerDead");
            }

        }
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
        EventManager.instance.DayTwoEvent();
    }
    private void DayThree()
    {
        EventManager.instance.DayThreeEvent();
    }
    private void DayFour()
    {
        EventManager.instance.DayFourEvent();
    }
    private void DayFive()
    {
        EventManager.instance.DayFiveEvent();
    }
}

