using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] voidSlot = new GameObject[5];
    [SerializeField]
    private Drawer drawer;

    private int stack = 0;
    void Start()
    {
        StartCoroutine("CheckPuzz"); //������ ����� ���ؼ� �ڷ�ƾ���� ����
    }

    IEnumerator CheckPuzz()
    {
        while (true)
        {
            foreach (GameObject obj in voidSlot)
            {
                IItem item = obj.GetComponent<IItem>();
                if (item.isCanUse)
                {
                    Debug.Log(stack);
                    stack++;
                }
            }
            if (stack >= 5)
            {
                Debug.Log("�����");
                drawer.isCanUse = true;
                drawer.Use(gameObject);
                StopCoroutine("CheckPuzz");
            }
            else
            {
                stack = 0;
            }

            yield return null;
        }
    }
}
