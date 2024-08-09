using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MainMenuPanel;
    public GameObject GameExitpanel;
    public GameObject SoundOnOff;
    public GameObject MusicOnOff;

    public Sprite SoundOnSprite;
    public Sprite SoundOffSprite;


    public string PrivacyURL;
    public string MoreGameURL;
    public string RateUsUrl;

    private void Start()
    {
        Time.timeScale = 1f;
        //SoundManager.Instance.PlayBG(SoundName.Background);

    }

    public void opensetting()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);

    }

    public void closesettings()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }


    public void openPrivacyPolicy()
    {
        Application.OpenURL(PrivacyURL);

    }
    public void MoreGamesURL()
    {
        Application.OpenURL(MoreGameURL);

    }


    public void RateUsURl()
    {
        Application.OpenURL(RateUsUrl);

    }

    public void openGamePlay()
    {
        Debug.LogError("Loading GamePlay");
        LoadingScript.Instance.loadscene(2);
        //SoundManager.Instance.PlayGamePlayMusic();

    }
    public void OpenGameExitPanel()
    {
        MainMenuPanel.SetActive(false);
        GameExitpanel.SetActive(true);
    }
    public void CloseGameExitPanel()
    {
        MainMenuPanel.SetActive(true);
        GameExitpanel.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }


    public void togglesound()
    {
        
    }
    public void toggleMusic()
    {

    }


}
