using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class SpeedPack : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; } // �������� ��밡�������� �Ǵ�

    

    [SerializeField]
    private Sprite itemImage_2;
    [SerializeField]
    private GameObject itemPrefab_2;
    [SerializeField]
    //private float fullTime = 5f;

    private float walkSpeed;
    private float runSpeed;
    private float crouchSpeed;
    PlayerController p_Controller;
    private void Start()
    {
        type = ItemType.Consumed;
        itemName = "�ӵ���";
        itemImage = itemImage_2;
        itemPrefab = itemPrefab_2;
        isCanUse = false;
    }

    private void Update()
    {
    }

    public void Use(GameObject target)
    {
        Debug.Log("�ӵ��� ���");
        p_Controller = target.GetComponent<PlayerController>();

        crouchSpeed = p_Controller.crouchSpeed;
        runSpeed = p_Controller.runSpeed;
        walkSpeed = p_Controller.walkSpeed;

        p_Controller.crouchSpeed *= 1.5f;
        p_Controller.runSpeed *= 1.5f;
        p_Controller.walkSpeed *= 1.5f;

        /*StartCoroutine(KeepEfect());

        IEnumerator KeepEfect() //�ӵ��������� ȿ�����ӽð� �� ����
        {
            yield return new WaitForSeconds(fullTime);

            Debug.Log("�ӵ��� ���Ϸ�");
            p_Controller.crouchSpeed = crouchSpeed;
            p_Controller.runSpeed = runSpeed;
            p_Controller.walkSpeed = walkSpeed;
            p_Controller.walkSpeed = walkSpeed;
        }*/
    }

}
