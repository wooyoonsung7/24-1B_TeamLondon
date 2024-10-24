using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCase : MonoBehaviour
{
    public GameObject[] voidCase = new GameObject[3];

    private int stack = 0;
    [SerializeField]
    private GameObject generatedItem;
    void Update()
    {
        CheckPuzz(); //������ ����� ���ؼ� �ڷ�ƾ���� ����
    }

    private void CheckPuzz()
    {
        foreach (GameObject obj in voidCase)
        {
            IItem item = obj.GetComponent<IItem>();
            if (item.isCanUse)
            {
                stack++;
            }
        }
        if (stack >= 3)
        {
            Debug.Log("�����");
            Vector3 itemPos = transform.position + transform.forward * 0.3f;
            itemPos.y -= (transform.localScale.y / 2 - generatedItem.transform.localScale.y / 2);

            Vector3 itemRot = Vector3.zero;
            itemRot.y += 55f;
            Quaternion quaternion = Quaternion.Euler(itemRot);

            Instantiate(generatedItem, itemPos, quaternion);
        }
        else
        {
            stack = 0;
        }
    }
}
