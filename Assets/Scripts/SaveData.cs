using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int m_Count, SettingsCount, BestScore;
    public static SaveData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetCount();
        GetSettingsCounter();
        GetBestScore();
    }

    public void SetCount()
    {
        PlayerPrefs.SetInt("Count", m_Count);
        PlayerPrefs.Save();
        Debug.Log(m_Count);
    }

    public void SettingsCounter()
    {
        PlayerPrefs.SetInt("SettingsCount", SettingsCount);
        PlayerPrefs.Save();
        Debug.Log(SettingsCount);
    }

    public void SetBestScore()
    {
        if(GameManager.instance.score > BestScore)
        {
            PlayerPrefs.SetInt("BestScore", GameManager.instance.score);
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
        PlayerPrefs.Save();
        GetBestScore();
        Debug.Log("BestScore: " + BestScore);
    }

    public void GetCount()
    {
        m_Count = PlayerPrefs.GetInt("Count");
    }

    public void GetSettingsCounter()
    {
        SettingsCount = PlayerPrefs.GetInt("SettingsCount");
    }

    public void GetBestScore()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
    }
}
