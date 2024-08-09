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
          //  Debug.Log("For Per: "+previouscoins);
            previouscoins++;
            PlayerPrefs.SetInt("PlayerCoins", previouscoins);
            foreach (Text myuitext in Showtext)
            {
                myuitext.text = previouscoins.ToString();
            }
            //  Showtext.text = previouscoins.ToString();

            yield return new WaitForSeconds(0.0001f);


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
            PlayerPrefs.SetInt("PlayerCoins", previouscoins);
            foreach(Text alltextfields in Showtext)
            {
                alltextfields.text = previouscoins.ToString();
            }
          //  Showtext.text = previouscoins.ToString();

            yield return new WaitForSeconds(0.01f);


        }

       
        yield return null;

    }

    private void Update()
    {
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Clicked");
            updatecoinsui.instance.updatingUI(50);
        }
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Mins Clicked");
            updatecoinsui.instance.updatingMinus(50);
        }
    }


}
