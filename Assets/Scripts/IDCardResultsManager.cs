using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IDCardResultsManager : Singleton<IDCardResultsManager>
{
    public GameObject NameRedImage,DOBRedimage,NationalityRedImage,IdExpiredRedImage,CardNumberWrong;
    public Text Resulttext;

    public bool NameerrorCheck, DateOfBirthCheck, NationalityerrorCheck, IdexpirederrorCheck, cardnumwrongCheck;

    public void init(bool Nameerror,bool DateOfBirth,bool Nationalityerror,bool Idexpirederror,bool cardnumwrong)
    {
        NameRedImage.SetActive(Nameerror ? true : false);

        DOBRedimage.SetActive(DateOfBirth ? true : false);

        NationalityRedImage.SetActive(Nationalityerror ? true : false);

        IdExpiredRedImage.SetActive(Idexpirederror ? true : false);

        CardNumberWrong.SetActive(cardnumwrong ? true : false);
        Resulttext.text = "No Any Fault in ID";

        if (Nameerror) Resulttext.text = "Wrong Pedistrain Name";
        if (DateOfBirth) Resulttext.text = "Wrong Date of Birth";
        if (Nationalityerror) Resulttext.text = "Wrong Nationality";
        if (Idexpirederror) Resulttext.text = "ID Card Is Expired";
        if (cardnumwrong) Resulttext.text = "ID Card Number is Wrong";

        NameerrorCheck = Nameerror;
        DateOfBirthCheck = DateOfBirth;
        NationalityerrorCheck = Nationalityerror;
        IdexpirederrorCheck = Idexpirederror;

        cardnumwrongCheck = cardnumwrong;
    }




}
