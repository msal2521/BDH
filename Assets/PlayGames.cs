using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayGames : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate((bool success) => {});

    }

    //#region Achievements
    //public static void UnlockAchievement(string id)
    //{
    //    Social.ReportProgress(id, 100, success => { });
    //}

    //public static void IncrementAchievement(string id, int stepsToIncrement)
    //{
    //    PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    //}

    //public static void ShowAchievementsUI()
    //{
    //    Social.ShowAchievementsUI();
    //}
    //#endregion /Achievements

    #region Leaderboards
    public void AddScoreToLeaderboard(int score)
    {
        string Leaderboard_id = "CgkIv4aU5NcOEAIQAQ";

        //if(Social.localUser.authenticated)
        PlayGamesPlatform.Instance.ReportScore(score, Leaderboard_id, success => {});
    }

    public void LeaderboardsUI()
    {
        //if(Social.localUser.authenticated)
        //Social.ShowLeaderboardUI();
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

    /*public void AddScoreToLeaderboard()
    {
        AddScoreToLeaderboard(GPGSIds.leaderboard_leaderboard, score);
    }*/
}
