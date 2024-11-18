using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class SafeButton : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    public bool selected = false;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "�ݰ��ư";
    }
    public void Use(GameObject target)
    {
        //��ư ������ ���ƿ��� �ִϸ��̼�
        transform.position = - transform.right * 0.1f;
        selected = true;
    }
}