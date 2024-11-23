using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour  //게임 전체적으로 <퀘스트, 각 회차의 맵>
{
    public static GameManager Instance;
    public static int Days = 0;
    public static int currentMap = 0;            //0번 튜토리얼, 1번이 집, 2번이 가는거리, 3번이 타겟의 집, 4번이 돌아오는 거리
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
        //CheckDays();   임시로 빼놓음
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Days = 3; Debug.Log(Days);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Days = 2; Debug.Log(Days);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Days = 1; Debug.Log(Days);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Days = 4; Debug.Log(Days);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Days = 5; Debug.Log(Days);
        }
    }
    public void transMap()
    {
        if (currentMap == 2)
        {
            //잠시 빼 놓자
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
        Debug.Log("현재 날짜는" + Days + "일차입니다");
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

    }
    private void DayFive()
    {

    }
}

