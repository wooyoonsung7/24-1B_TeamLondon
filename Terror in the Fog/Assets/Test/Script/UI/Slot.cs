using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemImage;
    public IItem item;

    //������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //�κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(IItem _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;
        SetColor(1);
    }

    // �ش� ���� �ϳ� ����
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
