using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour
{
    
    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public Sprite icon;
    }

    public Sprite cloverkeyIcon;
    public Sprite diamondKeyIcon;
    public Sprite spaceKeyIcon;

    public Achievement[] achievements;
    public GameObject achievementPrefab;  // ���� UI ������
    public Transform achievementsContainer; // UI�� ������ ���� �����̳�

    void Start()
    {
        // ���� ������ ����
        achievements = new Achievement[]
        {
            new Achievement { name = "Ŭ�ι� Ű", description = "Ŭ�ι� Ű ȹ��!", isUnlocked = true, icon = cloverkeyIcon },
            new Achievement { name = "���̾Ƹ�� Ű", description = "���̾Ƹ�� Ű ȹ��!", isUnlocked = false, icon = diamondKeyIcon },
            new Achievement { name = "�����̽� Ű", description = "�����̽� Ű ȹ��!", isUnlocked = true, icon = spaceKeyIcon }
        };

        // �� ������ ���� UI ����
        foreach (Achievement achievement in achievements)
        {
            CreateAchievementUI(achievement);
        }
    }

    void CreateAchievementUI(Achievement achievement)
    {
        // ���� UI�� �����տ��� �ν��Ͻ�ȭ�Ͽ� �߰�
        GameObject achievementUI = Instantiate(achievementPrefab, achievementsContainer);

        // ���� UI�� ������Ʈ ��������
        Image iconImage = achievementUI.transform.Find("Icon").GetComponent<Image>();
        Text nameText = achievementUI.transform.Find("Name").GetComponent<Text>();
        

        // ���� ������ UI ������Ʈ
        if (iconImage != null) iconImage.sprite = achievement.icon;
        if (nameText != null) nameText.text = achievement.name;
        

        // ���� ��� ���ο� ���� ������ ���� ����
        if (iconImage != null && !achievement.isUnlocked)
        {
            iconImage.color = new Color(1, 1, 1, 0.5f); // ������ ó��
        }
    }
}
    

