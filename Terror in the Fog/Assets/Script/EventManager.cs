using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour //이벤트관리
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

    private void ExitTargetHouse() //타겟의 집에서 나가기
    {
        housedoor.isCanUse = true;
        if (housedoor.isOpen)
        {
            GameManager.currentMap = 2;   //거리로 이동
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
        Debug.Log("된다2");
        if (GameManager.Days == 1)
        {
            if (name == "루비목걸이") day1Door.isCanUse = true; Debug.Log("된다");
        }

        if (GameManager.Days == 2)
        {
            if (name == "토큰2") count++; if (count == 2) day2Door.isCanUse = true;
            if (name == "토큰1") count++; if (count == 2) day2Door.isCanUse = true;
        }

        if (GameManager.Days == 3)
        {
            if (name == "다이아반지") day1Door.isCanUse = true;
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
                GameManager.currentMap = 4;   //거리로 이동
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
                GameManager.currentMap = 4;         //거리로 이동
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
                GameManager.currentMap = 4;          //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day3일 때, Street_1에만 해당 스크립트 넣기
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
