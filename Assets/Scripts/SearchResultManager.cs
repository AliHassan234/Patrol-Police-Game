using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchResultManager : Singleton<SearchResultManager>
{
    public Text item1Name, item2Name, item3Name, item4Name,resulttxt;
    public Image item1image, item2image, item3image, item4image;
    public GameObject item1red, item2red, item3red, item4red;
    public bool isitem1wrng, isitem2wrng, isitem3wrng, isitem4wrng;
    public void init(string item1name,string item2name,string item3name,string item4name,Sprite item1img,Sprite item2img, Sprite item3img, Sprite item4img,bool isitem1iswrong,bool isitem2iswrong,bool isitem3iswwrong,bool isitem4iswrong)
    {
        item1Name.text = item1name;
        item1image.sprite = item1img;

        item2Name.text = item2name;
        item2image.sprite = item2img;

        item3Name.text = item3name;
        item3image.sprite = item3img;

        item4Name.text = item4name;
        item4image.sprite = item4img;

        item1red.SetActive(isitem1iswrong);
        item2red.SetActive(isitem2iswrong);
        item3red.SetActive(isitem3iswwrong);
        item4red.SetActive(isitem4iswrong);

        isitem1wrng = isitem1iswrong;
        isitem2wrng = isitem2iswrong;
        isitem3wrng = isitem3iswwrong;
        isitem4wrng = isitem4iswrong;




        if (isitem1iswrong || isitem2iswrong || isitem3iswwrong || isitem4iswrong) {
            resulttxt.text = "Found An Illegal item.";
        }else
        {
            resulttxt.text = "Nothing Illegal Found";
        }

    }
}
