using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfController : MonoBehaviour
{
    public Transform[] slotPositions; // ���� ���� ��ġ
    public BookController[] books; // ���� å

    private int currentSlotIndex = 0; // ���� ����� ���� �ε���

    void OnMouseDown()
    {
        if (currentSlotIndex < books.Length)
        {
            // ���� å�� ���� ���Կ� ��ġ
            //books[currentSlotIndex].PlaceBook(slotPositions[currentSlotIndex]);
            currentSlotIndex++;
        }
    }
}
