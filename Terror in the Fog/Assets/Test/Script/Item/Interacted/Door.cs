using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;

public class Door : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    //public float tweenDuration = 0.01f;
    public bool isOpen = false;
    private bool canOpen = true;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Door";
        isCanUse = false; //�������� �Ұ�
    }
    private void Update()
    {

    }
    public void Use(GameObject target)
    {
        Vector3 doorPos = transform.position;
        isOpen = !isOpen;
        if (isOpen && canOpen)
        {
            doorPos += new Vector3(1.6f, 0f, 0f);
            Debug.Log("�� ����");
            transform.DOLocalMove(doorPos, 0.5f).OnComplete(()=> canOpen = false);
        }
        else if(!isOpen && !canOpen)
        {
            doorPos += new Vector3(-1.6f, 0.0f, 0.0f);
            Debug.Log("�� �ݱ�");
            transform.DOLocalMove(doorPos, 0.5f).OnComplete(()=> canOpen = true);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && !isOpen)
            {
                StartCoroutine(E_InteractDoor(enemy));
            }
        }
    }
    IEnumerator E_InteractDoor(Enemy _enemy)
    {
        Enemy enemy = _enemy;
        if (!isOpen)
        {
            Use(gameObject);
            enemy.navMeshAgent.isStopped = true;
        }

        yield return new WaitForSeconds(0.1f);
        enemy.navMeshAgent.isStopped = false;
        yield return new WaitForSeconds(0.1f);
        Use(gameObject);

    }
}