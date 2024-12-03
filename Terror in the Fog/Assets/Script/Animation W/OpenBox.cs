using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenBox : MonoBehaviour
{
    public Transform lid; // ���� �Ѳ� (�Ѳ��� Transform)
    public float openAngle = 90f; // ������ ����
    public float duration = 1f; // �ִϸ��̼� ���� �ð�

    public void ToggleBox()
    {
        lid.DOLocalRotate(new Vector3(-openAngle, 0, 0), duration); // ���� ����
        /*
        if (isOpen)
        {
            // ���� �ִϸ��̼�
            lid.DOLocalRotate(new Vector3(-openAngle, 0, 0), duration); // ���� ����
            isOpen = false;
        }*/
    }
}
