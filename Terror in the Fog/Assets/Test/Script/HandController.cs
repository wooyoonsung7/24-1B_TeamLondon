using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 1f;

    private bool pickupActivated = false;

    public LayerMask whatIsTarget;

    private RaycastHit hitInfo;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private InsideInventory theInventory;
    private OutsideInventory theInventory_2;

    [SerializeField]
    private float scrollSpeed = 2f;
    float scrollPoint = 0f;
    int i = 0;

    IItem item;

    private void Awake()
    {

    }
    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();
        theInventory_2 = FindObjectOfType<OutsideInventory>();
    }

    void Update()
    {
        CheckItem();
        TryPickUp();
        SellectItem();
        UseItem();
    }
        
    private void TryPickUp() //������ �ݱ�
    {
        if (Input.GetButtonDown("Interaction"))
        {
            CheckItem();
            CanPickUp();
            Interaction();
        }
    }
    private void CheckItem() //���������� Ȯ��
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget)) //�ش� �������� ���������� �ν�
        {

            item = hitInfo.collider.GetComponent<IItem>();

            ItemInfoAppear();
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //���������� ǥ�� �ϱ�
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "��ȭ�ۿ�" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //���������� ǥ�� ����
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp() //������ ȹ��
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                if (item.type == IItem.ItemType.Used || item.type == IItem.ItemType.Consumed)
                {
                    Debug.Log(item.itemName + " ȹ�� �߽��ϴ�.");
                    theInventory.AcuquireItem(item);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();

                }
            }
        }
    }
    public void SellectItem()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        if (scroll != 0)
        {
            scrollPoint += scroll;

            if (0f >= scrollPoint && scrollPoint > -1f)
            {
                i = 0;
            }
            else if (-1f >= scrollPoint && scrollPoint > -2f)
            {
                i = 1;
            }
            else if (-2f >= scrollPoint && scrollPoint > -3f)
            {
                i = 2;
            }
            else if (-3f >= scrollPoint && scrollPoint > -4f)
            {
                i = 3;
            }
            else if (-4f >= scrollPoint && scrollPoint >= -5f)
            {
                i = 4;
            }
            else if (scrollPoint < -5f)
            {
                scrollPoint = 0f;
            }
            else
            {
                scrollPoint = -5f;
            }
        }
        theInventory_2.CheckCanUse(i);
    }

    private void UseItem() //�κ����� ������ ���
    {
        if (Input.GetButtonDown("Use Item"))
        {
            theInventory_2.UsingItem();
        }
    }

    private void Interaction() //������ ��ȣ�ۿ�
    {
        if (hitInfo.transform != null)
        {
            if (item.type == IItem.ItemType.interacted)
            {
                item.Use(transform.parent.gameObject);
            }
        }
    }
}
