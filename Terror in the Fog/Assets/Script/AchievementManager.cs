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
                Debug.Log($"���� �޼�: {name}");

                if (showAlert)
                {
                    ShowAchievementAlert();
                }
            }
        }

        private void ShowAchievementAlert()
        {
            
            Debug.Log($"�˶�: {name} �޼�!");
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
       
        achievements = new Achievement[]
        {
            new Achievement
            {
                name = "clover key",
                description = "Obtained the clover key.",
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

    
