using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector]
    public PlayerProfileModel profile;

    public static PlayerInfo instance;
    void Awake() { instance = this; }

    public void OnLoggedIn()
    {
        GetPlayerProfileRequest getProfileRequest = new GetPlayerProfileRequest()
        {
            PlayFabId = LoginRegester.instance.playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true
            }
        };

        PlayFabClientAPI.GetPlayerProfile(getProfileRequest,
            result =>
            {
                profile = result.PlayerProfile;
                Debug.Log("loaded in player: " + profile.DisplayName);
            },
            error => Debug.Log(error.ErrorMessage)
        );
    }

}
