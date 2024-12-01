using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxToken : MonoBehaviour
{
    public Transform token;         // ��ū ������Ʈ
    public Transform tokenHole;     // ��ū�� ������ ��ġ
    public float animationDuration = 1f; // �ִϸ��̼� ���� �ð�


    public void ReleaseToken()
    {
        token.gameObject.SetActive(true); // ��ū Ȱ��ȭ
        gameObject.layer = 0;
        // ��ū�� �ö���� �ִϸ��̼� (ȸ�� ����)
        token.DOMove(tokenHole.position, animationDuration)
             .SetEase(Ease.OutBack); // �ε巯�� �ִϸ��̼�
    }
}
