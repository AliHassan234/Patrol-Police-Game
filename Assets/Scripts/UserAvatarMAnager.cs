using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserAvatarMAnager : MonoBehaviour
{
    public GameObject AvatarPanel;
    public InputField EnterName;
    public Text ErrorPrompt;
    public static readonly string UserNamePref="UserNamePref";

    [Header("Main Menu Avatar Settings")]
    public Text NameTxt;
    public Slider UserNameRankSlider;


    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(UserNamePref))
        {
            AvatarPanel.SetActive(false);
        }
        else
        {
            AvatarPanel.SetActive(true);

        }


        MainMenuAvatarSystem();
    }

    public void onNameEnter()
    {
        if (EnterName.text.ToString().Trim().Length <= 0)
        {
            ErrorPrompt.text = "Please Enter Name";
            return;
        }
        PlayerPrefs.SetString(UserNamePref,EnterName.text.ToString());
        AvatarPanel.SetActive(false);
        MainMenuAvatarSystem();
    }

    public void MainMenuAvatarSystem()
    {
        NameTxt.text = PlayerPrefs.GetString(UserNamePref);
        int oldrank = PlayerPrefs.GetInt("PrefRankNumHolder", 0);
        UserNameRankSlider.value = oldrank;

    }
}
