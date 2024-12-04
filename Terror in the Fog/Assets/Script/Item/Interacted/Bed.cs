using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Bed : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private Door door;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Bed";
        isCanUse = true;
    }

    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            Debug.Log("ħ����");
            isCanUse = false;
            StartCoroutine(UseBed());
            //�ڰ� �Ͼ�� �ִϸ��̼�
            
            door.isCanUse = true;
            //target.transform.position = transform.position - transform.right * 0.3f;
            GameManager.Instance.PassDay();
        }
    }

    private IEnumerator UseBed()
    {
        SoundManager.instance.PlaySound("GoToSleep");
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySound("WakeUp");
    }
}
