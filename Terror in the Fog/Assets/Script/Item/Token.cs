using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Token : MonoBehaviour, IItem
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

    public int tokenIndex;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "��ū1";
        itemImage = _itemImage;
        isCanUse = false;
        index = tokenIndex;
        this.gameObject.layer = 6;

        StartCoroutine(SetName());
    }
    public void Use(GameObject target)
    {
        Debug.Log("��ū ���");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController.item != null)
        {
            if (handController.item.index >= 15 && handController.item.index < 20)
            {
                handController.item.isCanUse = true;
                isCanUse = true;
                handController.item.getIndex = index;
                handController.item.Use(target);

            }
            else
            {
                Debug.Log("�������� ��ȿ���� �ʽ��ϴ�");
            }

        }

    }

    private IEnumerator SetName()
    {
        if (_itemName != "")
        {
            itemName = _itemName;
        }
        yield return null;
    }
}
