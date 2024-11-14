using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] voidSlot = new GameObject[5];
    [SerializeField]
    private GameObject drawer;

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
                Vector3 moveToPos = drawer.transform.position + transform.forward * 0.5f;
                moveToPos.y = drawer.transform.position.y;
                drawer.transform.DOMove(moveToPos, 3f);
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
