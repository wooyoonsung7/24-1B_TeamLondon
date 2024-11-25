using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class KeypadButton : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 pressedPosition;
    private static int buttonPressCount = 0;
    private const int maxPressCount = 4;
    private bool isPressed = false; // ��ư�� ���ȴ��� ����

    public GameObject door; // �� ������Ʈ ����

    private void Start()
    {
        originalPosition = transform.localPosition;
        pressedPosition = originalPosition + new Vector3(-0.001f, 0, 0); // ���� ��ġ ���
    }

    private void OnMouseDown()
    {
        if (!isPressed) // ��ư�� ������ ���� ��쿡�� ����
        {
            AnimateButtonPress();
        }
    }

    private void AnimateButtonPress()
    {
        isPressed = true;
        buttonPressCount++;

        // ��ư�� ���� ���·� ����
        transform.DOLocalMove(pressedPosition, 0.2f);

        if (buttonPressCount >= maxPressCount)
        {
            OpenDoor(); // �� ����
            RestoreAllButtons(); // ��ư �ʱ�ȭ
        }
    }

    private void OpenDoor()
    {
        if (door != null)
        {
            // ���� Y������ -60�� ȸ��
            door.transform.DORotate(new Vector3(0, 60f, 0), 1f, RotateMode.LocalAxisAdd);
        }
    }

    private void RestoreAllButtons()
    {
        // ��� ��ư �ʱ�ȭ
        KeypadButton[] allButtons = FindObjectsOfType<KeypadButton>();

        foreach (KeypadButton button in allButtons)
        {
            button.RestorePosition();
        }

        buttonPressCount = 0;
    }

    public void RestorePosition()
    {
        isPressed = false;
        transform.DOLocalMove(originalPosition, 0.2f); // ���� ��ġ�� ����
    }
}