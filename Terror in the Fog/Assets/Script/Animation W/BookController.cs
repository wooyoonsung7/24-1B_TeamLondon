using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookController : MonoBehaviour
{
    public bool isPlaced = false; // å�� ��ġ�Ǿ����� ���� Ȯ��
    public Transform setPlace;

    private void Start()
    {
        StartCoroutine(PlaceBook());
    }
    private IEnumerator PlaceBook()
    {
        while (true)
        {
            if (isPlaced)
            {
                //Debug.Log("�ȴٵȴ�");
                transform.DOMove(setPlace.position, 0.5f).SetEase(Ease.OutQuad);
                transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y�� ���� 90�� ȸ��
                isPlaced = false;
            }
            yield return null;
        }
    }

}
