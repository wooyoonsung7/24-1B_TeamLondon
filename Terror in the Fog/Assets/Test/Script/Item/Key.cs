using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static IItem;

public class Key : MonoBehaviour, IItem
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

    public int keyIndex = 0;
    private void Start()
    {
        type = ItemType.Used;
        itemName = "����";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = false;          //���⿡�� isCanUse�� �ǹ� :  �������� ���(=������)�� �� �ִ°�?
    }
    public void Use(GameObject target)
    {
        Debug.Log("���� ���");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                if (handController.item.index == keyIndex)
                {
                    handController.item.isCanUse = true;
                    isCanUse = true;
                    Debug.Log("������");
                }
                else
                {
                    Debug.Log("���谡 ��ȿ���� �ʽ��ϴ�");
                }
            }
        }
    }
}
