using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private TextMeshProUGUI text;  //��ư�� ������ ��, �ؽ�Ʈ �������

    [SerializeField]private Button button;  //��ư���ͷ��� �����ؼ� ����������Ʈ ������

    [SerializeField] bool isUseHighLight = false;

    private bool isHighlighted = false;
    private Color defaultColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
    }

    private void Update()
    {
        if (isUseHighLight)
        {
            //ChangeColorSetting();
        }
    }
    public void ChangeColor()
    {
        if (text == null)
        {
            Debug.Log("����");
            return;
        }
        text.color = Color.black;
    }
    /*
    public void ChangeColorSetting()
    {
        //defaultColor = text.color;
        if (isHighlighted)
        {
            Debug.Log("�ȴ�");
            text.color = Color.black;
        }
        else
        {
            text.color = new Color(191, 191, 191, 255);
        }
    }*/
}
