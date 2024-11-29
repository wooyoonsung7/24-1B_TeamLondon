using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidSlot_2 : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private int slotIndex = 0;

    [SerializeField]
    private GameObject[] itemPrf = new GameObject[5];

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUsing = false;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "�����";
        isCanUse = false;
        index = slotIndex;

        itemRot = Vector3.zero;
        itemRot.x -= 90f;
        itemRot.y += 180f;
        quaternion = Quaternion.Euler(itemRot);
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            isCanUse = false;
            isUsing = true;
            Debug.Log("�۵��Ѵ�");
            StartCoroutine(SetValuable());
        }
    }

    private IEnumerator SetValuable()
    {

        for (int i = 0; i < itemPrf.Length; i++)
        {
            Valuable item = itemPrf[i].GetComponent<Valuable>();
            if (getIndex == item.Index)
            {
                GameObject temp = Instantiate(itemPrf[i], transform.position - transform.forward * 0.05f, quaternion);
                Debug.Log("�����ȴ�");
            }
        }
        yield return new WaitForSeconds(0.2f);
        isUsing = false;
    }
}
