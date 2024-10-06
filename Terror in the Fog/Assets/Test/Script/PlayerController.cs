using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;
using System.Runtime.CompilerServices;

public class PlayerController : MonoBehaviour
{
    //�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float walkSpeed = 2.5f;
    public float runSpeed = 5.0f;
    public float crouchSpeed = 1.1f;
    public float playerHigh = 0.8f;
    public float crouchDgree = 0.3f;
    public int stamina_Time = 5;
    public int recover_stamina_Time = 7;
    public bool isHide = false;

    //ī�޶� ���� ����
    [Header("Camera Settings")]
    public Camera firstPersonCamera;      //1��Ī ī�޶�

    public float radius = 5.0f;          //3��Ī ī�޶�� �÷��̾� ���� �Ÿ�
    public float minRadius = 1.0f;       //ī�޶� �ּ� �Ÿ�
    public float maxRadius = 10.0f;      //ī�޶� �ִ� �Ÿ�


    public float yMinLimit = 30;         //ī�޶� ���� ȸ�� �ּҰ�
    public float yMaxLimit = 90;         //ī�޶� ���� ȸ�� �ִ밢        

    private float theta = 0.0f;                  //ī�޶� ����ȸ�� ����
    private float phi = 0.0f;                    //ī�޶��� ����ȸ�� ����
    private float targetVerticalRotation;         //��ǥ ���� ȸ�� ����
    private float RotationSpeed = 240f;           //���� ȸ�� �ӵ�

    public float mouseSenesitivity = 2f;  //���콺 ����

    private bool isCrouching = false;     //1��ġ ��� ���� ����
    private Rigidbody rb;

    [Header("Ext Setting")]
    public GameObject head;
    public Slider s_slider;        //�÷��̾��� ����� ��Ÿ���� UI
    public GameObject s_Fill;
    public float Fade_Duration = 5f;    //���¹̳� �帴�ϰ� �ϴ� ���ӽð�
    private bool playerCanRun = true;
    private Image[] s_Image = new Image[2];
    private bool isFadeIn = false;
    private bool isFadeOut = true;
    private float timer = 0f;

    public bool isCanMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        s_Image[0] = s_slider.GetComponentInChildren<Image>();
        s_Image[1] = s_slider.transform.GetChild(1).GetComponentInChildren<Image>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;          //���콺 Ŀ���� ��װ� �����
        UnityEngine.Cursor.visible = false;
        SetupCameras();
        SetActiveCamera();
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleRun();
        HandleCrouch();
    }

    //Ȱ��ȭ ī�޶� �����ϴ� �Լ�
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(true);
    }

    //ī�޶� �� ĳ���� ȸ��ó���ϴ� �Լ�
    void HandleRotation()
    {
        if (isCanMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

            //���� ȸ��(theta ��)
            theta += mouseX;
            theta = Mathf.Repeat(theta, 360f);

            //���� ȸ�� ó��
            targetVerticalRotation -= mouseY;
            targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit);

            phi = Mathf.MoveTowards(phi, targetVerticalRotation, RotationSpeed * Time.deltaTime);

            //�÷��̾�, �Ӹ�ȸ�� ó��
            head.gameObject.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
            gameObject.transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        }
    }

    //ī�޶� �ʱ� ��ġ �� ȸ���� �����ϴ� �Լ�
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0f, -0.75f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    //�÷��̾� �ൿó�� �Լ�
    void HandleMovement()
    {
        if (isCanMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //ĳ���� �������� �̵�
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);  //������� �̵�
        }

    }

    void HandleRun()
    {

        if (Input.GetButton("Run") && !isCrouching && playerCanRun)
        {
            moveSpeed = runSpeed;

            s_slider.value -= Time.deltaTime / stamina_Time;
            if (s_slider.value <= 0.01f)
            {
                playerCanRun = false;
                s_Fill.SetActive(false);
                timer = Time.time;
            }

            FadeIn();                  //���̵� �ѹ��� ó���ϱ� ���� �Ұ�
            Debug.Log("�ٴ�.");
            Debug.Log(moveSpeed);
        }
        else
        {
            if (Time.time >= timer + 0.3f)
            {
                s_Fill.SetActive(true);

                if (!isCrouching)
                    moveSpeed = walkSpeed;

                s_slider.value += Time.deltaTime / recover_stamina_Time;
                if (s_slider.value == 1)
                {
                    playerCanRun = true;
                    FadeOut();

                }
            }

        }
    }

    void HandleCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                Debug.Log("�ɴ�");
                gameObject.transform.localScale = new Vector3(0.0f, crouchDgree, 0.0f);
                moveSpeed = crouchSpeed;
                Debug.Log(moveSpeed);
            }
            else
            {
                Debug.Log("�Ͼ");
                gameObject.transform.localScale = new Vector3(0.0f, playerHigh, 0.0f);
                moveSpeed = walkSpeed;
                Debug.Log(moveSpeed);
            }
        }

    }

    private void FadeIn()
    {
        if (isFadeIn)
        {
            for (int i = 0; i < s_Image.Length; i++)
            {
                s_Image[i].DOFade(1f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => isFadeOut = true);
            }
            isFadeIn = false;
        }
    }
    private void FadeOut()
    {
        if (isFadeOut)
        {
            for (int i = 0; i < s_Image.Length; i++)
            {
                s_Image[i].DOFade(0f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => isFadeIn = true);
            }
            isFadeOut = false;
        }
    }
}
