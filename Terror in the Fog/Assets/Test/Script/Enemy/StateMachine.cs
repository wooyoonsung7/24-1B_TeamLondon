using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void StateEnter(Enemy enemy);     //��������
    void StateFixUpdate(Enemy enemy); //������(��������, ����) ������Ʈ
    void StateUpdate(Enemy enemy);    //������ ������Ʈ
    void StateExit(Enemy enemy);      //������ ������
}
public class StateMachine
{
    public IState currentState;

    public void Statemachine(Enemy enemy, IState defaultState)
    {
        currentState = defaultState;
        currentState.StateEnter(enemy);
    }
    public void SetState(Enemy enemy, IState state)
    {
        if (currentState == null || currentState == state)
        {
            Debug.Log("���º���Ұ�");
            return;
        }

        currentState.StateExit(enemy);
        currentState = state;
        //�÷��̾� ����Ÿ�Ժ���
        //������� ����
        currentState.StateEnter(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        currentState.StateUpdate(enemy);
    }

    public void FixedUpdateState(Enemy enemy)
    {
        currentState.StateFixUpdate(enemy);
    }
}
