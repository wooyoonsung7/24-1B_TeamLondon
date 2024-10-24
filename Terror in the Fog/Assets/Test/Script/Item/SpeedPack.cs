using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class SpeedPack : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; } // �������� ��밡�������� �Ǵ�

    

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;
    [SerializeField]
    private float speedUpValue = 1.5f;
    private void Start()
    {
        type = ItemType.Consumed;
        itemName = "�ӵ���";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = true;
    }

    public void Use(GameObject target)
    {
        Debug.Log("�ӵ��� ���");
        PlayerController p_Controller = target.GetComponent<PlayerController>();

        p_Controller.moveSpeed *= speedUpValue;
        p_Controller.crouchSpeed *= speedUpValue;
        p_Controller.runSpeed *= speedUpValue;
        p_Controller.walkSpeed *= speedUpValue;
    }
}
