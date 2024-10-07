using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



[System.Serializable]
public class FloorDate
{
    public GameObject[] columns;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public FloorDate[] floorIndex;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    int moveIndex = 0;         //���ȣ�� ����ϴ� �ε���(�迭�� ����)
    int roomNumber = 4;        //���� �Ѽ�
    int currentroomNumber = 3; //�ֱ� ���� ����(����)
    public List<int> isDoneIdex = new List<int>();

    public enum ENEMYSTATE
    {
        OPENDOOR,
        ENTERROOM,
        LOOKAROUND,
        LOOKBACK,
        STOPACTION
    }

    public ENEMYSTATE enemystate;

    void Start()
    {
        instance = this; //�̱��� ���ϻ��
        StartCoroutine(ResetIdex());
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
        }
    }

    private void ChangeEnemyState(ENEMYSTATE newState)
    {
        enemystate = newState;
    }

    public void ResearchArea()
    {
        Debug.Log("ã�� �� ���");

        isDoneIdex.Remove(moveIndex);
        --currentroomNumber;
        int randomNumber = Random.Range(0, currentroomNumber);
        moveIndex = isDoneIdex[randomNumber];

        if (isDoneIdex.Count == currentroomNumber)
        {

        }

        if (currentroomNumber <= 0)
        {
            StartCoroutine(ResetIdex());
        }
    }

    public void OpenDoor()
    {
        moveToPos = floorIndex[0].columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
        ChangeEnemyState(ENEMYSTATE.ENTERROOM);
    }

    private void EnterRoom()
    {   
        moveToPos = floorIndex[0].columns[moveIndex + 4].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        int randomNumber = Random.Range(0, 4);
        if (randomNumber == 0)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKBACK);
        }
        else
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
        }
    }

    private void LookAround()
    {
        bool isEnd = false;
        Vector3 act_1 = transform.rotation.eulerAngles;
        act_1.y += 90f;
        Vector3 act_2 = transform.rotation.eulerAngles;
        act_1.y += 180f;
        Vector3 act_3 = transform.rotation.eulerAngles;
        act_1.y -= 90f;
        transform.DOLocalRotate(act_2, 2f).OnComplete(() => transform.DOLocalRotate(act_1, 2f).OnComplete(() => transform.DOLocalRotate(act_3, 2f).OnComplete(() => isEnd = true)));
        if (isEnd)
        {
            ResearchArea();
        }
    }
    private void LookBack()
    {

    }

    private void StopAction()
    {

    }

    IEnumerator ResetIdex()
    {
        currentroomNumber = roomNumber - 1;
        for (int i = 1; i <= roomNumber; i++)
        {
            isDoneIdex.Add(i);  //0~3������ �����ϴ� �ǵ�
        }
        yield return null;
    }
}
