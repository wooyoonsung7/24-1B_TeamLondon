using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : MonoBehaviour
{
    public static EnemyAnimation instance; 

    [SerializeField] private Enemy enemy;

    public Sequence sequence;  //���۽�, �ٶ󺸴� ����
    public Sequence sequence2; //�� �ݴ����
    public Sequence sequence3; //���۽�, �ٶ󺸴� ����
    public Sequence sequence4; //�� �ݴ����

    public Sequence checkAroundSeq; //������ �ѷ���

    public Sequence tutoSequence; //Ʃ�丮��� �ִϸ��̼�
    public Sequence atStudySeq;   //���翡���� �ִϸ��̼�


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        sequence = DOTween.Sequence();
        sequence2 = DOTween.Sequence();
        sequence3 = DOTween.Sequence();
        sequence4 = DOTween.Sequence();

        checkAroundSeq = DOTween.Sequence();
    }
    private void Start()
    {
        DOMotion();
        CheckAround();
    }
    private void DOMotion()
    {
        sequence.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 3f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 3f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 3f))
       .SetAutoKill(false)
       .Pause()
       .OnComplete(() => Distribute(1));

        sequence2.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 3f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 3f))
        .SetAutoKill(false)
        .Pause()
        .OnComplete(() => Distribute(1));


        sequence3.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -180, 0), 3f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => Distribute(2));

        sequence4.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 3f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => Distribute(2));
    }

    private void CheckAround()
    {
        checkAroundSeq.Prepend(transform.DOLocalRotate(new Vector3(0, 90, 0), 1.5f))
                      .Append(transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f))
                      .Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 1.5f))
                      .SetAutoKill(false)
                      .Pause()
                      .OnComplete(() => enemy.isCheckAround = false);
    }

    private void DoTutoMotion()
    {

    }

    public void DoDayOneMotion()
    {
        //���翡���� �ִϸ��̼�
        
    }

    private void Distribute(int number)
    {
        if (ResearchManager.instance != null)
        {
            ResearchManager.instance.stepNumber = number;
        }
        if (ResearchManager_Simple.instance != null)
        {
            ResearchManager_Simple.instance.stepNumber = number;
        }
    }
    public void StopSquance()
    {
        sequence.Pause();
        sequence2.Pause();
        sequence3.Pause();
        sequence4.Pause();
    }
}
