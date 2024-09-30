using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class FlashLight : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    private void Start()
    {
        type = ItemType.Used;
        itemName = "손전등";
    }
    public void Use(GameObject target)
    {
        Debug.Log("손전등 사용");
    }
}
