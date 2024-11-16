using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Idle : IState
{
    private static Idle Instance = new Idle();
    private Idle() { }
    public static Idle GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {

    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {

    }
    public void StateExit(Enemy enemy)
    {

    }
}


//�÷��̾ ã�� ���� Ž���ϴ� ����
public class Research : IState
{
    private static Research Instance = new Research();
    private Research() { }
    public static Research GetInstance() { return Instance; }

    private bool isFindPlayer = true;
    private bool pauseResearch = false;
    private bool isOneTime = false;
    public void StateEnter(Enemy enemy)
    {

        if (enemy.navMeshAgent != null)                          //���� Ȱ���ٽ� ����
        {
            enemy.navMeshAgent.isStopped = false;
        }
        isFindPlayer = true;

        enemy.ResetSound();                                         //���尨�� ���ʱ�ȭ
        enemy.ResetResearch();                                      //Ž�� �� �ʱ�ȭ
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckObject();                                        //�þ߿��� �÷��̾��
        enemy.DetectToSound();                                      //���尨��
    }
    public void StateUpdate(Enemy enemy)
    {
        if (!pauseResearch)
        {
            enemy.ResearchArea();                                       //��Ž��
        }

        if (enemy != null)                                          
        {
            if (enemy.hitTargetList.Count > 0 && isFindPlayer)      //�÷����̾� �þ߳� ����(1����)
            {
                enemy.navMeshAgent.updateRotation = true;
                enemy.stateMachine.SetState(enemy, JudgeChase.GetInstance());
            }
            
            if (SoundDetector.instance.SoundPos.Count > 0)         //�÷��̾��� ���尨��(2����)
            {
                enemy.navMeshAgent.updateRotation = true;
                pauseResearch = true;
                isOneTime = true;
            }
            else
            {
                if (isOneTime)
                {
                    pauseResearch = false;
                    enemy.RestartSearch();
                    isOneTime = false;
                }
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        isFindPlayer = false;
    }
}


//�����ϴٰ� ������ ��, ������ �ѷ����� ����
public class FeelStrage : IState
{
    private static FeelStrage Instance = new FeelStrage();
    private FeelStrage() { }
    public static FeelStrage GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {
        enemy.navMeshAgent.updateRotation = false;
        enemy.navMeshAgent.isStopped = true;
        enemy.CheckAround();
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckObject();                                        //�þ߿��� �÷��̾��
    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy.hitTargetList.Count > 0)      //�÷����̾� �þ߳� ����(1����)
        {
            enemy.stateMachine.SetState(enemy, JudgeChase.GetInstance());
        }

        if (enemy != null && !enemy.isCheckAround)
        {
            if (enemy.hitTargetList.Count <= 0)
            {
                enemy.stateMachine.SetState(enemy, Research.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        enemy.navMeshAgent.updateRotation = true;
        enemy.navMeshAgent.isStopped = false;

        enemy.StartMove();                                          //ResearchManager_Simple�� �̵�����
    }
}



//�÷��̾ �߰��ϰ� �ѱ⸦ �Ǵ��ϴ� ����
public class JudgeChase : IState
{
    private static JudgeChase Instance = new JudgeChase();
    private JudgeChase() { }
    public static JudgeChase GetInstance() { return Instance; }

    private float detectTime = 1f;
    private float detectStartTime = 0f;
    public void StateEnter(Enemy enemy)
    {
        if (enemy != null)
        {
            
            enemy.navMeshAgent.isStopped = true;  //������ �ൿ ���� �ʱ�ȭ ����
            enemy.StopTween();                    //�������� �ִϸ��̼� ��� �ʱ�ȭ
            enemy.StopMove();                    //ResearchManager_Simple�� ����
            detectStartTime = Time.time;
            //enemy.FeelStrage() �߰�����
        }
    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy != null)
        {
            if (enemy.stateMachine.currentState != ChaseState.GetInstance())
            {
                bool canChase = Time.time >= detectTime + detectStartTime;
                if (canChase)                                                       //1�� ����Ŀ� ��������
                {
                    enemy.stateMachine.SetState(enemy, ChaseState.GetInstance());
                }
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        //���� �̻����� ������ �ִϸ��̼� ����
        enemy.navMeshAgent.isStopped = false;
    }
}


//���� �����ϴ� ����
public class ChaseState : IState
{
    private static ChaseState Instance = new ChaseState();
    private ChaseState() { }
    public static ChaseState GetInstance() { return Instance; }

    //�������� ����
    float findTime = 1.5f;                       //�÷��̾ ������ ���, ��׷ΰ� Ǯ�� �������� �ð�
    float f_timer = 0f;

    //���� �ʾ��� ���� ����
    float notChaseTime = 10f;

    PlayerController playerController;
    public void StateEnter(Enemy enemy)
    {
        playerController = enemy.playerController;
        enemy.navMeshAgent.isStopped = false;
        enemy.isFindPlayer = true;
    }

    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckObject();
        enemy.ChasePlayer();                      //�÷��̾ �Ѵ´�.
    }

    public void StateUpdate(Enemy enemy)
    {
        enemy.StopFinding();                      //�ü��ȿ� ������ �� ��Ÿ��

        if (playerController.isHide)
        {
            f_timer += Time.deltaTime;
            if (findTime <= f_timer)
            {
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
                Debug.Log("���� ����");
            }
            else
            {
                enemy.CheckDeath();
                Debug.Log("���� ����");
            }
        }
        else
        {
            enemy.CheckDeath();
            if (enemy.timer >= notChaseTime)
            {
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
            }
        }
    }

    public void StateExit(Enemy enemy)
    {
        f_timer = 0f;                              //Ÿ�̸��ʱ�ȭ
        enemy.timer = 0f;
        enemy.isFindPlayer = false;
    }
}
