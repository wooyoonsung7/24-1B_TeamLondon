using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class StaminaPack : MonoBehaviour, IItem
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
        type = ItemType.Consumed;
        itemName = "���׹̳�ȸ����";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
    }
    public void Use(GameObject target)
    {
        Debug.Log("���׹̳�ȸ���� ���");
        PlayerController p_Controller = target.GetComponent<PlayerController>();
        if (p_Controller != null)
        {
            p_Controller.s_slider.value = 1;
            p_Controller.isFadeOut = true;
        }
    }
}
