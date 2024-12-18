using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static IItem;

public class Chest : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private OpenBox chestCap;
    [SerializeField]
    private GameObject knife;
    [SerializeField]
    private GameObject Locked;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "����";
        isCanUse = false;
        index = 4;

        if(knife != null) knife.layer = 0;
        if (GameManager.Days == 4) StartCoroutine("CheckUse");
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            chestCap.ToggleBox();
            SoundManager.instance.PlaySound("OpenBox");
            knife.layer = 6;
            gameObject.layer = 0;
        }
        else
        {
            SoundManager.instance.PlaySound("CanNotOpen");
        }
    }

    private IEnumerator CheckUse()
    {
        while (true)
        {
            if (isCanUse)
            {
                Locked.SetActive(false);
                StopCoroutine("CheckUse");
            }
            yield return null;
        }
    }
}
