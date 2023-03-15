using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialsManager : MonoBehaviour
{
    private Vector3 ScreenVector, tempX, tempY;
    public GameObject Basket, BG, LeftEnd, RightEnd, TopEnd;


    private void Start()
    {
        ScreenAdjustments();
        Basket.gameObject.transform.position = new Vector3(-tempX.x, 14f, 10f);
    }

    private void ScreenAdjustments()
    {
        BG.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        ScreenVector = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        //Debug.Log(ScreenVector);
        tempX.x = ScreenVector.x;
        tempY.y = ScreenVector.y;
        LeftEnd.transform.position = tempX;
        RightEnd.transform.position = -tempX;
        TopEnd.transform.position = -tempY;
    }

    public void play()
    {
        SaveData.instance.m_Count = 1;
        SaveData.instance.SetCount();
        SceneManager.LoadScene(2);
    }
}
