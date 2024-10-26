using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidSlot : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private int slotIndex = 0;

    [SerializeField]
    private GameObject[] tokenPrf = new GameObject[5];


    private void Start()
    {
        type = ItemType.interacted;
        itemName = "��å��";
        isCanUse = false;
        index = slotIndex;
    }
    public void Use(GameObject target)
    {
        SetToken();
        CheckCorrect();
    }

    private void SetToken()
    {
        Vector3 itemRot = Vector3.zero;
        itemRot.x += 72f;
        Quaternion quaternion = Quaternion.Euler(itemRot);
        for (int i = 0; i< tokenPrf.Length; i++)
        {
            Token token = tokenPrf[i].GetComponent<Token>();
            if (getIndex == token.tokenIndex)
            {
                Instantiate(tokenPrf[i], transform.position + transform.forward * 0.05f, quaternion);
            }
        }
    }

    private void CheckCorrect()
    {
        if (getIndex == index)
        {
            isCanUse = true;
        }
        else
        {
            isCanUse = false;
        }
    }
}