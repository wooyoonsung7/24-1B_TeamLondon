using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField] private int pointIndex; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("�ȴ�....");
            if (ResearchManager_Simple.instance != null)
            {
                //Debug.Log("�ȴ�....");
                Debug.Log(ResearchManager_Simple.instance.moveIndex);
                if (ResearchManager_Simple.instance.moveIndex == pointIndex)
                {
                    //Debug.Log("�ȴ�");
                    ResearchManager_Simple.instance.isEnd = true;
                }
            }
        }
    }
}
