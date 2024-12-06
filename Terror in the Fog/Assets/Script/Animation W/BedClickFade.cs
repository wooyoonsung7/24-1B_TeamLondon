using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BedClickFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // ���̵�� �̹���
    [SerializeField] private float fadeDuration = 1.0f; // ���̵� ���� �ð�
    [SerializeField] private float holdDuration = 2.0f; // ���� ȭ�� ���� �ð�
    [SerializeField] private TextMeshProUGUI dDayText;
    [SerializeField] private Transform spawnPos;

    public bool isFading = false;
    private int dDay = 1; // ���� �ʱⰪ (1�������� ����)

    public void BedAnimation()
    {
        if (isFading || fadeImage == null || dDayText == null) return;

        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        isFading = true;

        // ���̵� ��
        yield return fadeImage.DOFade(1, fadeDuration).WaitForCompletion();

        // D-day ������Ʈ �� ǥ��
        dDayText.text = $"{dDay}����";
        dDayText.gameObject.SetActive(true);
        dDayText.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad).Play(); // �ؽ�Ʈ ���̵� ��
        yield return new WaitForSeconds(holdDuration);
        dDayText.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad).Play(); // �ؽ�Ʈ ���̵� �ƿ�

        // ���� ȭ�� ����
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = spawnPos.position;
        player.Theta += 180;
        yield return new WaitForSeconds(holdDuration);

        // �ؽ�Ʈ �����
        dDayText.gameObject.SetActive(false);

        // ���̵� �ƿ�
        yield return fadeImage.DOFade(0, fadeDuration).WaitForCompletion();

        isFading = false;

        // D-day ���� (5�������� ����)
        dDay = Mathf.Min(dDay + 1, 5);
    }
}
