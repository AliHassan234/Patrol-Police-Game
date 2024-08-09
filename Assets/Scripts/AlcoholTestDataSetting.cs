using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholTestDataSetting : Singleton<AlcoholTestDataSetting>
{
    public Text BACText;
    public Text CTDText;
    public Text ResultOfAlcohol;

    public GameObject BACred, CTDred;

    public bool is_BAC_rationFine, is_CDT_ratiofine;

    public void init(string BACRatio,string CTDRatio,bool isBACratioFine,bool isCDTratioFine)
    {
        
        //0.05% this ratio is Normal For BAC test
        BACText.text = "BAC = " + BACRatio + " %";
        //less than or equal to 1.7%	is Noraml ratio of 

        CTDText.text = "CDT = " + CTDRatio + " %";

        BACred.SetActive(isBACratioFine);
        CTDred.SetActive(isCDTratioFine);

        is_BAC_rationFine = isBACratioFine;
        is_CDT_ratiofine = isCDTratioFine;
        if (isBACratioFine && isCDTratioFine)
        {
            ResultOfAlcohol.text = "Pedistran Have not Consumed Any Alcohol";
        }
        else
        {
            ResultOfAlcohol.text = "Pedistran Have Consumed Alcohol";

        }

    }

}
