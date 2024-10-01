using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class HealPack : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }
    private void Start()
    {
        type = ItemType.Used;
        itemName = "ü��ȸ����";
    }
    public void Use(GameObject target)
    {
        Debug.Log("ü��ȸ���� ���");
    }
}
