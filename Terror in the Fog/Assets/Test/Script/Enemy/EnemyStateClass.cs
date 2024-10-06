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
public class Research : IState
{
    private static Research Instance = new Research();
    private Research() { }
    public static Research GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {
        if (enemy.navMeshAgent != null)
        {
            enemy.navMeshAgent.isStopped = false;
        }
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.ResearchArea();
        enemy.CheckObject();                 //�þ߿��� �÷��̾��
    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy != null)
        {
            if (enemy.hitTargetList.Count > 0)
            {
                enemy.stateMachine.SetState(enemy, JudgeChase.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {

    }
}

public class FeelStrage : IState
{
    private static FeelStrage Instance = new FeelStrage();
    private FeelStrage() { }
    public static FeelStrage GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {
        enemy.CheckAround();
    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy != null)
        {
            if (enemy.hitTargetList.Count == 0)
            {
                enemy.stateMachine.SetState(enemy, Research.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {

    }
}

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
            //�̻��� ���� �ִϸ��̼�
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
                if (canChase) //1�� ����Ŀ� ��������
                {
                    enemy.stateMachine.SetState(enemy, ChaseState.GetInstance());
                }
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        //���� �̻����� ������ �ִϸ��̼� ����
    }
}

public class ChaseState : IState
{
    private static ChaseState Instance = new ChaseState();
    private ChaseState() { }
    public static ChaseState GetInstance() { return Instance; }

    //�������� ����
    float findTime = 1.5f;               //�÷��̾ ������ ���, ��׷ΰ� Ǯ�� �������� �ð�
    float f_timer = 0f;
    float seizeRadius = 1f;

    //���� �ʾ��� ���� ����
    float chaseTime = 5f;
    float c_timer = 0f;

    PlayerController playerController;
    public void StateEnter(Enemy enemy)
    {
        playerController = enemy.playerController;
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.ChasePlayer();                  //�÷��̾ �Ѵ´�.
    }
    public void StateUpdate(Enemy enemy)
    {
        if (playerController.isHide)
        {
            f_timer += Time.deltaTime;
            if (findTime <= f_timer)
            {
                enemy.seizeRadius = 0.5f;         //����� �������� �ʱ�ȭ
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
            }
            else
            {
                enemy.seizeRadius = seizeRadius;  //����� �������� �ø���
                enemy.CheckDeath();
            }
        }
        else
        {
            enemy.CheckDeath();
            c_timer += Time.deltaTime;
            if (c_timer >= chaseTime)
            {
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        f_timer = 0f;                   //Ÿ�̸��ʱ�ȭ
    }
}