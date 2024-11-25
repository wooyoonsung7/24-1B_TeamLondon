using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BedInteraction : MonoBehaviour
{
    public Transform player;                //�÷��̾� ������Ʈ
    public Transform bedPosition;           //ħ�� �� ���� ��ġ
    public float moveDuration = 1.0f;       //�̵� �ð�
    public float rotationDuration = 0.5f;   //ȸ�� �ð�

    private bool isLyingDown = false;       //�÷��̾ ���� �ִ� ���� üũ

    public Vector3 lyingRotation = new Vector3(90f, 0f, 0f); // x�� ȸ������ ���η� ���� 
    private void OnMouseDown()
    {
        if (isLyingDown)        //�÷��̾ �������� ���� ���� ����
        {
            LieDownOnBed();
        }
        else
        {
            GetUpFromBed();
        }
    }
    void LieDownOnBed()
    {
        isLyingDown = true;

        // ���� �ִϸ��̼� ����
        DOTween.Kill(player);

        // �̵� �ִϸ��̼�
        player.DOMove(bedPosition.position, moveDuration).SetEase(Ease.InOutQuad);

        // ȸ�� �ִϸ��̼�
        player.DORotate(lyingRotation, rotationDuration).SetEase(Ease.InOutQuad);

        // �÷��̾� ��Ʈ�� ��Ȱ��ȭ
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;
    }
    void GetUpFromBed()
    {
        isLyingDown = false;

        // ���� ��ġ�� �ʱ� ��ġ�� ���� (���÷� Vector3.zero ���)
        player.DOMove(Vector3.zero, moveDuration).SetEase(Ease.InOutQuad);

        // ���� ȸ�� ����
        player.DORotate(Vector3.zero, rotationDuration).SetEase(Ease.InOutQuad);

        // �÷��̾� ��Ʈ�� �ٽ� Ȱ��ȭ
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
