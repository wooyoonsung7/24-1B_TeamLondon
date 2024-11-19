using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class FloorDate
{
    public GameObject[] columns;
}

public class ResearchManager : MonoBehaviour
{
    public static ResearchManager instance;

    public FloorDate[] floorIndex;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    int moveIndex = 0;         //���ȣ�� ����ϴ� �ε���(�迭�� ����)
    int roomNumber = 4;        //���� �Ѽ�
    int floorNumber = 0;
    int currentroomNumber = 4; //�ֱ� ���� ����(����)
    [SerializeField] private List<int> isDoneIdex = new List<int>();  //�����ϰ� ���ڸ� �־��ֱ� ���� ����;
    
    //ResetIndex()�� ���� �����Ұ�
    bool r_IsEnd = false;
    //�������� �̵����� �������� �̵��� ���� ����
    public float changeTime = 0f;
    float waitTime = 2f;

    public int stepNumber = 0;

    bool isOneTime = false;

    Vector3 dir;
    bool isheight = true;

    //���Ÿ�� ����� ����
    Vector3 hidePos;
    Vector3 lookPos;
    float lookTime = 10f;
    float lookTimer = 0f;
    
    //�� Control��
    public bool isLookBack = false;
    public bool isdoorLocked = false;

    //��乮 �����¿�
    public float timer = 0f;
    public enum ENEMYSTATE
    {
        OPENDOOR,
        ENTERROOM,
        LOOKAROUND,
        LOOKBACK,
        STOPACTION,
        CHANGEROOM
    }

    public ENEMYSTATE enemystate;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //�̱��� ���ϻ��
        }
    }
    void Start()
    {
        changeTime = Time.time;
    }

    public void RESEARCH()
    {
        switch (enemystate)
        {

            case ENEMYSTATE.OPENDOOR:
                OpenDoor();
                break;

            case ENEMYSTATE.ENTERROOM:
                EnterRoom();
                break;

            case ENEMYSTATE.LOOKAROUND:
                LookAround();
                break;

            case ENEMYSTATE.LOOKBACK:
                LookBack();
                break;

            case ENEMYSTATE.STOPACTION:
                StopAction();
                break;

            case ENEMYSTATE.CHANGEROOM:
                ChangeRoom();
                break;
        }
    }

    public void ChangeEnemyState(ENEMYSTATE newState)
    {
        enemystate = newState;
    }

    private void ChangeRoom()
    {
        Debug.Log("�� ��ü");
        stepNumber = 0;
        //Debug.Log("�������� " + currentroomNumber);
        isDoneIdex.Remove(moveIndex);
        --currentroomNumber;
        int randomNumber = Random.Range(0, currentroomNumber);

        if (currentroomNumber > 0)
        {
            moveIndex = isDoneIdex[randomNumber];
            ChangeEnemyState(ENEMYSTATE.OPENDOOR);
            changeTime = Time.time;
        }
        else
        {
            int randomNumber2 = Random.Range(0, 3);
            if (randomNumber2 == 0)
            {
                ChangeEnemyState(ENEMYSTATE.STOPACTION);
                changeTime = Time.time;
                stepNumber = 3;
            }
            else
            {
                ChangeFloor(false);
            }
        }
    }

    private void OpenDoor()
    {
        //Debug.Log(1);
        //Debug.Log("��¥ ���� " + floorNumber);
        //Debug.Log("�ٲ� ���� ������" + roomNumber);

        moveToPos = floorIndex[floorNumber].columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && Time.time >= changeTime + waitTime)
        {
            changeTime = Time.time;
            ChangeEnemyState(ENEMYSTATE.ENTERROOM);
            isOneTime = true;
            timer = 0f; //Ÿ�̸��ʱ�ȭ
        }
    }
    private void EnterRoom()
    {
        timer += Time.deltaTime; //��乮Ȯ�ο�             --------���߿� ��乮�� �ε����� �׷��� ����-------------
        if (timer < 0.6f) return;

        //Debug.Log(2);
        moveToPos = floorIndex[floorNumber].columns[moveIndex + roomNumber].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        dir = (moveToPos - enemy.transform.position).normalized;
        if (isOneTime)
        {
            if (dir.z > 0)
            {
                isheight = true;
            }
            else
            {
                isheight = false;
            }
            isOneTime = false;
        }

        int randomNumber = Random.Range(0, 3);

        if (randomNumber == 0)
        {
            isLookBack = true;
        }
        else
        {
            isLookBack = false;
        }
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && Time.time >= waitTime + changeTime)
        {
            if (randomNumber == 0)
            {
                ChangeEnemyState(ENEMYSTATE.LOOKBACK);
                isOneTime = true;
            }
            else
            {
                ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
                isOneTime = true;
            }
            enemy.navMeshAgent.updateRotation = false;
        }
    }
    private void LookAround()
    {
        //Debug.Log(3);
        if (isOneTime)
        {
            if (isheight)
            {
                EnemyAnimation.instance.sequence.Restart();
            }
            else
            {
                EnemyAnimation.instance.sequence2.Restart();
            }
            isOneTime = false;
        }
        if (stepNumber == 1)
        {
            enemy.navMeshAgent.updateRotation = true;
            ChangeEnemyState(ENEMYSTATE.CHANGEROOM);
        }
    }
    private void LookBack()
    {
        //Debug.Log(4);
        if (isOneTime)
        {
            if (isheight)
            {
                EnemyAnimation.instance.sequence3.Restart();
                //stepNumber = 2;
            }
            else
            {
                EnemyAnimation.instance.sequence4.Restart();
                //stepNumber = 2;
            }
            isOneTime = false;
        }

        if (stepNumber == 2)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
            isOneTime = true;
            isLookBack = false;
        }
    }

    private void StopAction()
    {
        //Debug.Log(5);
        if (stepNumber == 3)
        {
            int randomNum = Random.Range(0, roomNumber - 1);
            hidePos = floorIndex[floorNumber + 2].columns[randomNum].transform.position;
            lookPos = floorIndex[floorNumber].columns[randomNum].transform.position;
            stepNumber = 4;
        }

        if (stepNumber == 4)
        {
            enemy.navMeshAgent.SetDestination(hidePos);
        }
        if (enemy.navMeshAgent.remainingDistance <= 0.1f && Time.time >= waitTime + changeTime)
        {
            enemy.gameObject.transform.DOLookAt(lookPos, 1f);

            lookTimer += Time.deltaTime;
            if (lookTime <= lookTimer)
            {
                ChangeFloor(false);
                lookTimer = 0f;
            }
        }
    }

    public void ChangeFloor(bool isFisrt)
    {
        if (isFisrt)
        {
            stepNumber = 7;
            roomNumber = floorIndex[floorNumber].columns.Length / 2;
            r_IsEnd = false;
            ResetIndex();
        }
        else
        {
            if (floorNumber == 0 && stepNumber != 7)
            {
                stepNumber = 7;

                floorNumber = 1;
                roomNumber = floorIndex[floorNumber].columns.Length / 2;
                //Debug.Log("���� ������" + roomNumber);
                //Debug.Log("���� " + floorNumber);
                r_IsEnd = false;
                ResetIndex();
            }
            else if (floorNumber == 1 && stepNumber != 7)
            {
                stepNumber = 7;
                floorNumber = 0;
                roomNumber = floorIndex[floorNumber].columns.Length / 2;

                //Debug.Log("���� ������" + roomNumber);
                //Debug.Log("���� " + floorNumber);
                r_IsEnd = false;
                ResetIndex();
            }
        }
    }

    private void ResetIndex()
    {
        Debug.Log("�ʱ�ȭ");
        isDoneIdex.Clear();
        if (!r_IsEnd)
        {
            currentroomNumber = roomNumber;
            for (int i = 0; i < roomNumber; i++)
            {
                isDoneIdex.Add(i);  //0~3������ �����ϴ� �ǵ�
            }
            r_IsEnd = true;
            Debug.Log(isDoneIdex.Count);
        }
        if (isDoneIdex.Count == roomNumber)
        {
            ChangeEnemyState(ENEMYSTATE.OPENDOOR);
            changeTime = Time.time;
        }
    }
}
