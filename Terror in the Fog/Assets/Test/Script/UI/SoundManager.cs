using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;



public class SoundManager : MonoBehaviour
{
    //static �������� �����ͼ� ��� �� �� �ְ� ���ش�.  �̱�������: ��𼭵� �������� �����ϰ� ������ �� �ִ� ������ �ִ�.
    public static SoundManager instance;               //�̱��� �ν��Ͻ� ȭ ��ƾ��.


    private void Awake()
    {
        instance = this;
    }

}

