using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingBarController : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage;

    [SerializeField, Range(1f, 7f)]
    private float loadingSpeed = 0.01f;



    AsyncOperation operation;
    private void Start()
    {
        // �ε� ����
        StartCoroutine(LoadTutorialScene());
    }

    public IEnumerator LoadTutorialScene()
    {
        if (!EventManager.TutorialEnd)
        {
            operation = SceneManager.LoadSceneAsync("TutorialScene");
        }
        else
        {
            operation = SceneManager.LoadSceneAsync("Home");
            GameManager.Days--;
        }

        operation.allowSceneActivation = false;
        float fakeProgress = 0f;




        while (!operation.isDone)
        {
            if (operation.progress < 0.9f)
            {
                fakeProgress = Mathf.Lerp(fakeProgress, operation.progress, Time.deltaTime * 2f); 
            }
            else
            {
                fakeProgress = Mathf.Lerp(fakeProgress, 1f, Time.deltaTime * 3f); 
            }


            progressBar.value = fakeProgress;
            fillImage.fillAmount = fakeProgress;

            if (fakeProgress >= 0.99f && operation.progress >= 0.9f)
            {
               
                operation.allowSceneActivation = true; 
            }




            yield return new WaitForSeconds(0.001f);
        }

    }
}
