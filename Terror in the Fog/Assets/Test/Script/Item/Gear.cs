using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Gear : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "��Ϲ���";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;

        index = 6;
        isCanUse = false;          //���⿡�� isCanUse�� �ǹ� :  �������� ���(=������)�� �� �ִ°�?
    }
    public void Use(GameObject target)
    {
        Debug.Log("��Ϲ��� ���");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                if (handController.item.index == index)
                {
                    handController.item.isCanUse = true;
                    isCanUse = true;
                    Debug.Log("��������");
                }
                else
                {
                    Debug.Log("�������� ��ȿ���� �ʽ��ϴ�");
                }
            }
        }
    }
}
