using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name;        // ���� �̸�
        public string description; // ���� ����
        public Sprite icon;        // ���� ������
        public bool isUnlocked;    // ���/���� ����
    }

    [Header("Achievements Data")]
    public List<Achievement> achievements; // ���� ������ ����Ʈ

    [Header("UI References")]
    public GameObject achievementItemPrefab; // Prefab ���ø�
    public Transform achievementsParent;     // Grid Layout Group �Ǵ� Content

    void Start()
    {
        PopulateAchievements();
    }

    // ���� UI ����
    void PopulateAchievements()
    {
        foreach (var achievement in achievements)
        {

            GameObject item = Instantiate(achievementItemPrefab, achievementsParent);

            // Prefab�� UI ��� ����
            item.transform.Find("Icon").GetComponent<Image>().sprite = achievement.icon;
            item.transform.Find("Name").GetComponent<Text>().text = achievement.name;
            item.transform.Find("Description").GetComponent<Text>().text = achievement.description;


            var statusText = item.transform.Find("Status").GetComponent<Text>();
            if (achievement.isUnlocked)
            {
                statusText.text = "Unlocked";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "Locked";
                statusText.color = Color.red;
            }
        }
    }


    public void UnlockAchievement(string achievementName)
    {
        foreach (var achievement in achievements)
        {
            if (achievement.name == achievementName && !achievement.isUnlocked)
            {
                achievement.isUnlocked = true;
                RefreshUI();
                Debug.Log($"Achievement Unlocked: {achievementName}");
                break;
            }
        }
    }
    void RefreshUI()
    {
        // ���� UI ����
        foreach (Transform child in achievementsParent)
        {
            Destroy(child.gameObject);
        }

        // ������Ʈ�� �����ͷ� �ٽ� ����
        PopulateAchievements();
    }
}
