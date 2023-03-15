using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class GPGS : MonoBehaviour
{
    /*FOR ERRORS
    "GPGSIds.leaderboard_leader" REPLACE WITH YOUR LEADERBOARD OR ACHIEVEMENT*/

    [SerializeField] bool autoSignIn;

    [HideInInspector] public int localLeaderboardScore;
    [HideInInspector] public int incrementData;
    public static Text text;

    private void Awake() {

        localLeaderboardScore = incrementData = 0;
    }

    private void Start() {
        text = GameObject.Find("display").GetComponent<Text>();
        text.text = "first page";
        PlayGamesPlatform.Activate();

        if (autoSignIn)
            Login();
                //SignInAuthenticate();
    }




    private static void Login()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success, string error) => {
                text.text = "LOGIN ERROR: " + error;
                Debug.Log("LOGIN ERROR: " + error);

                if (success)
                {
                    Debug.Log("We're signed in! Welcome " + Social.localUser.userName);
                    text.text = "We're signed in! Welcome " + Social.localUser.userName;
                }
                else
                {
                    Debug.Log("Oh... we're not signed in.");
                    text.text = "Oh... we're not signed in.";
                }

            });
        }
        else
        {
            Debug.Log("We're already signed in! Welcome " + Social.localUser.userName);
            text.text = "We're already signed in! Welcome " + Social.localUser.userName;
        }
    }






    #region SIGNIN
    public void SignInAuthenticate() {
        
        Social.localUser.Authenticate(success => {

            PlayGamesPlatform.Instance.LoadScores(GPGSIds.leaderboard_best_score, LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, data => {

               SaveData.instance.BestScore = int.Parse(data.PlayerScore.formattedValue);
            });
        });
        
        
    }

    public void SignOut() {

        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion SIGNOUT



    #region LEADERBOARD START
    public void ShowLeaderboard() {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_best_score);
    }

    public void AddScoreToLeaderboard() {
       
        Social.ReportScore(localLeaderboardScore, GPGSIds.leaderboard_best_score, success =>{
        
        });

    }
    #endregion LEADERBOARD END



    #region ACHIEVEMENT START
    public void ShowAchievements() {

        Social.ShowAchievementsUI();
    }

    public void IncrementAchievements() {

        ((PlayGamesPlatform)Social.Active).IncrementAchievement(GPGSIds.achievement_temp, incrementData, succss =>{});
    }
    public void UnlockAchievement() {

        ((PlayGamesPlatform)Social.Active).UnlockAchievement(GPGSIds.achievement_temp);
    }
    #endregion ACHIVEMENT END

}
