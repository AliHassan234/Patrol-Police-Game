using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDCardTestManager : Singleton<IDCardTestManager>
{
    //public string dateOfBrith, issueDate, expiryDate, name, gender, state, idCardNum;
    public int wrongNum;
    //public Sprite characterSP, signatureSP;
    


    public Text dateOfBrithtxt, issueDatetxt, expiryDatetxt, Pedistrainnametxt, gendertxt, statetxt, idCardNumtxt;
    public Image characterSPimg, signatureSPimg;

    public void init(string dateOfBrith,string issueDate,string expiryDate,string Pedistrainname, string gender,string state,string idCardNum , Sprite characterSP,Sprite signatureSP)
    {
        dateOfBrithtxt.text = dateOfBrith;
        issueDatetxt.text = issueDate;
        expiryDatetxt.text = expiryDate;
        Pedistrainnametxt.text = Pedistrainname;
        gendertxt.text = gender;
        statetxt.text = state;
        idCardNumtxt.text =idCardNum;
        characterSPimg.sprite = characterSP;
        signatureSPimg.sprite = signatureSP;


    }


  
}
