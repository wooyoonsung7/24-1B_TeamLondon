using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ResearchManager;

public class ResearchManager_Simple : MonoBehaviour
{
    public static ResearchManager_Simple instance;

    public GameObject[] columns;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    public int moveIndex = 0;             //���ȣ�� ����ϴ� �ε���(�迭�� ����)

    //�������� �̵����� �������� �̵��� ���� ����
    public float changeTime = 0f;

    public bool isstepEnd = false;
    private bool isOneTime = false;

    public bool isEnd = false; //1������

    //� ������ ��������� ����


    //��Control��
    public bool isLookBack = false;

    public bool EventEnd = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //�̱��� ���ϻ��
        }
    }

    private IEnumerator Tuto()
    {
        MoveToPos();
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2();

        yield return new WaitForSeconds(CheckTime(moveToPos) + 6f * Time.deltaTime);

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {

            EnemyAnimation.instance.sequence.Restart();
        }
        yield return new WaitUntil(() => isstepEnd);

        yield return StartCoroutine("GoBack");
    }

    public IEnumerator GoBack()
    {
        isOneTime = true; //����Ȱ��ȭ-------------------------------------�ٽ� ���ư��� �ڵ�
        moveIndex = 2;
        moveToPos = columns[--moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        yield return new WaitForSeconds(CheckTime(moveToPos) + 6f * Time.deltaTime);

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {

            moveToPos = columns[--moveIndex].transform.position;
            enemy.navMeshAgent.SetDestination(moveToPos);
            OFFState();
        }

        yield return new WaitForSeconds(CheckTime(moveToPos));

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {
            enemy.gameObject.SetActive(false);
            EventEnd = true;
        }
    }
    private void OFFState()
    {
        //enemy.enabled = false;
        enemy.GetComponent<SoundDetector>().isDetectOFF = true;
    }

    private float CheckTime(Vector3 moveToPos)
    {
        Vector3 currentPos;
        if (isOneTime) currentPos = columns[moveIndex + 1].transform.position;
        else currentPos = columns[moveIndex - 1].transform.position;

        float dis = Vector3.Distance(currentPos, moveToPos);                    //�����ΰ� �̵��� ��ġ������ �Ÿ�
        float time = (int)dis / enemy.navMeshAgent.speed + 14 * Time.deltaTime;

        return time;
    }

    private void MoveToPos()
    {
        moveToPos = columns[++moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
    }
    private void MoveToPos_2()
    {
        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {
            MoveToPos();
        }
    }
    public IEnumerator DayOne()
    {
        while (true)
        {
            moveIndex = 0;
            Debug.Log("111");
            MoveToPos(); if (isEnd) yield return StartCoroutine(DayOneEvent());
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);  //�ִϸ��̼ǽð� 5��(�ӽ�) + (�ִϸ��̼� ����)
                MoveToPos_2(); if (isEnd) yield return StartCoroutine(DayOneEvent());
            }
            yield return null;
            moveIndex = 0;
            yield return null;
        }
    }

    public IEnumerator DayOneEvent()
    {
        int temp = 7;
        moveIndex = 5;

        MoveToPos();
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(CheckTime(moveToPos));
            MoveToPos_2();
        }
        yield return new WaitForSeconds(CheckTime(moveToPos));
        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f) moveIndex++;
        if(moveIndex >= 9) EnemyAnimation.instance.sequence.Restart();
        yield return new WaitUntil(() => isstepEnd);
        
        moveIndex = temp;
        MoveToPos();
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(CheckTime(moveToPos));
            --temp; moveIndex = temp;
            MoveToPos_2();
        }
        yield return null;
        if (moveIndex <=6) isEnd = false; yield return StartCoroutine(DayOne());
    }

    public IEnumerator DayTwo()
    {
        moveIndex = 0;
        MoveToPos(); Debug.Log("1��°");
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2(); Debug.Log("2��°");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        MoveToPos_2(); Debug.Log("3��°");
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2(); Debug.Log("4��°");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        MoveToPos_2(); Debug.Log("5��°");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 25f * Time.deltaTime);
        MoveToPos_2(); Debug.Log("6��°");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);

        while (true)
        {
            moveIndex = 0;
            MoveToPos_2(); Debug.Log("1��°");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 80f * Time.deltaTime);
            MoveToPos_2(); Debug.Log("2��°");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
            MoveToPos_2(); Debug.Log("3��°");
            yield return new WaitForSeconds(CheckTime(moveToPos));
            MoveToPos_2(); Debug.Log("4��°");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
            MoveToPos_2(); Debug.Log("5��°");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 25f * Time.deltaTime);
            MoveToPos_2(); Debug.Log("6��°");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        }
    }
}
