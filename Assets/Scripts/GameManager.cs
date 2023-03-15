using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float maxTime, timeleft, temptimeleft = 4f, RewardCount;
    public int score;
    private Vector3 ScreenVector, tempX, tempY;
    public TextMeshProUGUI TimerText, ScoreText, FinalScoreText, tempText, WaitTimeText, BestScoreText;
    public GameObject GameOverPanel, InGameUIPanel, InGameObjects, Basket, BG, LeftEnd, RightEnd, TopEnd, PausePanel, RewardsPanel, SoundButton;
    public bool direction = false, BallMovement = true, isGameOver, isPaused, isAudioPlaying = true, PerfectScore = true;
    public AudioSource ButtonAudioSource, BounceAudioSource, ScoreAudioSource;
    public AudioListener MainAudio;
    public Sprite AudioOn, AudioOff;
    public ParticleSystem FireTrail, SmokeTrail;
    public Slider TimerSlider;
    public Shake _shake;
    GPGS gpgs;

    void Awake()
    {
        instance = new GameManager();
        //If we don't currently have a GameManager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        Time.timeScale = 1;
        SaveData.instance.GetSettingsCounter();
        AudioSettings();
    }

    private void Start()
    {
        timeleft = maxTime;
        ScreenAdjustments();
        Basket.gameObject.transform.position = new Vector3(-tempX.x + 1, Random.Range(7, 17), 10f);

        _shake = Camera.main.GetComponent<Shake>();
        gpgs = GameObject.Find("GPGS").GetComponent<GPGS>();
    }

    private void Update()
    {
        CountdownTimer();
        if (timeleft > 0 && timeleft < 5 && !isPaused)
        {
            Camera.main.GetComponent<AudioSource>().enabled = true;
        }
    }

    public void ButtonAudio()
    {
        if (isAudioPlaying)
        {
            ButtonAudioSource.Play();
        }
    }

    public void AudioSettings()
    {
        if (SaveData.instance.SettingsCount == 0)
        {
            SoundButton.GetComponent<Image>().sprite = AudioOn;
            MainAudio.gameObject.GetComponent<AudioSource>().mute = false;
            isAudioPlaying = true;
            SaveData.instance.SettingsCount = 1;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = AudioOff;
            MainAudio.gameObject.GetComponent<AudioSource>().mute = true;
            isAudioPlaying = false;
            SaveData.instance.SettingsCount = 0;
        }
        SaveData.instance.SettingsCounter();
    }

    public void PerfectScored()
    {
        if (PerfectScore)
        {
            Debug.Log("heloooo");
            FireTrail.Play();
            StartCoroutine(_shake.Shaker(0.4f, 0.4f));
        }
        else
        {
            Debug.Log("heloooo 22");
            SmokeTrail.Play();
            StartCoroutine(_shake.Shaker(0.2f, 0.2f));
        }
        //Handheld.Vibrate();
    }

    private void ScreenAdjustments()
    {
        BG.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        ScreenVector = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        Debug.Log(ScreenVector);
        tempX.x = ScreenVector.x;
        tempY.y = ScreenVector.y;
        LeftEnd.transform.position = tempX;
        RightEnd.transform.position = -tempX;
        TopEnd.transform.position = -tempY;
    }

    private void CountdownTimer()
    {
        if (timeleft > 0)
        {
            if (score < 20)
            {
                timeleft -= Time.deltaTime;
            }
            else if (score < 30)
            {
                timeleft -= Time.deltaTime * 1.5f;
            }
            else if (score < 40)
            {
                timeleft -= Time.deltaTime * 2f;
            }
            else if (score < 500)
            {
                timeleft -= Time.deltaTime * 2.5f;
            }
            if (timeleft <= 5)
            {
                TimerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0.8f, 0f, 0f, 1f);
            }
            else
            {
                TimerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0.1686275f, 0.4588236f, 0.8196079f, 1f);
            }
            //TimerText.text = "Time Left: " + (int)timeleft;
            TimerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = timeleft/10f;
        }

        if (timeleft < 0)
        {
            BallMovement = false;
            tempText.text = "Time Up!!";
            tempText.gameObject.SetActive(true);
            StartCoroutine(DisableTempText(2f));
        }
    }

    public void Cancel()
    {
        temptimeleft = 0;
        RewardsPanel.SetActive(false);
        GameOver();
    }

    public void RewardAd()
    {
        RewardsPanel.SetActive(false);
        BallMovement = true;
        temptimeleft = 4f;
        timeleft = maxTime;
        Camera.main.GetComponent<AudioSource>().enabled = false;
        isGameOver = false;
        isPaused = false;
        RewardCount++;
    }

    public void Score()
    {
        if (isAudioPlaying)
        {
            ScoreAudioSource.Play();
        }
        score++;
        ScoreText.text = "Score: " + score;
        timeleft = maxTime;
        TimerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
        Camera.main.GetComponent<AudioSource>().enabled = false;

        if (score == 5)
        {
            tempText.text = "NICE!!";
            tempText.gameObject.SetActive(true);
            StartCoroutine(DisableTempText(1f));
        }
        else if (score == 15)
        {
            tempText.text = "GREAT!!";
            tempText.gameObject.SetActive(true);
            StartCoroutine(DisableTempText(1f));
        }
        else if (score == 20)
        {
            tempText.text = "EXCELLENT!!";
            tempText.gameObject.SetActive(true);
            StartCoroutine(DisableTempText(1f));
        }
    }

    IEnumerator DisableTempText(float timer)
    {
        yield return new WaitForSeconds(timer);
        tempText.gameObject.SetActive(false);
        if (timeleft < 0)
        {
            GameOver();
        }

        //if (timeleft < 0 && RewardCount < 3 && score >= 5)
        //{
        //    if (temptimeleft > 0)
        //    {
        //        BallMovement = false;
        //        RewardsPanel.SetActive(true);
        //        temptimeleft -= Time.deltaTime;
        //        WaitTimeText.text = "Watch Ad to Continue in: " + (int)temptimeleft;
        //        StartCoroutine(DisableRewardAds(4f));
        //    }
        //}
        //else if (!isGameOver && timeleft < 0)
        //{
        //    UnityMonetization.instance.DisplayInterstitialAd();
        //}
    }

    IEnumerator DisableRewardAds(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        RewardsPanel.SetActive(false);
        if (!isGameOver && timeleft < 0)
        {
            UnityMonetization.instance.DisplayInterstitialAd();
        }
    }

    public void SwitchOnScoring()
    {
        if (!direction)
        {
            direction = true;
            Basket.transform.position = new Vector3(tempX.x - 1, Random.Range(7, 17), 10f);
            Basket.transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        }
        else
        {
            direction = false;
            Basket.transform.position = new Vector3(-tempX.x + 1, Random.Range(7, 17), 10f);
            Basket.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        }
    }

    public void DisableObjects()
    {
        InGameUIPanel.SetActive(false);
        InGameObjects.SetActive(false);
        Camera.main.GetComponent<AudioSource>().enabled = false;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            FinalScoreText.text = "Score: " + score;
            SaveData.instance.SetBestScore();
            BestScoreText.text = "Best Score: " + SaveData.instance.BestScore;
            gpgs.localLeaderboardScore = SaveData.instance.BestScore;
            gpgs.AddScoreToLeaderboard();
            GameOverPanel.SetActive(true);
            DisableObjects();
            isGameOver = true;
        }
    }

    public void Resume()
    {
        isPaused = false;
        ButtonAudio();
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        InGameUIPanel.SetActive(true);
        InGameObjects.SetActive(true);
    }

    public void Pause()
    {
        isPaused = true;
        ButtonAudio();
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        DisableObjects();
    }

    public void Restart()
    {
        ButtonAudio();
        if (SaveData.instance.SettingsCount == 0)
        {
            SaveData.instance.SettingsCount = 1;
        }
        else
        {
            SaveData.instance.SettingsCount = 0;
        }
        SaveData.instance.SettingsCounter();
        SceneManager.LoadScene(2);        
    }

    public void Home()
    {
        ButtonAudio();
        //SceneManager.MoveGameObjectToScene(gpgs.gameObject, SceneManager.GetSceneByBuildIndex(0));
        SceneManager.LoadScene(0);
    }
}
