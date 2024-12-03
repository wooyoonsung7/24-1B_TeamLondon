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

    public bool isEnd = false; //����
    public bool isEnd_2 = false; //1������

    //� ������ ��������� ����


    //��Control��
    public bool isLookBack = false;

    public bool EventEnd = false;

    public int changeIndex = 0;

    private bool isRunning = false;
    private Coroutine currentCoroutine;

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
        isRunning = true;

        Debug.Log("���� 1" );
        moveToPos = columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
        if (isEnd_2)
        {
            StopSafeCoroutine();
            StartSafeCoroutine(); Debug.Log(moveIndex + "��°");
        }
        yield return new WaitUntil(() => isEnd); isEnd = false;

        while (isRunning)
        {
            moveIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                MoveToPos(); 
                if (isEnd_2)
                {
                    StopSafeCoroutine();
                    StartSafeCoroutine(); Debug.Log(moveIndex + "��°");
                }
                yield return new WaitUntil(() => isEnd); isEnd = false;
                //Vector3 currentRot = transform.localEulerAngles;
                //currentRot.y -= 90f;
                //transform.DOLocalRotate(currentRot, 0.2f);
                yield return new WaitForSeconds(5f);
            }
            yield return null;
        }
    }

    public IEnumerator DayOneEvent()
    {
        isRunning = true;
        isEnd_2 = false;
        moveIndex = 5;

        MoveToPos(); Debug.Log(moveIndex + "��°");
        yield return new WaitUntil(() => isEnd); isEnd = false;

        MoveToPos(); Debug.Log(moveIndex + "��°");
        yield return new WaitUntil(() => isEnd); isEnd = false;

        MoveToPos(); Debug.Log(moveIndex + "��°");
        yield return new WaitUntil(() => isEnd); isEnd = false;
        EnemyAnimation.instance.sequence.Restart();
        yield return new WaitUntil(() => isstepEnd);
        Debug.Log("���� 2��");
        yield return null; 
        StartSafeCoroutine();
    }

    public IEnumerator DayTwo()
    {
        isRunning = true;

        Debug.Log("0��°");
        moveToPos = columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
        yield return new WaitUntil(() => isEnd); isEnd = false;

        moveIndex = 0;
        while (isRunning)
        {
            Debug.Log("���ư���");
            moveIndex = 0;

            if (moveIndex == 0) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 1) yield return new WaitUntil(() => isEnd); isEnd = false;

            if (moveIndex == 1) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 2)
            {
                yield return new WaitUntil(() => isEnd); isEnd = false;
                yield return new WaitForSeconds(0.5f);
                EnemyAnimation.instance.sequence.Restart();
                yield return new WaitUntil(() => isstepEnd);
                isstepEnd = false;
            }

            if (moveIndex == 2) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 3) yield return new WaitUntil(() => isEnd); isEnd = false;


            if (moveIndex == 3) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 4)
            {
                Debug.Log("����2");
                yield return new WaitUntil(() => isEnd); isEnd = false;
                yield return new WaitForSeconds(0.5f);
                EnemyAnimation.instance.sequence2.Restart();
                yield return new WaitUntil(() => isstepEnd);
                isstepEnd = false;
            }

            if (moveIndex == 4) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 5) yield return new WaitUntil(() => isEnd); isEnd = false;

            if (moveIndex == 5) MoveToPos(); Debug.Log("�ε���" + moveIndex + "�ִϿ�?" + isEnd);
            if (moveIndex == 6)
            {
                Debug.Log("����3");
                yield return new WaitUntil(() => isEnd); isEnd = false;
                yield return new WaitForSeconds(0.5f);
                EnemyAnimation.instance.sequence.Restart();
                yield return new WaitUntil(() => isstepEnd);
                isstepEnd = false;
            }

            yield return null;
        }
    }
    public void StopSafeCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        isRunning = false;
    }
    public void StartSafeCoroutine()
    {
        if (isRunning)
        {
            StopSafeCoroutine();
        }
        ResetValue();
        if (GameManager.Days == 1)
        {
            if (isEnd_2)
            {
                currentCoroutine = StartCoroutine(DayOneEvent());
            }
            else
            {
                currentCoroutine = StartCoroutine(DayOne());
            }
        }
        if(GameManager.Days == 2) currentCoroutine = StartCoroutine(DayTwo());
    }

    public void ResetValue()
    {
        moveIndex = 0;
        isEnd = false;
        isstepEnd = false;
    }
}
