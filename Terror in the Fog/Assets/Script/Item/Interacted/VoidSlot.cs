using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidSlot : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private int slotIndex = 0;

    [SerializeField]
    private GameObject[] tokenPrf = new GameObject[5];

    private Token saveToken;

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUsing = false;
    public bool isSettingAtHome = false;
    private void Start()
    {
        type = ItemType.interacted;
        isCanUse = false;
        index = slotIndex;

        if (!isSettingAtHome)
        {
            itemName = "��å��";
            itemRot = Vector3.zero;
            itemRot.x -= 75.4f;
            quaternion = Quaternion.Euler(itemRot);
        }
        else
        {
            itemName = "å��<��ū>";
            itemRot = Vector3.zero;
            itemRot.x -= 90f;
            itemRot.y -= 180f;
            quaternion = Quaternion.Euler(itemRot);

            if (SaveData.instance.data.ContainsKey("��ū����" + index))
            {
                GameObject temp = Instantiate(tokenPrf[SaveData.instance.data["��ū����" + index]], transform.position - transform.forward * 0.03f, quaternion);
                saveToken = temp.GetComponent<Token>();
                StartCoroutine(CheckSave());
            }
            else
            {
                StartCoroutine(CheckSave());
            }

            if (GameManager.Days == 5 && SaveData.instance.data.ContainsKey("��ū����_5������" + index))
            {
                GameObject temp = Instantiate(tokenPrf[SaveData.instance.data["��ū����_5������" + index]], transform.position - transform.forward * 0.03f, quaternion);
                saveToken = temp.GetComponent<Token>();
            }
        }
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            SoundManager.instance.PlaySound("SetToken");
            isCanUse = false;
            isUsing = true;
            gameObject.layer = 0;
            StartCoroutine(SetToken());
        }
        CheckCorrect();
    }

    private IEnumerator SetToken()
    {
        for (int i = 0; i < tokenPrf.Length; i++)
        {
            Token token = tokenPrf[i].GetComponent<Token>();
            if (getIndex == token.tokenIndex)
            {
                if (!isSettingAtHome)
                {
                    Instantiate(tokenPrf[i], transform.position + transform.forward * 0.05f, quaternion);
                }
                else
                {
                    GameObject temp = Instantiate(tokenPrf[i], transform.position - transform.forward * 0.03f, quaternion);
                    saveToken = temp.GetComponent<Token>();
                    if (!SaveData.instance.data.ContainsKey("��ū����" + index)) SaveData.instance.data.Add("��ū����" + index, i);
                }
            }
        }
        yield return null;
    }

    private void CheckCorrect()
    {
        if (getIndex == index)
        {
            isCanUse = true;
        }
        else
        {
            isCanUse = false;
        }
    }

    private IEnumerator CheckSave()
    {
        while (true)
        {
            if (SaveData.instance.data.ContainsKey("��ū����" + index))
            {

                if (!SaveData.instance.data.ContainsKey("��ū����_5������" + index))
                {
                    if (GameManager.Days == 5 || GameManager.Days == 4)
                    {
                        SaveData.instance.data.Add("��ū����_5������" + index, SaveData.instance.data["��ū����" + index]);
                    }
                }

                if (saveToken == null)
                {
                    gameObject.layer = 6;
                    isUsing = false;
                    SaveData.instance.data.Remove("��ū����" + index);
                }
                else
                {
                    //Debug.Log("��������");
                }
            }
            yield return null;
        }
    }
}
