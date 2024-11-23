using UnityEngine;
using DG.Tweening;

public class DrawerController : MonoBehaviour
{
    public Transform drawer; // ������ Transform
    public float openPositionZ = -1f; // ������ ������ ���� Z ��ġ
    public float closePositionZ = 0f; // ������ �ݾ��� ���� Z ��ġ
    private bool isOpen = false; // ������ �����ִ��� ����

    void Start()
    {
        // ������ ������ �� ���� ���·� ����
        drawer.localPosition = new Vector3(drawer.localPosition.x, drawer.localPosition.y, closePositionZ);
    }

    void Update()
    {
        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0)) // 0�� ��Ŭ��
        {
            ToggleDrawer(); // ���� ����/�ݱ� ���
        }
    }

    public void ToggleDrawer()
    {
        if (isOpen)
        {
            // ������ ���� ������ �ݱ�
            drawer.DOLocalMoveZ(closePositionZ, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            // ������ ���� ������ ����
            drawer.DOLocalMoveZ(openPositionZ, 0.5f).SetEase(Ease.OutQuad);
        }

        // ���¸� ������Ŵ
        isOpen = !isOpen;
    }
}
