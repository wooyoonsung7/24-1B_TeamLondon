using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�÷��̾ ã�� ���� Ž���ϴ� ����
public class Research : IState
{
    private static Research Instance = new Research();
    private Research() { }
    public static Research GetInstance() { return Instance; }

    private bool isFindPlayer = true;
    private bool pauseResearch = false;
    private bool isOneTime = false;
    public bool isOneTimeInGame = true;
    public void StateEnter(Enemy enemy)
    {

        if (enemy.navMeshAgent != null) enemy.navMeshAgent.isStopped = false;                          //���� Ȱ���ٽ� ����
        isFindPlayer = true;
        pauseResearch = false;
        isOneTime = false;                                          //���尨���ʱ�ȭ

        enemy.ResetSound();                                         //���尨�� ���ʱ�ȭ
        if (isOneTimeInGame)
        {
            enemy.ResetResearch();                                  //Ž�� �� �ʱ�ȭ
            isOneTimeInGame = false;
        }
        enemy.RestartSearch();                                      //Ž���ֱ�ȭ

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
        else
        {
            enemy.StopMove();                                          //ResearchManaget_Simple��
            enemy.StopTween();
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
                    enemy.StartMove();                             //ResearchManaget_Simple��
                    isOneTime = false;
                }
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        isFindPlayer = false;
        enemy.ResetSound();                                         //���尨�� ���ʱ�ȭ
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
        SoundManager.instance.PlaySound("EnemyDoubt"); //�ǽ��ϴ� �����߰�
       
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckAround();
        enemy.CheckObject();                                        //�þ߿��� �÷��̾��
    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy.hitTargetList.Count > 0)      //�÷����̾� �þ߳� ����(1����)
        {
            //Debug.Log("�ٲ��");
            enemy.stateMachine.SetState(enemy, JudgeChase.GetInstance());
        }
        if (enemy != null && !enemy.isCheckAround)
        {
            if (enemy.hitTargetList.Count <= 0)
            {
                enemy.StartMove();                                          //ResearchManager_Simple�� �̵�����
                enemy.stateMachine.SetState(enemy, Research.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        enemy.navMeshAgent.updateRotation = true;
        enemy.navMeshAgent.isStopped = false;
        enemy.currentTime = 0f;                                     //�̻��� �����ִϽð� �ʱ�ȭ
        SoundManager.instance.PauseSound("EnemyDoubt"); //�ǽ��ϴ� �����߰�
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
            SoundManager.instance.PlaySound("EnemyDetect"); //�߰��Ǵ� �����߰�
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
    float findTime = 2f;                       //�÷��̾ ������ ���, ��׷ΰ� Ǯ�� �������� �ð�
    float f_timer = 0f;

    //���� �ʾ��� ���� ����
    float notChaseTime = 10f;

    PlayerController playerController;
    bool isOneTime = false;
    public void StateEnter(Enemy enemy)
    {
        playerController = enemy.playerController;
        enemy.isFindPlayer = true;
        SoundManager.instance.PlaySound("EnemyChase");  //�ѱ�� ���
        isOneTime = true;
    }

    public void StateFixUpdate(Enemy enemy)
    {
        //enemy.CheckObject();
        enemy.ChasePlayer();                      //�÷��̾ �Ѵ´�.
    }

    public void StateUpdate(Enemy enemy)
    {
        enemy.StopFinding();                      //�ü��ȿ� ������ �� ��Ÿ��

        if (playerController.isHide)
        {
            if (isOneTime)
            {
                f_timer += Time.deltaTime;
            }

            if (findTime <= f_timer)
            {
                if (!enemy.isOneTime4) return;
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
                //Debug.Log("���� ����");
                isOneTime=false; 
            }
            else
            {
                enemy.CheckDeath();
                //Debug.Log("���� ����");
            }
        }
        else
        {
            enemy.CheckDeath();
            if (!enemy.isOneTime4) return;
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
        SoundManager.instance.PauseSound("EnemyChase");  //�ѱ�� ���_��]
        enemy.hitTargetList.Clear();
    }
}
