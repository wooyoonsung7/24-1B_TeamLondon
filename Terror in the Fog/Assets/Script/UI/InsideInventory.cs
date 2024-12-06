using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InsideInventory : MonoBehaviour
{
    public static InsideInventory Instance = null;

    //�κ��丮�� ���Ե鿡�� ����� ���������� ����
    [SerializeField]
    private GameObject go_InsideInventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    //�θ��丮�� ������ ��, �÷��̾������� ���߱� ���� ����
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject staminaSlider;
    [SerializeField]
    private Text itemText;
    public bool isFull = false;

    //�������� ���
    private int i_index  = 0;
    private GameObject[] checkImages;

    private bool OneTime = true;
    public bool ItemExist = false;
    public string ItemName;

    int[] indexs = { 15, 16, 17, 18, 19, 90, 91, 92, 93, 94 };
    int[] tokenIndexs = { 15, 16, 17, 18, 19 };
    int[] ValuableId = { 90, 91, 92, 93, 94 };
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        checkImages = new GameObject[6];
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetColor(0);
            checkImages[i] = slots[i].transform.GetChild(1).gameObject;
            checkImages[i].SetActive(false);
        }
    }

    private void Update()
    {
        FadeOut();
    }
    public void AcuquireItem(IItem _item)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public void CheckSlotFull()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                count++;
            }
        }
        if (count >= slots.Length)
        {
            isFull = true;
            //Debug.Log("�κ��丮�� ���� á���ϴ�");
        }
        else
        {
            isFull = false;
        }
    }

    public void CheckCanUse(int index)
    {
        if (index != i_index)
        {
            OneTime = true;
        }

        i_index = index;

        if (OneTime && slots[i_index].item != null) //�������̸�ǥ��
        {
            itemText.text = slots[i_index].item.itemName;
            FadeIn();
            OneTime = false;
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i != index)                            //�κ��丮�� ��밡��ǥ��, �����ۻ�밡�� �ʱ�ȭ
            {
                checkImages[i].SetActive(false);
                ItemExist = false;

                if (slots[i].item == null)
                {
                    //Debug.Log("Ȯ�� 2");
                    slots[i].isCanUse = false;
                }
            }
        }
        checkImages[i_index].SetActive(true);

        if (slots[index].item != null)
        {
            ItemExist = true;
            ItemName = slots[index].item.itemName;
            //Debug.Log("Ȯ�� 3");
            slots[index].isCanUse = true;
        }
        else ItemExist = false;
    }

    public bool CheckItem()  //�κ��丮���� ����� �޼���
    {
        if (slots[i_index].item == null) return false;
        for (int j = 0; j < ValuableId.Length; j++)
        {
            if (slots[i_index].item.index == ValuableId[j])
            {
                ItemExist = false;
                return false;
            }
        }
        return true;
    }

    public void UsingItem()
    {
        int index = i_index;
        if (slots[index].isCanUse && slots[index].item != null)
        {
            slots[index].item.Use(playerController.gameObject);
            UsedItem(index);
        }
        else
        {
            //Debug.Log("�������� �����ϴ�");
        }
    }

    private void UsedItem(int index)
    {
        if (slots[index].item.isCanUse)
        {
            slots[index].ClearSlot();
        }
    }

    public void ClearAllItem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public bool CheckInventory()  //1~4������ �����۵�� �����������_Home����
    {
        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                return false;
            }
        }
        return true;
    }
    public void CheckInventory_2() //����ǰ�̳� ��ū�̿��� ������ ���Ա���
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                for (int j = 0; j < indexs.Length; j++)
                {
                    if (slots[i].item.index == indexs[j]) break;
                    else if(j == indexs.Length - 1) slots[i].ClearSlot();
                }
            }
        }
    }

    public bool CheckInventory_3()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                for (int j = 0; j < tokenIndexs.Length; j++)
                {
                    if (slots[i].item.index == tokenIndexs[j]) break;
                    else if (j == tokenIndexs.Length - 1) return false;
                }
            }
        }
        return true;
    }

    private void FadeIn()
    {
        //SetColor((int)currentTime);
        itemText.DOFade(1f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => StartCoroutine(FadeOut()));
    }
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);

        itemText.DOFade(0f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            Destroy(gameObject);
        }
        else if(scene.name != "GameClear")
        {
            playerController = FindObjectOfType<PlayerController>();
            staminaSlider = GameObject.Find("StaminaSlider");
            itemText = GameObject.Find("ItemNameText").GetComponent<Text>();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
