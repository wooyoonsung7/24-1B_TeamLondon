using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour //������ ����3���带 ��� �������� ������ ��, ����
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (SoundDetector.instance.SoundPos.Count > 0)
            {
                
            }
        }
    }
}
