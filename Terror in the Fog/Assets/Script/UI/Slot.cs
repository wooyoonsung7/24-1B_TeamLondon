using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image itemImage;
    public IItem item;

    //������������ OutsideInventory��ũ��Ʈ�� �������� ���� ����
    public bool isItemExist = false;

    public bool isCanUse = false;


    public void Update() //��ӵǴ� üŷ
    {
        if (item != null)
        {
            isItemExist = true;
        }
        else
        {
            isItemExist = false;
        }
    }
    //������ �̹����� ���� ����
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //�κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(IItem _item, bool _isItemExist = true)
    {
        //�ٱ��κ��丮�� ������ �̹��� Ȱ��ȭ, ����
        isItemExist = _isItemExist;

        //���� ������ Ȱ��ȭ
        item = _item;
        itemImage.sprite = item.itemImage;
        SetColor(1);
    }

    // �ش� ���� �ϳ� ����
    public void ClearSlot(bool _isItemExist = false)
    {
        //�ٱ��κ��丮�� ������ �̹��� ��Ȱ��ȭ, �������
        isItemExist = _isItemExist;

        //���� ������ ��Ȱ��ȭ
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
