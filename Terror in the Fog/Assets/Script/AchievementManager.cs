using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public bool showAlert; 



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
                //Debug.Log($"���� �޼�: {name}");

                if (showAlert)
                {
                    ShowAchievementAlert();
                }
            }
        }

        private void ShowAchievementAlert()
        {
            
            //Debug.Log($"�˶�: {name} �޼�!");
        }
    }

    public Achievement[] achievements;

   
    public GameObject alertPanel;
    public Text alertTitleText;
    public Text alertDescriptionText;


    public void CheckAchievements(string condition)
    {
        
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.isUnlocked && condition == achievement.name)
            {
                achievement.Unlock();
                ShowAchievementAlert(achievement.name, achievement.description);
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

           
            Invoke(nameof(HideAchievementAlert), 3f);
        }
    }

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

        alertPanel = GameObject.Find("alertPanel");
        alertTitleText = alertPanel.transform.GetChild(0).GetComponent<Text>();
        alertDescriptionText = alertPanel.transform.GetChild(1).GetComponent<Text>();
        HideAchievementAlert();

        achievements = new Achievement[]
        {
            new Achievement
            {
                name = "Ŭ�ι�����",
                description = "Ŭ�ι����踦 ȹ���߽��ϴ�.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "���̾Ƹ�忭��",
                description = "���̾Ƹ�忭�踦 ȹ���߽��ϴ�.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "�����̵忭��",
                description = "�����̵忭�踦 ȹ���߽��ϴ�.",
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

    
