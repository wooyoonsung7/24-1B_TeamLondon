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
    [SerializeField] private Transform spawnPos;

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
    public bool isGameOver = false;

    private int count = 0;
    private bool isOneTime = true;
    public bool isCanOpen = false;

    public bool isGameClear = false;
    private void Awake()
    {
        instance = this;
    }

    public void TutoEvent()
    {
        if (roomdoor != null && !EndEvent)
        {
            if (isOneTime)
            {
                enemy.gameObject.SetActive(false);
                isOneTime = false;
            }

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
            GameManager.currentMap = 4;   //�Ÿ��� �̵�
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

            if (isOneTime)
            {
                isOneTime = false;
                SoundManager.instance.PlaySound("Knell");
            }
        }
    }

    public void CheckGoToStreet()
    {
        if (homeDoor != null)
        {
            if (GameManager.Days != 5)
            {
                if (isCanOpen && InsideInventory.Instance.CheckInventory())
                {
                    homeDoor.isCanUse = true;
                    GoToStreet();
                }
                else homeDoor.isCanUse = false;
            }
            else
            {
                if (isCanOpen && InsideInventory.Instance.CheckInventory_3())
                {
                    homeDoor.isCanUse = true;
                    GoToStreet();
                }
                else homeDoor.isCanUse = false;

                CheckGameClear();
            }
        }
    }

    private void GoToStreet()
    {
        if (homeDoor.isOpen)
        {
            GameManager.currentMap = 2;
            SceneManager.LoadScene("Street_2");
            isOneTime = true;
        }
    }

    private void CheckGameClear()
    {
        int[] slotIds = { 90, 91, 92, 93, 94 };
        int count = 0;
        for (int i = 0; i < slotIds.Length; i++)
        {
            if (SaveData.instance.data.ContainsKey("����ǰ����" + slotIds[i]))
            {
                count++;
            }
        }
        if (count >= slotIds.Length) { isGameClear = true; GameClear(); } 
        else count = 0;
    }

    private void GameClear()
    {
        //���� �ʱ�ȭ
        //�ִϸ��̼�(����)�ֱ�
        GameManager.Days = 0;
        SceneManager.LoadScene("GameClear");
    }

    public void PlayerDead()
    {
        InsideInventory.Instance.ClearAllItem();
        GameManager.Instance.GameOverCanvas.SetActive(true);
        isGameOver = true;
    }
    public IEnumerator AfterPlayerDead()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.position = spawnPos.position;
        playerdead = false;
        yield return new WaitForSeconds(0.1f);
        bed.Use(player);
        //Debug.Log("ħ�����");
    }
    public void CheckIventoryItem(string name)
    {
        //Debug.Log("�ȴ�2");
        if (GameManager.Days == 0 && roomdoor != null)
        {
            if (name == "�ݱ�")
            {
                SoundManager.instance.PauseSound("GetItem");
                SoundManager.instance.PlaySound("GetSpecial");
            }
        }

        if (GameManager.Days == 1 && day1Door != null)
        {
            if (name == "�������")
            {
                SoundManager.instance.PauseSound("GetItem");
                SoundManager.instance.PlaySound("GetSpecial");
                day1Door.isCanUse = true;
            }
        }

        if (GameManager.Days == 2 && day2Door != null)
        {
            if (name == "����ū") { count++; if (count == 2) { day2Door.isCanUse = true; } }
            if (name == "����ū") { count++; if (count == 2) { day2Door.isCanUse = true; } }
        }

        if (GameManager.Days == 3 && day3Door != null)
        {
            if (name == "���̾ƹ���")
            {
                SoundManager.instance.PauseSound("GetItem");
                SoundManager.instance.PlaySound("GetSpecial");
                day3Door.isCanUse = true; 
            }
        }

        if (GameManager.Days == 4 && day4Door != null)
        {
            if (name == "������ū") { count++; if (count == 3) { day4Door.isCanUse = true; } }
            if (name == "��������ū") { count++; if (count == 3) { day4Door.isCanUse = true; } }
            if (name == "Alcohol") { count++; if (count == 3) { day4Door.isCanUse = true; } }
        }

        if (GameManager.Days == 5 && day5Door != null)
        {
            if (name == "�����̾�Ͱ���")
            {
                SoundManager.instance.PauseSound("GetItem");
                SoundManager.instance.PlaySound("GetSpecial");
                day5Door.isCanUse = true; 
            }
        }
    }
    /*
    public bool CheckInventory()
    {
        if (GameManager.Days != 5)
        {

        }
    }*/

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

    IEnumerator Day5Event()
    {
        while (true)
        {
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.stopResearch = true;
            enemy.navMeshAgent.speed = 10f;
            enemy.gameObject.GetComponent<SoundDetector>().isDetectOFF = true;
            enemy.navMeshAgent.SetDestination(new Vector3(11.15f, 5.57f, -41.88f));
            enemy.transform.rotation = Quaternion.Euler(0f, 90, 0);
            yield return new WaitForSeconds(1f);
            if (enemy.transform.position == new Vector3(11.15f, 5.57f, -41.88f)) break;

            yield return null;
        }
    }
}
