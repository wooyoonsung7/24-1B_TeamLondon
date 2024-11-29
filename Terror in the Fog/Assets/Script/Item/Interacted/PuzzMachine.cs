using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzMachine : MonoBehaviour
{
    [SerializeField]
    private VoidSlot[] voidSlots = new VoidSlot[5];
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
            foreach (VoidSlot obj in voidSlots)
            {
                if (obj.isCanUse)
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
                
                foreach(VoidSlot slot in voidSlots)
                {
                    slot.gameObject.layer = 0;
                }
                Token[] tokens = FindObjectsOfType<Token>();
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i].gameObject.layer = 0;
                    Debug.Log("�ȴ�");
                }
                
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
