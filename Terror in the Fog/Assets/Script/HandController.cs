using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 1.5f;

    private bool pickupActivated = false;

    public LayerMask whatIsTarget;
    public LayerMask obstacleMask;

    private RaycastHit hitInfo;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private float scrollSpeed = 2f;
    float scrollPoint = 0f;
    int i = 0;

    public IItem item;

    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();
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
            InsideInventory.Instance.CheckSlotFull();
        }
    }
    private void CheckItem() //���������� Ȯ��
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        //������Ʈ�� ��, ȹ�� X�� ������ �ٴڿ� �������� ���� ��, �����۰� ������Ʈ�� ��ġ�� ������ ����, �ٸ� ������
        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget)) //�ش� �������� ���������� �ν�
        {

            item = hitInfo.collider.GetComponent<IItem>();
            ItemInfoAppear();
        }
        else
        {
            item = null;
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //���������� ǥ�� �ϱ�
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "��ȭ�ۿ�" + "<color=yellow>" + "(F)" + "</color>";
    }
    private void ItemInfoDisappear() //���������� ǥ�� ����
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp() //������ ȹ��
    {
        if (pickupActivated && !InsideInventory.Instance.isFull)
        {
            if (hitInfo.transform != null && hitInfo.collider.gameObject.layer == 6)
            {
                if (item.type == IItem.ItemType.Used)
                {
                    SoundManager.instance.PlaySound("GetItem");
                    Debug.Log(item.itemName + " ȹ�� �߽��ϴ�.");
                    InsideInventory.Instance.AcuquireItem(item);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                    Debug.Log("�������̸���" + item.itemName);
                    EventManager.instance.CheckIventoryItem(item.itemName);
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
            else if (-5f >= scrollPoint && scrollPoint >= -6f)
            {
                i = 5;
            }
            else if (scrollPoint < -6f)
            {
                scrollPoint = 0f;
            }
            else
            {
                scrollPoint = -6f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            i = 0; scrollPoint = -0.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            i = 1; scrollPoint = -1.5f;
        }
        else if( Input.GetKeyDown(KeyCode.Alpha3))
        {
            i = 2; scrollPoint = -2.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            i = 3; scrollPoint = -3.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            i = 4; scrollPoint = -4.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            i = 5; scrollPoint = -5.5f;
        }
        InsideInventory.Instance.CheckCanUse(i);
    }

    private void UseItem() //�κ����� ������ ���
    {
        if (Input.GetButtonDown("Use Item"))
        {
            if (item != null)
            {
                InsideInventory.Instance.UsingItem();
            }

        }
    }

    private void Interaction() //������ ��ȣ�ۿ�
    {
        if (hitInfo.transform != null && item != null)
        {
            if (item.type == IItem.ItemType.interacted)
            {
                item.Use(transform.parent.gameObject);
            }
        }
    }

    public void isDead()  //�����, ��������� ��������
    {
        Interaction();
    }
}
