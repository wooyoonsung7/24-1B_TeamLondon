using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundDetector : MonoBehaviour
{
    public static SoundDetector instance;


    public bool isDetectOFF = false;
    private int g_level = 0;
    public int G_level
    {
        get { return g_level; }
        set 
        {
            if (g_level > value)
            {
                if (g_level != 3 && value == 0)
                {
                    g_level = value;
                }
                else return;
            }
            else
            {
                g_level = value;
            }

            if (value == 3)
            {
                SoundPos.Clear();  //�̷��� �ϴ��� �����߻�����
            }
        }
    }
    private int defultLevel = 0;

    public List<Vector3> SoundPos = new List<Vector3>();

    private Enemy enemy;
    private bool isPlay_3 = false;
    private bool isPlay_2 = false;
    private bool isPlay_1 = false;

    public bool isFind = false;

    [SerializeField]
    private float detectRadius = 11f;
    [SerializeField]
    private float r_DetectRadius = 11f;
    [SerializeField]
    private LayerMask layerMask;
    int targetMask = (1 << 6) | (1 << 0);

    private Vector3 myPos;
    public bool isOneTime = true;
    private bool isOneTime_2 = true;
    private bool isOneTime_3 = true;
    private float timer = 0f;
    public enum LEVEL
    {
        Level3, Level2, Level1, Level0
    }
    public LEVEL level;
    void Awake()
    {
        instance = this;
        enemy = GetComponent<Enemy>();
        ChangeLevelState(LEVEL.Level0);
    }

    public void OnDetect()
    {
        if (isDetectOFF) return;

        //Debug.Log("isPlay_1" + isPlay_1);
        //Debug.Log("isPlay_2" + isPlay_2);
        //Debug.Log("isPlay_3" + isPlay_3);
        //Debug.Log("���� ���巹��" + g_level);
        Debug.Log("���� ���巹��" + level);
        //Debug.Log("�ν����� ���尳��" + SoundPos.Count);
        switch (level)
        {
            case LEVEL.Level3:
                HurryToPos();
                isPlay_3 = true;
                //Debug.Log("����3������");
                break;

            case LEVEL.Level2:
                MoveToPos();
                isPlay_2 = true;
                //Debug.Log("����2������");
                break;

            case LEVEL.Level1:
                CheckPos();
                isPlay_1 = true;
                //Debug.Log("����1������");
                break;

            case LEVEL.Level0:
                ResetPos();
                //Debug.Log("����0������");
                break;

        }
    }
    public void ChangeLevelState(LEVEL newLevel)
    {
        level = newLevel;
    }

    public void CheckLevel()
    {
        //Debug.Log("�ȴ�2");             g_level, isPlay ������ �����Ǵܰ��̱⿡ �̸� �ٲپ�� �ٸ� ���� �ٲ��.
        if (g_level == 3)
        {
            Debug.Log("�ȴ礷������");
            ChangeLevelState(LEVEL.Level3);
            isPlay_3 = true;
            isPlay_2 = false;
            isPlay_1 = false;
        }
        if (g_level == 2)
        {
            if (!isPlay_3)
            {
                ChangeLevelState(LEVEL.Level2);
                isPlay_2 = true;
                isPlay_1 = false;
            }
        }
        if (g_level == 1)
        {
            if (!isPlay_2 && !isPlay_3)
            {
                ChangeLevelState(LEVEL.Level1);
                isPlay_1 = true;
            }
        }
        if (g_level == 0)
        {
            if (!isPlay_2 && !isPlay_3 && !isPlay_1)
            {
                ChangeLevelState(LEVEL.Level0);
            }
        }
    }

    private void HurryToPos()
    {
        timer += Time.deltaTime;
        if (timer > 4 && isOneTime)
        {
            Debug.Log("����3 ������");
            enemy.navMeshAgent.SetDestination(SoundPos[0]);
            timer = 0f;
        }

        Collider[] target = Physics.OverlapSphere(myPos, 2f, targetMask);

        foreach (Collider p_collider in target)
        {
            Debug.Log("�����ߴ�");
            if (p_collider.gameObject.CompareTag("Trap"))
            {
                Debug.Log("�����ߴ�2");
                isOneTime = false;

                if (GameManager.Days == 2)
                {
                    SoundManager.instance.PauseSound("Toy");
                    ResetHurry();
                }
                if (GameManager.Days == 3)
                {
                    //Debug.Log("������ ����2");
                    if (p_collider.gameObject.layer == 6)
                    {
                        //Debug.Log("������ ����3");
                        SoundManager.instance.PauseSound("Toy");
                        p_collider.GetComponent<Toy>().isGen = true;
                        ResetHurry();
                    }
                    else if(p_collider.gameObject.layer == 0)
                    {
                        SoundManager.instance.PauseSound("Emergency");
                        Debug.Log("�ȴ٤�����");
                        //�������� �����ϴ� ��Ҹ� ���
                        if (timer >= 5)
                        {
                            ResetHurry();
                        }
                    }
                }
                if (GameManager.Days == 5 || GameManager.Days == 4)
                {
                    p_collider.GetComponent<Trap2>().isCanUse = true;
                    ResetHurry();
                }
            }
        }
    }

    private void ResetHurry()
    {
        Debug.Log("����3�� �����ߴ�");
        SoundPos.Clear();
        isPlay_3 = false;
        g_level = defultLevel;
        ChangeLevelState(LEVEL.Level0);
    }
    private void MoveToPos()
    {
        Collider[] target = Physics.OverlapSphere(myPos, r_DetectRadius, layerMask);
        foreach (Collider p_collider in target)
        {
            //Debug.Log("�ȴ�");
            SoundPos.Add(p_collider.transform.position);
            enemy.navMeshAgent.updateRotation = false;
            enemy.transform.DOLookAt(SoundPos[0], 0.5f).OnComplete(() => enemy.navMeshAgent.updateRotation = true);

            //timer += Time.deltaTime;
            if (isOneTime_2)
            {
                //Debug.Log("�̰� �ȵ�?");
                enemy.navMeshAgent.SetDestination(SoundPos[0]);
                //timer = 0f;
                isOneTime_2 = false;
            }
        }
        
        if (target.Length <= 0) isPlay_2 = false;

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                SoundPos.Clear();
                isPlay_2 = false;
                g_level = defultLevel;
                ChangeLevelState(LEVEL.Level0);
                isOneTime_2 = true;
            }
        }
    }
    private void CheckPos()
    {
        Collider[] target = Physics.OverlapSphere(myPos, detectRadius, layerMask);
        foreach (Collider p_collider in target)
        {
            //Debug.Log("�ȴٵȴ�");
            SoundPos.Add(p_collider.transform.position);
            enemy.navMeshAgent.updateRotation = false;
            enemy.transform.DOLookAt(SoundPos[0], 0.5f).OnComplete(() => enemy.navMeshAgent.updateRotation = true);

            if (isOneTime_3)
            {
                enemy.navMeshAgent.SetDestination(SoundPos[0]);
                isOneTime_3 = false;
            }
        }
        
        if (target.Length <= 0) isPlay_1 = false;

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                SoundPos.Clear();
                isPlay_1 = false;
                g_level = defultLevel;
                ChangeLevelState(LEVEL.Level0);
                isOneTime_3 = true;
            }
        }
    }

    public void ResetPos()
    {
        if (g_level != 3)
        {
            isPlay_1 = false;
            isPlay_2 = false;
            isPlay_3 = false;
            g_level = defultLevel;
            SoundPos.Clear();
            //Debug.Log("�ƹ��� �͵� ����");
            isOneTime = true;
        }
        else
        {
            isPlay_3 = true;
            isOneTime = true;
        }
    }

    public void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, detectRadius);
        Gizmos.DrawWireSphere(myPos, r_DetectRadius);
    }
}
