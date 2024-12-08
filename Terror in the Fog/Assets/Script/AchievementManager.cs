using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;          //�̱��� ȭ
    public List<Achievement> achievements;              //Achievement Ŭ������ List�� ����

    public Text[] AchievementTexts = new Text[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);              //�ٸ� Scene������ ���� �ϱ� ���ؼ� �ı� ���� �ʰ� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void UpdateAchievementUI()
    {


        for (int i = 0; i < achievements.Count; i++)
        {
            var achievement = achievements[i];
            Image iconImage = AchievementTexts[i].transform.Find("Icon").GetComponent<Image>(); // Icon �̹��� ��������

            if (achievement.isUnlocked)
            {
                // ������ ������ ��� �����ϰ�
                iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 1f);
            }
            else
            {
                // ������ ��� ��� �帴�ϰ�
                iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0.8f); // ���İ� 0.5�� �帴�ϰ�
            }

            // �ؽ�Ʈ ������Ʈ
            AchievementTexts[0].text = achievement.name;
            AchievementTexts[1].text = achievement.description;
            AchievementTexts[2].text = $"{achievement.currentProgress}/{achievement.goal}";
            AchievementTexts[3].text = achievement.isUnlocked ? "�޼�" : "�̴޼�";
        }

    }


    public void AddProgress(string achievementName, int amount) //���� ���� ��Ȱ ���� �Լ�
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);         //�μ����� �޾ƿ� �̸����� ���� ����Ʈ���� ã�Ƽ� ��ȯ
        if (achievement != null)                                                            //��ȯ�� ������ ���� ���
        {
            achievement.AddProgress(amount);                                                //���α׷����� ���� ��Ų��.
        }
    }

    //���ο� ���� �߰� �Լ�
    public void AddAchievement(Achievement achievement)
    {
        //Achievement temp = new Achievement("�̸�", "����", 5);
        achievements.Add(achievement);                              //List�� ���� �߰�
    }
   
}
