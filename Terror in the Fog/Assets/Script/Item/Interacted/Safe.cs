using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Safe : MonoBehaviour
{
    private int selectCount = 0;
    private int count = 0;
    private SafeButton[] safeButtons;
    [SerializeField]private GameObject item;
    private void Awake()
    {
        safeButtons = GetComponentsInChildren<SafeButton>();
    }
    void Start()
    {
        StartCoroutine("CheckPuzz"); //성능의 우려로 인해서 코루틴으로 변경
    }
    IEnumerator CheckPuzz()
    {
        while (true)
        {
            Debug.Log("체킹 중");
            for (int i = 0; i < safeButtons.Length; i++)
            {
                //Debug.Log(i + "번째" + safeButtons[i].selected + "이다");
                if (selectCount >= 4)
                {
                    Debug.Log("초과되었다");
                    if (count >= 4)
                    {
                        Vector3 moveToPos = transform.position + transform.forward * 1f;
                        transform.DOMove(moveToPos, 1.5f);
                        safeButtons[i].gameObject.layer = 0;
                        item.gameObject.layer = 6;
                        StopCoroutine("CheckPuzz");
                    }
                    else
                    {
                        StartCoroutine(Clear());
                    }
                }
                else
                {
                    if (safeButtons[i].selected && safeButtons[i].isCanUse)
                    {
                        Debug.Log("선택완");
                        selectCount++;
                        safeButtons[i].isCanUse = false;

                        if (safeButtons[i].isCollect)
                        {
                            Debug.Log("굳");
                            count++;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator Clear()
    {
        for (int i = 0; i < safeButtons.Length; i++)
        {
            if (safeButtons[i].selected)
            {
                safeButtons[i].ResetValue();
            }
            safeButtons[i].isCanUse = true;
            safeButtons[i].selected = false;
            safeButtons[i].ResetValue();
        }
        yield return null;
        selectCount = 0;
        count = 0;
        yield return null;
    }
}
