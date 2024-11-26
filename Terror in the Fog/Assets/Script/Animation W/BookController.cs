using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookController : MonoBehaviour
{
    public bool isPlaced = false; // å�� ��ġ�Ǿ����� ���� Ȯ��

    // å�� ������ ��ġ�� �̵���Ű�� �Լ�
    public void PlaceBook(Transform targetSlot)
    {
        if (!isPlaced)
        {
            isPlaced = true;
            // å�� �̵��ϰ� ���� ȸ�� ����
            transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y�� ���� 90�� ȸ��
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
