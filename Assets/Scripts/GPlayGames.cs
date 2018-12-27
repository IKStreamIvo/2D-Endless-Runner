using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPlayGames : MonoBehaviour {

	private void Start(){
        if(GameObject.FindObjectsOfType<GPlayGames>().Length > 1){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		SignIn();
	}

	private void SignIn(){
		Social.localUser.Authenticate(success => {});
	}

	#region Achievements
    public static void UnlockAchievement(string id){
        Social.ReportProgress(id, 100, success => {});
    }
 
    public static void IncrementAchievement(string id, int steps){
        PlayGamesPlatform.Instance.IncrementAchievement(id, steps, success => {});
    }
 
    public static void ShowAchievementsUI(){
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements
 
    #region Leaderboards
    public static void AddScoreToLeaderboard(string id, long score){
        Social.ReportScore(score, id, success => {});
    }
 
    public static void ShowLeaderboardsUI(){
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards
}
