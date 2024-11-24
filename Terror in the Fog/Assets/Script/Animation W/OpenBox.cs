using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenBox : MonoBehaviour
{
    public Transform lid; // ���� �Ѳ� (�Ѳ��� Transform)
    public float openAngle = 90f; // ������ ����
    public float duration = 1f; // �ִϸ��̼� ���� �ð�

    private bool isOpen = false; // ���ڰ� ���ȴ��� ���� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� Ŭ������ ���� ���� �ݱ� �׽�Ʈ
        if (Input.GetMouseButtonDown(0))
        {
            ToggleBox();
        }
    }
    public void ToggleBox()
    {
        if (isOpen)
        {
            // ���� �ִϸ��̼�
            lid.DOLocalRotate(new Vector3(-openAngle, 0, 0), duration); // ���� ����
        }
        isOpen = !isOpen; // ���� ����
    }
}
