using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Bed : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private Door door;
    private void Awake()
    {

    }
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Bed";
        isCanUse = true;
    }

    public void Use(GameObject target)
    {
        Debug.Log("���õ�");
        if (isCanUse)
        {
            Debug.Log("ħ����");
            isCanUse = false;

            //�ڰ� �Ͼ�� �ִϸ��̼�
            //����ý����߰�
            door.isCanUse = true;
            GameManager.Instance.PassDay();
        }
    }
}
