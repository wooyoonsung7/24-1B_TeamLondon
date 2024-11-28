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
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private string _itemName;

    public int keyIndex = 0;
    private void Start()
    {
        type = ItemType.Used;
        itemName = _itemName;
        index = keyIndex;
        itemImage = _itemImage;
        isCanUse = false;          //���⿡�� isCanUse�� �ǹ� :  �������� ���(=������)�� �� �ִ°�?
    }
    public void Use(GameObject target)
    {
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                Debug.Log("���� ��ȣ��" + handController.item.index);
                Debug.Log("�����ȣ��" + index);

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
