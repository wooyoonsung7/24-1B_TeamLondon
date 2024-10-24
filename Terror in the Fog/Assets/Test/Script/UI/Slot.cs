using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemImage;
    public IItem item;
    public string slotID;

    //������������ OutsideInventory��ũ��Ʈ�� �������� ���� ����
    public bool isItemExist = false;

    public bool isCanUse = false;

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

    //���콺 �巡�װ� ���� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("�ȴ�0");
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;

            Debug.Log("�ȴ�1");        
        }
    }

    // ���콺 �巡�� ���� �� ��� �߻��ϴ� �̺�Ʈ
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;

            Debug.Log("�ȴ�2");
        }
    }

    //���콺 �巡�װ� ������ �� �߻��ϴ� �̺�Ʈ
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        //SetColor(1);
    }

    //�ش� ���Կ� ���𰡰� ���콺 ��� ���� �� �߻��ϴ� �̺�Ʈ
    //�巡�� ����� �ƴϴ� ��� ��ġ ����� �� ���Կ��� ȣ��ȴ�.
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot() // ��: �ٲ� ������ = A, �ٲ� ������ = B
    {
        IItem _item = item; //A�� ��� ����

        AddItem(DragSlot.instance.dragSlot.item); //A��ſ� B�� �ִ´�.

        if (_item != null)
        {
            DragSlot.instance.dragSlot.AddItem(_item); //�����ߴ� A�� B ��ſ� �ִ´�.
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot(); //A�� ���� ���, ����.
            DragSlot.instance.dragSlot = null;
        }
    }
}
