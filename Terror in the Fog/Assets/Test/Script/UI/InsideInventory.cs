using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class InsideInventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //�κ��丮�� ���Ե鿡�� ����� ���������� ����
    [SerializeField]
    private GameObject go_InsideInventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    //�θ��丮�� ������ ��, �÷��̾������� ���߱� ���� ����
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;
    [SerializeField]
    private GameObject outsideInventory;
    [SerializeField]
    private GameObject staminaSlider;

    public bool isFull = false;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetColor(0);
        }
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                InventoryAcited();
            }
            else
            {
                InventoryUnactivated();
            }
        }
    }

    private void InventoryAcited()
    {
        go_InsideInventoryBase.SetActive(true);
        Cursor.lockState = CursorLockMode.None;              //���콺���� Ȱ��ȭ
        Cursor.visible = true;
        playerController.enabled = false;                    //ĳ�������� ��Ȱ��ȭ
        handController.enabled = false;
        outsideInventory.SetActive(false);
        staminaSlider.SetActive(false);
    }

    private void InventoryUnactivated()
    {
        go_InsideInventoryBase.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;           //���콺���� ��Ȱ��ȭ
        Cursor.visible = false;
        playerController.enabled = true;                    //ĳ�������� ��Ȱ��ȭ
        handController.enabled = true;
        outsideInventory.SetActive(true);
        staminaSlider.SetActive(true);
    }

    public void AcuquireItem(IItem _item)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public void UsedItem(IItem _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].ClearSlot();
                    return;
                }
            }
        }
    }

    public void CheckSlotFull()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                count++;
            }
        }
        if (count >= 9)
        {
            isFull = true;
            Debug.Log("�κ��丮�� ���� á���ϴ�");
        }
    }
}
