using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using static UnityEngine.GraphicsBuffer;

public class BookCase : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public Transform[] slotPositions; // ���� ���� ��ġ
    [SerializeField] private GameObject[] generatedItem;
    
    private bool OneTime = true;
    private int count = 0;

    [SerializeField]
    private GameObject generatedItem_2;
    void Start()
    {
        type = ItemType.interacted;
        itemName = "å��";
        isCanUse = false;
        index = 5;

        StartCoroutine("CheckPuzz"); //������ ����� ���ؼ� �ڷ�ƾ���� ����
    }

    public void Use(GameObject target)
    {
        if (isCanUse && OneTime)
        {
            OneTime = false;
            StartCoroutine(UseItem(target));
        }
    }

    IEnumerator UseItem(GameObject target)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject temp = Instantiate(generatedItem[count], target.transform.position, Quaternion.identity);
        SoundManager.instance.PlaySound("SetBook");
        if (temp != null)
        {
            temp.GetComponent<Collider>().enabled = false;
            temp.GetComponent<BookController>().setPlace = slotPositions[count];
            temp.GetComponent<BookController>().isPlaced = true;
            temp.gameObject.layer = 0;
        }

        count++;
        index++;
        if (count >= 3) gameObject.layer = 0;
        Debug.Log(index);
        OneTime = true;
    }
    
    IEnumerator CheckPuzz()
    {
        while (true)
        {

            if (count >= 3)
            {
                Debug.Log("�����");
                SoundManager.instance.PlaySound("FalledKey");
                Vector3 itemPos = transform.position - transform.right * 0.8f;
                //itemPos.y -= (transform.localScale.y / 2 - generatedItem_2.transform.localScale.y / 2);

                Vector3 itemRot = Vector3.zero;
                itemRot.z += 90f;
                Quaternion quaternion = Quaternion.Euler(itemRot);

                Instantiate(generatedItem_2, itemPos, quaternion);
                StopCoroutine("CheckPuzz");
            }

            yield return null;
        }
    }
}
