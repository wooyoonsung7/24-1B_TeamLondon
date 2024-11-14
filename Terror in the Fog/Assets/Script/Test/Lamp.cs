using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Light lampLight;
    public float activationRange = 5f; // �÷��̾ ���� �ȿ� ���� �� ����Ʈ�� ������ ����

    void Start()
    {
        lampLight = GetComponent<Light>();
        lampLight.enabled = false; // ������ �� ����Ʈ�� ��
    }

    void Update()
    {
        // �÷��̾�� Lamp ������Ʈ ���� �Ÿ� ����
        GameObject player = GameObject.FindWithTag("player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // ���� ���� ���� ��� ����Ʈ �ѱ�
            if (distance <= activationRange)
            {
                lampLight.enabled = true;
            }
            else
            {
                lampLight.enabled = false;
            }
        }
    }
}
