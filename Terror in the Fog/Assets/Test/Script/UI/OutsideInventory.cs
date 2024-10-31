using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject i_InventoryParent;
    [SerializeField]
    private GameObject o_InventoryParent;
    [SerializeField]
    private int o_InventoryCount = 5;

    private Slot[] i_slots;

    private Transform[] o_slots;

    private Image[] itemImages;

    private GameObject[] checkImages;

    [SerializeField]
    private GameObject player;

    public bool isCanUse = false;
    private int i_index;

    public IItem item;
    private void Start()
    {
        //�迭ũ�� �Ҵ�(�ʼ�)
        o_slots = new Transform[o_InventoryCount];
        itemImages = new Image[o_InventoryCount];
        checkImages = new GameObject[o_InventoryCount];

        //���� �κ��丮 ������Ʈ ��������
        i_slots = i_InventoryParent.GetComponentsInChildren<Slot>();

        //���� �κ��丮 Transform,������Ʈ ��������
        for (int i = 0; i < o_InventoryCount; i++)
        {
            o_slots[i] = o_InventoryParent.transform.GetChild(i).transform;
            itemImages[i] = o_slots[i].GetChild(2).GetComponent<Image>();
            checkImages[i] = o_slots[i].GetChild(1).gameObject;
            checkImages[i].SetActive(false);
        }
    }
    private void Update()
    {
        CopySlots();
    }

    //������ �̹����� ���� ����
    private void SetColor(float _alpha, int i)
    {
        Color color = itemImages[i].color;
        color.a = _alpha;
        itemImages[i].color = color;
    }

    //���� �κ��丮�� ���� �κ��丮�� ����
    public void CopySlots()
    {
        for (int i = 0; i < o_InventoryCount; i++)
        {
            if (i_slots[i] == null || itemImages == null)
                return;

            if (i_slots[i].isItemExist)
            {
                item = i_slots[i].item;
                itemImages[i].sprite = i_slots[i].itemImage.sprite;
                SetColor(1,i);

            }
            else
            {
                item = null;
                itemImages[i].sprite = null;
                SetColor(0,i);
            }
        }
    }
    //���콺��ũ�ѷ� ����� �̹��� ǥ��
    public void CheckCanUse(int index)
    {
        i_index = index;
        for (int i = 0; i < o_InventoryCount; i++)
        {
            if (i != index)                            //�κ��丮�� ��밡��ǥ��, �����ۻ�밡�� �ʱ�ȭ
            {
                checkImages[i].SetActive(false);

                if (i_slots[i].item == null)
                {
                    i_slots[i].isCanUse = false;
                }
            }
        }

        checkImages[index].SetActive(true);           //�κ��丮�� ��밡��ǥ��, �����ۻ�밡�� ǥ��

        if (i_slots[index].item != null)
        {
            item = i_slots[index].item;
            i_slots[index].isCanUse = true;
        }

    }

    public void UsingItem()
    {
        if (i_slots[i_index].isCanUse && item != null)
        {
            item.Use(player);
            StartCoroutine(Clearslot());
        }
        else
        {
            Debug.Log("�������� �����ϴ�");
        }
    }

    IEnumerator Clearslot()
    {
        yield return new WaitForSeconds(0.2f);

        if (item.type == IItem.ItemType.Consumed)
        {
            i_slots[i_index].ClearSlot();
        }
        if (item.type == IItem.ItemType.Used && item.isCanUse)
        {
            i_slots[i_index].ClearSlot();
        }
    }
}
