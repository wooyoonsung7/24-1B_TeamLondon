using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;
using static ResearchManager;

public class Door : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public bool isOpen = false;
    private bool canOpen = true;
    private bool checkState = false;

    public bool isOpened = true; //��乮���� Ȯ��
    public int doorIndex = 0;     //�����̵�

    Vector3 doorRot;
    Vector3 doorPos;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask TargetMask;
    private bool isOneTime = true;
    private bool isEnemy = false;

    AudioSource[] audioSources;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Door";
        isCanUse = isOpened; //�������� �Ұ�
        index = doorIndex;
        doorRot = transform.rotation.eulerAngles;
        doorPos = transform.position + transform.right * 0.3f - transform.up;

        StartCoroutine(CheckEnemy());
        GetAudioSource();

    }

    public void GetAudioSource()
    {
        if(FindAnyObjectByType<Enemy>() != null) audioSources = FindAnyObjectByType<Enemy>().gameObject.GetComponents<AudioSource>();
    }

    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            StartCoroutine(CheckUse());
        }
        else
        {
            if (!isEnemy) SoundManager.instance.PlaySound("CanNotOpen");
        }
    }

    private IEnumerator CheckUse()
    {
        if (canOpen)
        {
            canOpen = false;

            checkState = isOpen;
            isOpen = !isOpen;
            if (isOpen != checkState)
            {
                StartCoroutine(OpenDoor());
                StartCoroutine(CloseDoor());

                if (!isEnemy)
                {
                    if (isOpen) SoundManager.instance.PlaySound("OpenDoor");
                    else SoundManager.instance.PlaySound("CloseDoor");
                }
            }
        }
        yield return null;
    }
    private IEnumerator OpenDoor()
    {
        if (isOpen)
        {
            doorRot += new Vector3(0f, 90f, 0f);
            transform.DOLocalRotate(doorRot, 0.5f).OnComplete(()=>canOpen = true);
        }
        yield return null;
    }
    private IEnumerator CloseDoor()
    {
        if (!isOpen)
        {
            doorRot -= new Vector3(0f, 90f, 0f);
            transform.DOLocalRotate(doorRot, 0.5f).OnComplete(() => canOpen = true);
        }
        yield return null;
    }

    private IEnumerator CheckEnemy()
    {
        while (true)
        {
            Collider[] targets = Physics.OverlapSphere(doorPos, detectRadius, TargetMask);

            if (targets.Length > 0 && isOneTime)
            {
                isOneTime = false;
                isEnemy = true;
                if (!isOpen)
                {
                    Use(gameObject);
                    audioSources[1].Play();
                    yield return new WaitForSeconds(0.5f);
                    Use(gameObject);
                    audioSources[2].Play();
                    yield return new WaitForSeconds(0.5f);
                    isOneTime = true;
                    isEnemy = false;
                }

                if (!isCanUse)
                {
                    if (ResearchManager.instance != null)
                    {
                        ResearchManager.instance.ChangeEnemyState(ENEMYSTATE.CHANGEROOM); //���� : ������ ������ �Ѵ�.

                        while (true)
                        {
                            if (ENEMYSTATE.CHANGEROOM == ResearchManager.instance.enemystate)
                            {
                                yield return new WaitForSeconds(0.5f);
                                isOneTime = true;
                                break;
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f * Time.deltaTime);
        }
    }
}
