using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public bool showAlert;  // �˶��� ����� ����
        



        // UI ����
        private AchievementManager  achievementManager;

        public void Initialize(AchievementManager system)
        {
            achievementManager = system;
        }


        public void Unlock()
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                Debug.Log($"���� �޼�: {name}");

                if (showAlert)
                {
                    ShowAchievementAlert();
                }
            }
        }

        private void ShowAchievementAlert()
        {
            // �˶��� ���� ���� �߰� (��: UI �˶�, �˾� ��)
            Debug.Log($"�˶�: {name} �޼�!");
        }
    }

    public Achievement[] achievements;

    // UI �˸� �ؽ�Ʈ �� �г�
    public GameObject alertPanel;
    public Text alertTitleText;
    public Text alertDescriptionText;

 


    public void CheckAchievements(string condition)
    {
        // Ư�� ������ �����ϴ� ��� �ش� ���� �޼�
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.isUnlocked && condition == achievement.name)
            {
                achievement.Unlock();
            }
        }
    }



  


    public void ShowAchievementAlert(string title, string description)
    {
        if (alertPanel != null)
        {
            alertPanel.SetActive(true);
            alertTitleText.text = title;
            alertDescriptionText.text = description;

            // ���� �ð� �� �˸� �����
            Invoke(nameof(HideAchievementAlert), 3f);
        }
    }

    private void Awake()
    {
        // ���� �迭 �ʱ�ȭ
        achievements = new Achievement[]
        {
            new Achievement
            {
                name = "box key",
                description = "I got the box key.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "diamond key",
                description = "Obtained the Diamond Key.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "spade key",
                description = "Obtained the spade key.",
                isUnlocked = false,
                showAlert = true
            }
        };
    }

    private void HideAchievementAlert()
    {
        if (alertPanel != null)
        {
            alertPanel.SetActive(false);
        }
    }
}

    
