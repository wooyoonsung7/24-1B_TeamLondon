using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    private int count = 0;
    private SafeButton[] safeButtons;
    void Start()
    {
        StartCoroutine("CheckPuzz"); //������ ����� ���ؼ� �ڷ�ƾ���� ����
    }
    IEnumerator CheckPuzz()
    {
        for (int i = 0; i < 9; i++)
        {/*
            switch (i)
            {
                case 0:
                    count++;
                case 1;
                    count++;
                case 2:
                    count++;
                case 3:
                    count++;
                case 4:

                case 5:
                case 6:
                case 7:
                case 8:
                    break;

            }*/
        }
        yield return null;
    }
}
