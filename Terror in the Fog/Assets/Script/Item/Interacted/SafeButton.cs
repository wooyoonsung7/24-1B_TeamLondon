using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class SafeButton : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public bool selected = false;
    public bool isCollect = false;
    public int ListIndex = 5;
    Vector3 currentPos;

    private Vector3 originalPosition;
    private Vector3 pressedPosition;

    private void Start()
    {
        currentPos = transform.position;
        type = ItemType.interacted;
        itemName = "�ݰ��ư";
        isCanUse = true;

        originalPosition = transform.localPosition;
        pressedPosition = originalPosition + new Vector3(-0.001f, 0, 0); // ���� ��ġ ���
    }
    public void Use(GameObject target)
    {
        if (!selected)
        {
            //��ư ������ ���ƿ��� �ִϸ��̼�
            transform.DOLocalMove(pressedPosition, 0.2f);
            selected = true;
        }
    }
    public void ResetValue()
    {
        transform.DOLocalMove(originalPosition, 0.2f); // ���� ��ġ�� ����
        //transform.position = currentPos;
    }
}