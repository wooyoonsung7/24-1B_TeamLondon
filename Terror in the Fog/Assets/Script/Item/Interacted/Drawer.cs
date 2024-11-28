using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static IItem;

public class Drawer : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField] private int drawerIndex = 0;
    [SerializeField] private bool isUnLocked = true;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject item2;

    public Transform drawer; // ������ Transform
    public float openValue = 1.6f; // ������ ������ ���� Z ��ġ
    public float closePositionZ = 0f; // ������ �ݾ��� ���� Z ��ġ

    public bool isOpen = false;
    private bool canOpen = true;
    private bool checkState = false;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "����";
        isCanUse = isUnLocked;
        index = drawerIndex;

        closePositionZ = transform.localPosition.z;
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            StartCoroutine(CheckUse());
        }
        else
        {
            Debug.Log("���谡 ����");
        }
    }


    private IEnumerator CheckUse()
    {
        if (canOpen)
        {
            canOpen = false;

            checkState = isOpen;
            isOpen = !isOpen;
            if (isOpen != checkState)
            {
                StartCoroutine(OpenDoor());
                StartCoroutine(CloseDoor());
            }
        }
        yield return null;
    }
    private IEnumerator OpenDoor()
    {
        if (isOpen)
        {
            drawer.DOLocalMoveZ(closePositionZ - openValue, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => canOpen = true);
            if (item != null) item.gameObject.layer = 6;
            if (item2 != null) item2.gameObject.layer = 6;
        }
        yield return null;
    }
    private IEnumerator CloseDoor()
    {
        if (!isOpen)
        {
            drawer.DOLocalMoveZ(closePositionZ, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => canOpen = true);
        }
        yield return null;
    }
}
