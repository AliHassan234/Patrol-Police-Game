using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugTestResult : Singleton<DrugTestResult>
{
    public Text THCText;
    public Text CBDText;
    public Text ResultOfAlcohol;

    public GameObject THCred, CBDRed;

    public bool is_THC_fine, is_CBD_Fine;
    public void init(string THCRatio, string CBDRatio, bool isTHCFine, bool isCBDFine)
    {

        //0.05% this ratio is Normal For BAC test
        THCText.text = "BAC = " + THCRatio + " %";
        //less than or equal to 1.7%	is Noraml ratio of 

        CBDText.text = "CDT = " + CBDRatio + " %";

        THCred.SetActive(isTHCFine);

        CBDRed.SetActive(isCBDFine);

        is_THC_fine = isTHCFine;
        is_CBD_Fine = isCBDFine;
        if (isTHCFine && isCBDFine)
        {
            ResultOfAlcohol.text = "Pedistran Have Consumed Drugs";
        }
        else
        {
            ResultOfAlcohol.text = "Pedistran Have Consumed Drugs";
        }

    }
}
