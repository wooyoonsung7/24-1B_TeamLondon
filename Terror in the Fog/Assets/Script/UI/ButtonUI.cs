using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;  //��ư�� ������ ��, �ؽ�Ʈ �������

    public void ChangeColor()
    {
        if (text == null)
        {
            Debug.Log("����");
            return;
        }
        text.color = Color.black;
    }

}
