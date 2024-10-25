using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReturn : MonoBehaviour
{
    // �÷��̾ �ǵ��ư� ��ġ�� ������ ����
    public Vector3[] returnPositions = new Vector3[4]; // �迭 ũ�⸦ 4�� ����

    void Start()
    {
        // �⺻ ��ġ�� ù ��° ��ġ�� ����
        returnPositions[0] = new Vector3(-40, 2, 130); // ù ��° ��ġ
        returnPositions[1] = new Vector3(160, 2, -180); // �� ��° ��ġ
        returnPositions[2] = new Vector3(-230, 2, 180); // �� ��° ��ġ
        returnPositions[3] = new Vector3(-40, 2, -470); // �� ��° ��ġ

        // �ʱ� ��ġ ���� (ù ��° ��ġ�� �̵�)
        transform.position = returnPositions[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ڽ� �ݶ��̴��� �±׸� üũ
        if (other.CompareTag("Boundary1")) // ù ��° �ݶ��̴�
        {
            transform.position = returnPositions[0]; // ù ��° ��ġ�� �̵�
        }
        else if (other.CompareTag("Boundary2")) // �� ��° �ݶ��̴�
        {
            transform.position = returnPositions[1]; // �� ��° ��ġ�� �̵�
        }
        else if (other.CompareTag("Boundary3")) // �� ��° �ݶ��̴�
        {
            transform.position = returnPositions[2]; // �� ��° ��ġ�� �̵�
        }
        else if (other.CompareTag("Boundary4")) // �� ��° �ݶ��̴�
        {
            transform.position = returnPositions[3]; // �� ��° ��ġ�� �̵�
        }
    }
}
