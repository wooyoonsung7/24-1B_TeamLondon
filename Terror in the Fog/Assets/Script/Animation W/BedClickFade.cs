using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BedClickFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // ���̵�� �̹���
    [SerializeField] private float fadeDuration = 1.0f; // ���̵� ���� �ð�
    [SerializeField] private float holdDuration = 2.0f; // ���� ȭ�� ���� �ð�
    [SerializeField] private Transform spawnPos;

    public bool isFading = false;

    public void BedAnimation()
    {
        if (isFading || fadeImage == null) return;

        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        isFading = true;

        // ���̵� ��
        yield return fadeImage.DOFade(1, fadeDuration).WaitForCompletion();

        // ���� ȭ�� ����
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = spawnPos.position;
        player.Theta += 180;
        yield return new WaitForSeconds(holdDuration);

        // ���̵� �ƿ�
        yield return fadeImage.DOFade(0, fadeDuration).WaitForCompletion();

        isFading = false;
    }
}
