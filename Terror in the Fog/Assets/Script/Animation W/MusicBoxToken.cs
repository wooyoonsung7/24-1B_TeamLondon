using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxToken : MonoBehaviour
{
    public Transform token;         // ��ū ������Ʈ
    public Transform tokenHole;     // ��ū�� ������ ��ġ
    public float animationDuration = 1f; // �ִϸ��̼� ���� �ð�

    private bool isTokenReleased = false; // ��ū�� �̹� ���Դ��� Ȯ��

    private void OnMouseDown()
    {
        if (isTokenReleased) return; // ��ū�� �̹� ������ �������� ����

        ReleaseToken();
    }

    private void ReleaseToken()
    {
        isTokenReleased = true;
        token.gameObject.SetActive(true); // ��ū Ȱ��ȭ

        // ��ū�� �ö���� �ִϸ��̼� (ȸ�� ����)
        token.DOMove(tokenHole.position, animationDuration)
             .SetEase(Ease.OutBack); // �ε巯�� �ִϸ��̼�
    }
}
