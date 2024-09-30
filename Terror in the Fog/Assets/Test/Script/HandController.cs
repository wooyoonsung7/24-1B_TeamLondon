using System;
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
    private Inventory theInventory;

    IItem item;

    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();

    }

    void Update()
    {
        CheckItem();
        TryPickUp();
    }

    private void TryPickUp() //������ �ݱ�
    {
        if (Input.GetButtonDown("Interaction"))
        {
            CheckItem();
            CanPickUp();
        }
    }
    private void CheckItem() //���������� Ȯ��
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget))
        {

            item = hitInfo.collider.GetComponent<IItem>();

            ItemInfoAppear();
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //������ȹ�� ǥ�� �ϱ�
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "ȹ��" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //������ȹ�� ǥ�� ����
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
                Debug.Log(item.itemName + " ȹ�� �߽��ϴ�.");
                theInventory.AcuquireItem(item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    private void UseItem()
    {
        if (Input.GetButtonDown("Use Item"))
        {

        }
    }
}
