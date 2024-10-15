using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundname;

    public int soundLevel;

    public GameObject soundGameObject;

    public AudioClip clip;

    public float spatialBlend = 1.0f;

    public float volume = 1.0f;

    public float pitch = 1.0f;
    
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    //static �������� �����ͼ� ��� �� �� �ְ� ���ش�.  �̱�������: ��𼭵� �������� �����ϰ� ������ �� �ִ� ������ �ִ�.
    public static SoundManager instance;               //�̱��� �ν��Ͻ� ȭ ��ƾ��.

    public List<Sound> sounds = new List<Sound>();

    SoundData soundData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetSound();
    }

    private void SetSound()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = sound.soundGameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            SoundData soundData = sound.soundGameObject.GetComponent<SoundData>();
            soundData.soundname.Add(sound.soundname);
            soundData.soundLevel.Add(sound.soundLevel);
        }
    }

    // ���带 ����ϴ� �ż���
    public void PlaySound(string name)                                  //�μ� Name �޾Ƽ�
    {

        Sound soundToPlay = sounds.Find(sound => sound.soundname == name);   //List �ȿ� �ִ� name�� ���� ���� �˻� �� soundToPlay �� ����
        if (soundToPlay != null)
        {
            soundToPlay.source.Play();
        }
        else
        {
            Debug.LogWarning("���� : " + name + " �����ϴ�.");
        }
    }
    
    public void PauseSound(string name)
    {
        Sound soundToPause = sounds.Find(sound => sound.soundname == name);
        //Debug.Log(soundToPause.soundname);
        if (soundToPause != null)
        {
            soundToPause.source.Stop();
        }

    }
}

