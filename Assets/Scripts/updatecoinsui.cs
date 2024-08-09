using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class updatecoinsui : MonoBehaviour
{
    public Text[] Showtext;

    public static updatecoinsui instance;
    public static event Action<int> updateUitext;



    public string PrefRankNumHolder = "PrefRankNumHolder";
    public int[] ranksnumbers;
    public GameObject Acheivementspanel;
    public Slider RankSlider;
    public Sprite[] Ranksimages;
    public Image NewRankGot;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
      foreach(Text myuitext in Showtext)
        {
            myuitext.text = PlayerPrefs.GetInt("PlayerCoins", 0).ToString();
        }
       
    }
    

  

    public void updatingUI(int coins)
    {
        StartCoroutine(updatewithanim(coins));
    }

    public void updatingMinus(int coins)
    {
        StartCoroutine(updatewithanimminus(coins));
    }
   

    IEnumerator updatewithanim(int coinstoupdate)
    {
        int i = PlayerPrefs.GetInt("PlayerCoins", 0);
        //if (i <= 0)
        //{
        //    yield break;
        //}
        Debug.Log("Prev" + i);
        for(int j = 0; j < coinstoupdate; j++)
        {

            
            int previouscoins = PlayerPrefs.GetInt("PlayerCoins", 0);
              Debug.Log("For Per: "+previouscoins);
            previouscoins+=1;

            try
            {
                if (previouscoins >= ranksnumbers[PlayerPrefs.GetInt(PrefRankNumHolder, 0)])
                {
                    Acheivementspanel.SetActive(true);
                    int oldrank = PlayerPrefs.GetInt(PrefRankNumHolder, 0);
                    Debug.LogError("OLD RANK First Time" + oldrank);
                    //if (PlayerPrefs.HasKey(PrefRankNumHolder))
                    //{
                    oldrank += 1;

                    //}
                    if (oldrank > Ranksimages.Length - 1)
                    {

                    }
                    else
                    {
                        NewRankGot.sprite = Ranksimages[oldrank];
                        RankSlider.value = ranksnumbers[oldrank];

                    }
                    PlayerPrefs.SetInt(PrefRankNumHolder, oldrank);
                    Debug.LogError("OLD RANK NUM" + oldrank);
                }
            }catch(Exception e)
            {

            }
            PlayerPrefs.SetInt("PlayerCoins", previouscoins);
            foreach (Text myuitext in Showtext)
            {
                myuitext.text = previouscoins.ToString();
            }
          //  GameManager.Instance.XPRankSlider.value = previouscoins;
         //   GameManager.Instance.XPGainAndTotalText.text = " " + previouscoins + " / 100";
            //  Showtext.text = previouscoins.ToString();

            // yield return new WaitForSeconds(0.0001f);


        }

       
        yield return null;

    }
    IEnumerator updatewithanimminus(int coinstoupdate)
    {
       // SoundsPlayer.instance.coinspendsoundplay();

        int i = PlayerPrefs.GetInt("PlayerCoins", 0);
        Debug.Log("Prev" + i);
        if (i <= 0)
        {
            yield break;
                
        }
        for(int j = 0; j < coinstoupdate; j++)
        {

            
            int previouscoins = PlayerPrefs.GetInt("PlayerCoins", 0);
           // Debug.Log("For Per: "+previouscoins);
            previouscoins--;
            if (previouscoins <= 0)
            {
                PlayerPrefs.SetInt("PlayerCoins", 0);

                yield break;
                    
            }
            foreach(Text alltextfields in Showtext)
            {
                alltextfields.text = previouscoins.ToString();
            }
            PlayerPrefs.SetInt("PlayerCoins", previouscoins);
            //  Showtext.text = previouscoins.ToString();

            // yield return new WaitForSeconds(0.01f);


        }

       
        yield return null;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Clicked");
            updatecoinsui.instance.updatingUI(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Mins Clicked");
            updatecoinsui.instance.updatingMinus(1);
        }
    }


}
