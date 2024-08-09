using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCScriptableNS;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : Singleton<GameManager>
{
    public NPCScriptable nPCScriptable;

    public GameObject CenterDot;
    public GameObject MainSelectionScreen;
    public Button AskIdBTN, SearchBTN, AndTesTBTN,TicketsPunish;

    public GameObject PunishmentsScreen;
    public GameObject ParkingTicketspunishment;
    public GameObject itemsFoundinSearch;
    public GameObject IDCardScanning;
    public GameObject IDCardScanningResults;

    public GameObject PausePanel;
    public GameObject Achievementsunlocked;
    public GameObject AlcoholTestResult;
    public GameObject SharabiTestsMain;
    public GameObject DrugtestResultpanel;

    public GameObject positionwhendoingcarchallan;

  //  public GameObject TutCompleted;



    public GameObject FPSControlleer;
    public GameObject policeCharacter;
    public Animator PoliceAnimator;
    public GameObject PencilInCharacterHand;
    public GameObject NoteBookinHand;
    public GameObject AndTestMachine;
    public GameObject HathkariofPlayer;
    
    
    public GameObject camera2nd;
    public GameObject camerasearch;


    [Header("****************  Controller Refrence  **************")]
    public RigidbodyFirstPersonController cotroller;
    
    [Header("****************  Car Parking Fines  **************")]

    public Button ClosetoSideWalk;
    public int fineamountsidewalk;

    public Button isinWrongDirection;
    public int fineamountwrongdirection;

    public Button iscariswrongparked;
    public int cariswrongparked;

    public Button ismeterisExpired;
    public int fineamountmeterexpired;

    public GameObject rightDecisionPanel;
    public Text RightdecisionAmount;
    public GameObject WrongDecisionPanel;
    public Text WrongDecisionAmount;


    [Header("****************  Controller Canvas **************")]
    public GameObject cf2canvas;
    public GameObject Handparent;

    public GameObject DialoguePanel;
    public GameObject NextBTN;

    [Header("****************  All Characters Reference **************")]
    public GameObject[] AllCharacters;


    public GameObject BGSOUND;


    [Header("GamePlay Avatar Settings")]
    public Text NameTxt;
    public Slider UserNameRankSlider;


    [Header("XP Slider Settings")]
    public Slider XPRankSlider;
    public Text XPGainAndTotalText;

    public enum InvestigationType
    {
        
        PassportIDSearch,
        SearchingbyHand,
        AlcoholTest,
        Drugtest

    }


    private void Start()
    {
        ClosetoSideWalk.onClick.RemoveAllListeners();

        isinWrongDirection.onClick.RemoveAllListeners();

        iscariswrongparked.onClick.RemoveAllListeners();

        ismeterisExpired.onClick.RemoveAllListeners();


        ClosetoSideWalk.onClick.AddListener(() => CheckforDecisionAndFineAmount(fineamountsidewalk,0));
        isinWrongDirection.onClick.AddListener(() => CheckforDecisionAndFineAmount(fineamountwrongdirection, 1));
        iscariswrongparked.onClick.AddListener(() => CheckforDecisionAndFineAmount(cariswrongparked, 2));
        ismeterisExpired.onClick.AddListener(() => CheckforDecisionAndFineAmount(fineamountmeterexpired, 3));


  /*      GamePlayAvatarSystem();*//**/
    }
    public void OpenMainSelectionScreen()
    {
        CenterDot.SetActive(false);
        MainSelectionScreen.SetActive(true);
    }
    public void CloseMainSelectionScreen()
    {
        CenterDot.SetActive(true) ;
        MainSelectionScreen.SetActive(false);
        Closenextplayercam();
    }
    public void Closenextplayercam()
    {
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);
        // RaycastFromCamera.Instance.nextplayercam.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        RaycastFromCamera.Instance.nextplayercam.transform.localPosition = Vector3.zero;
        RaycastFromCamera.Instance.nextplayercam.transform.localRotation = Quaternion.identity;
    }

    public void GamePause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GOTOHOME()
    {
        LoadingScript.Instance.loadscene(1);
    }
    public void RestartGame()
    {
        LoadingScript.Instance.loadscene(2);

    }
    public void giveFine(int fineamount)
    {
        updatecoinsui.instance.updatingMinus(fineamount);
    }
    public void RightDecisionEconomyPlus(int amount)
    {
        updatecoinsui.instance.updatingUI(amount);

    }

    ////////////////////////////////////////************     Car Fine System        *************/////////////////////////////////
    public void CheckforDecisionAndFineAmount(int Amount,int BTNClickedID)
    {
        policeCharacter.transform.position = positionwhendoingcarchallan.transform.position;

        if (BTNClickedID == 0)
        {
           
            //Close to SideWalk..
          if(RaycastFromCamera.Instance.CPSscript.isCarisClosetoSideWalk)
            {
                //Right Decision
                RightDecisionEconomyPlus(Amount);
                AcheivementManager.Instance.updateParkingTicketAch();
                RightdecisionAmount.text = "+"+Amount+" XP";
                rightDecisionPanel.SetActive(true);
            }
            else
            {
                //Wrong Decision
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-" + Amount + " XP";
                giveFine(Amount);
            }

        }
        else if (BTNClickedID==1)
        {

            //Wrong Direction..
            if (RaycastFromCamera.Instance.CPSscript.isCarIsInWrongDirection)
            {
                //Right Decision
                RightDecisionEconomyPlus(Amount);
                AcheivementManager.Instance.updateParkingTicketAch();

                RightdecisionAmount.text = "+" + Amount + " XP";
                rightDecisionPanel.SetActive(true);
            }
            else
            {
                //Wrong Decision
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-" + Amount + " XP";
                giveFine(Amount);
            }

        }
        else if (BTNClickedID==2)
        {
            //Is Car is in No Parking..
            if (RaycastFromCamera.Instance.CPSscript.isCarIsInWrongParking)
            {
                //Right Decision
                RightDecisionEconomyPlus(Amount);
                AcheivementManager.Instance.updateParkingTicketAch();

                RightdecisionAmount.text = "+" + Amount + " XP";
                rightDecisionPanel.SetActive(true);
            }
            else
            {
                //Wrong Decision
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-" + Amount + " XP";
                giveFine(Amount);
            }

        }
        else if (BTNClickedID==3)
        {
            //Meter Expired..
            if (RaycastFromCamera.Instance.CPSscript.isCarMeterExpired)
            {
                //Right Decision
                RightDecisionEconomyPlus(Amount);
                AcheivementManager.Instance.updateParkingTicketAch();

                RightdecisionAmount.text = "+" + Amount + " XP";
                rightDecisionPanel.SetActive(true);
            }
            else
            {
                //Wrong Decision
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-" + Amount + " XP";
                giveFine(Amount);
            }

        }
        MainSelectionScreen.SetActive(false);
        ParkingTicketspunishment.SetActive(false);
        policeCharacter.SetActive(true);
        camera2nd.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);
        FPSControlleer.SetActive(false);
        PencilInCharacterHand.SetActive(true);
        NoteBookinHand.SetActive(true);
        PoliceAnimator.Play("Writing");
        cf2canvas.SetActive(false);
        CenterDot.SetActive(false);
        cotroller.canMove = false;
        cotroller.canRotate = false;
        Handparent.SetActive(false);
        StartCoroutine(GobacktoController());
    }
    IEnumerator GobacktoController()
    {
        yield return new WaitForSeconds(6f);
        Handparent.SetActive(true);
        PencilInCharacterHand.SetActive(false);
        NoteBookinHand.SetActive(false);
        cotroller.canRotate = true;
        cotroller.canMove = true;
        CenterDot.SetActive(true);
        cf2canvas.SetActive(true);
        camera2nd.SetActive(false);
        FPSControlleer.SetActive(true);
        policeCharacter.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        AutoWriter.Instance.WriteTextinDialouge("Hello Sir We have got a lot of reports in this area . Can i Check your Regular Checkup.");
    //    }
    //}


    int Num = 0;

    public void Dialogue1()
    {
        Debug.LogError("Number Value: " + Num);
        if (Num == 0)
        {
            int ran = Random.RandomRange(0, 2);
            if (ran == 0)
            {
                AutoWriter.Instance.SetnameText("Officer");
                AutoWriter.Instance.WriteTextinDialouge("Hello. Can I have a moment of your time?");


            }else if (ran == 1)
            {
                AutoWriter.Instance.SetnameText("Officer");
                AutoWriter.Instance.WriteTextinDialouge("Hi! Can i have a minute of your time?");

            } 
            Num += 1 ;
        }
        else if (Num == 1)
        {
            AutoWriter.Instance.SetnameText("Pedistrain");
            AutoWriter.Instance.WriteTextinDialouge(" Sure, officer. What's the matter?");
            Num += 1;

        }else if (Num == 2)
        {
            AutoWriter.Instance.SetnameText("Officer");
            AutoWriter.Instance.WriteTextinDialouge("Our Police Station have Found a lot of Complaints in this area. And We are checking Most of the People. Please Cooperate. ");

            Num += 1;

        }
        else
        {
            DialoguePanel.SetActive(false);
            MainSelectionScreen.SetActive(true);
 
            Num = 0;
        }


     //   Num = 0;
    }


    //      For Issuing Violance Challan

    public void startIssuingChallan(string selectformode)
    {

       
        MainSelectionScreen.SetActive(false);
        ParkingTicketspunishment.SetActive(false);
        policeCharacter.transform.position = RaycastFromCamera.Instance.PlayerInFront.transform.Find("PolicePos").transform.position;
        policeCharacter.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        camera2nd.SetActive(true);
        FPSControlleer.SetActive(false);
        PoliceAnimator.Play("Writing");
        cf2canvas.SetActive(false);
        CenterDot.SetActive(false);
        PencilInCharacterHand.SetActive(true);
        NoteBookinHand.SetActive(true);
        cotroller.canMove = false;
        cotroller.canRotate = false;
        Handparent.SetActive(false);
        IDCardScanningResults.SetActive(false);
        DrugtestResultpanel.SetActive(false);
        itemsFoundinSearch.SetActive(false);
        AlcoholTestResult.SetActive(false);
        DrugtestResultpanel.SetActive(false);
        DialoguePanel.SetActive(true);
        NextBTN.GetComponent<Button>().interactable = false;
        StartCoroutine(GoBacktoNormalGame(selectformode));

    }

    IEnumerator GoBacktoNormalGame(string mode)
    {
        AutoWriter.Instance.SetnameText("Officer");
        AutoWriter.Instance.WriteTextinDialouge("Issuing You a Violance Ticket. Be Carefull for the Next Time.");
        yield return new WaitForSeconds(6f);
        NextBTN.GetComponent<Button>().interactable = true;

        DialoguePanel.SetActive(false);

        Handparent.SetActive(true);

        cotroller.canRotate = true;
        cotroller.canMove = true;
        CenterDot.SetActive(true);
        cf2canvas.SetActive(true);
        camera2nd.SetActive(false);
        FPSControlleer.SetActive(true);
        policeCharacter.SetActive(false);


        if (mode == InvestigationType.SearchingbyHand.ToString())
        {
            Debug.LogError("Search By Hand Result" + mode);

            if (SearchResultManager.Instance.isitem1wrng
                ||SearchResultManager.Instance.isitem2wrng
                || SearchResultManager.Instance.isitem3wrng
                || SearchResultManager.Instance.isitem4wrng
                )
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updatesearchTicketAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(1);
            }

          
            
        }
        else if (mode == InvestigationType.PassportIDSearch.ToString())
        {
            Debug.LogError("PassPort Search Result" +mode);
            if (
                 IDCardResultsManager.Instance.NameerrorCheck
              || IDCardResultsManager.Instance.DateOfBirthCheck
              || IDCardResultsManager.Instance.NationalityerrorCheck
              || IDCardResultsManager.Instance.IdexpirederrorCheck
              || IDCardResultsManager.Instance.cardnumwrongCheck)
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);

                AcheivementManager.Instance.updateAskForIdAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }


        }
        else if (mode == InvestigationType.AlcoholTest.ToString())
        {
            Debug.LogError("Alcohol Test Result" +mode);
            if (AlcoholTestDataSetting.Instance.is_CDT_ratiofine && AlcoholTestDataSetting.Instance.is_BAC_rationFine)
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }
            else
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();
            }

        }
        else if (mode == InvestigationType.Drugtest.ToString())
        {
            Debug.LogError("Drug Test Result"+mode);
            //Debug.LogError("Drug Test Result" + arrest);
            if (DrugTestResult.Instance.is_THC_fine && DrugTestResult.Instance.is_CBD_Fine)
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }

        }
       /* //For Tutorial
        if (PlayerPrefs.GetInt("RotatingTutorial") != 1)
        {
            PlayerPrefs.SetInt("RotatingTutorial", 1);
        //    TutCompleted.SetActive(true);
            Debug.LogError("Tut");

        }*/
    }



    //Verbally Warning

    public void VerballyWarning(string inverstigationtypecheck)
    {
       
        MainSelectionScreen.SetActive(false);
        ParkingTicketspunishment.SetActive(false);
        policeCharacter.transform.position = RaycastFromCamera.Instance.PlayerInFront.transform.Find("PolicePos").transform.position;
        policeCharacter.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        camera2nd.SetActive(true);
        FPSControlleer.SetActive(false);
        //PoliceAnimator.Play("Writing");
        PoliceAnimator.Play("Talking");
        cf2canvas.SetActive(false);
        CenterDot.SetActive(false);
        PencilInCharacterHand.SetActive(false);
        NoteBookinHand.SetActive(false);
        AndTestMachine.SetActive(false);

        cotroller.canMove = false;
        cotroller.canRotate = false;
        Handparent.SetActive(false);
        IDCardScanningResults.SetActive(false);
        DrugtestResultpanel.SetActive(false);
        itemsFoundinSearch.SetActive(false);
        AlcoholTestResult.SetActive(false);
        DrugtestResultpanel.SetActive(false);

        DialoguePanel.SetActive(true);
        NextBTN.GetComponent<Button>().interactable = false;

        StartCoroutine(WarnComplete(inverstigationtypecheck));

    }


    IEnumerator WarnComplete(string warncompleted)
    {
        AutoWriter.Instance.SetnameText("Officer");
        AutoWriter.Instance.WriteTextinDialouge("Verbally Warining you this Time. Be Carefull For the Next time As an Advice.");


        yield return new WaitForSeconds(6f);
        NextBTN.GetComponent<Button>().interactable = true;

        Handparent.SetActive(true);
        DialoguePanel.SetActive(false);

        cotroller.canRotate = true;
        cotroller.canMove = true;
        CenterDot.SetActive(true);
        cf2canvas.SetActive(true);
        camera2nd.SetActive(false);
        FPSControlleer.SetActive(true);
        policeCharacter.SetActive(false);

        //Warining Type Like From Search , Passport check and Alcoholtests

        if (warncompleted == InvestigationType.SearchingbyHand.ToString())
        {
            Debug.LogError("Search By Hand Result" + warncompleted);
            //Debug.LogError("Search By Hand Result" + mode);

            if (SearchResultManager.Instance.isitem1wrng
                || SearchResultManager.Instance.isitem2wrng
                || SearchResultManager.Instance.isitem3wrng
                || SearchResultManager.Instance.isitem4wrng
                )
            {
                RightDecisionEconomyPlus(10);
                AcheivementManager.Instance.updatesearchTicketAch();
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }


        }
        else if(warncompleted== InvestigationType.PassportIDSearch.ToString())
        {
            Debug.LogError("PassPort Search Result" + warncompleted);
            if (
                 IDCardResultsManager.Instance.NameerrorCheck
              || IDCardResultsManager.Instance.DateOfBirthCheck
              || IDCardResultsManager.Instance.NationalityerrorCheck
              || IDCardResultsManager.Instance.IdexpirederrorCheck
              || IDCardResultsManager.Instance.cardnumwrongCheck)
            {
                //Wrong Decision
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateAskForIdAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }

        }
        else if (warncompleted == InvestigationType.AlcoholTest.ToString())
        {
            Debug.LogError("Alcohol Test Result" + warncompleted);
            if (AlcoholTestDataSetting.Instance.is_CDT_ratiofine && AlcoholTestDataSetting.Instance.is_BAC_rationFine)
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }
            else
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();

            }

        }
        else if (warncompleted == InvestigationType.Drugtest.ToString())
        {
            Debug.LogError("Drug Test Result" + warncompleted);
            if (DrugTestResult.Instance.is_THC_fine && DrugTestResult.Instance.is_CBD_Fine)
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+1 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-1 XP";
                giveFine(10);
            }

        }
     /*   //For Tutorial
        if (PlayerPrefs.GetInt("RotatingTutorial") != 1)
        {
            PlayerPrefs.SetInt("RotatingTutorial", 1);
            Debug.LogError("Tut");

            TutCompleted.SetActive(true);
        }
*/
    }


    //Arresting the Culprit

    public void ArrestingCulprit(string selectformode)
    {
        if (PlayerPrefs.GetInt("MovingTutorial") != 1)
        {
            PlayerPrefs.SetInt("MovingTutorial", 1);

        }
        MainSelectionScreen.SetActive(false);
        ParkingTicketspunishment.SetActive(false);
        policeCharacter.transform.position = RaycastFromCamera.Instance.PlayerInFront.transform.Find("SearchPos").transform.position;
        policeCharacter.SetActive(true);
        camera2nd.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        FPSControlleer.SetActive(false);
        //PoliceAnimator.Play("Writing");
        PoliceAnimator.Play("Handcuffs");
        RaycastFromCamera.Instance.PlayerInFront.GetComponent<Animator>().Play("Handcuffs Response");
        RaycastFromCamera.Instance.PlayerInFront.GetComponent<characterstopper>().isplayerarrested = true ;

        cf2canvas.SetActive(false);
        CenterDot.SetActive(false);
        PencilInCharacterHand.SetActive(false);
        NoteBookinHand.SetActive(false);
        AndTestMachine.SetActive(false);

        cotroller.canMove = false;
        cotroller.canRotate = false;
        Handparent.SetActive(false);
        IDCardScanningResults.SetActive(false);
        DrugtestResultpanel.SetActive(false);
        itemsFoundinSearch.SetActive(false);
        AlcoholTestResult.SetActive(false);
        DrugtestResultpanel.SetActive(false);

        //DialoguePanel.SetActive(true);s
        HathkariofPlayer.SetActive(true);

        StartCoroutine(ArrestedPlayer(selectformode));

    }

    IEnumerator ArrestedPlayer(string arrest)
    {
        yield return new WaitForSeconds(8);
        HathkariofPlayer.SetActive(false);
        RaycastFromCamera.Instance.PlayerInFront.transform.Find("Bip01").transform.Find("Handcuff").transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(4);
        Handparent.SetActive(true);
        DialoguePanel.SetActive(false);

        cotroller.canRotate = true;
        cotroller.canMove = true;
        CenterDot.SetActive(true);
        cf2canvas.SetActive(true);
        camera2nd.SetActive(false);
        FPSControlleer.SetActive(true);
        policeCharacter.SetActive(false);


        if (arrest == InvestigationType.SearchingbyHand.ToString())
        {
            Debug.LogError("Search By Hand Result" + arrest);
            //Debug.LogError("Search By Hand Result" + mode);

            if (SearchResultManager.Instance.isitem1wrng
                || SearchResultManager.Instance.isitem2wrng
                || SearchResultManager.Instance.isitem3wrng
                || SearchResultManager.Instance.isitem4wrng
                )
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updatesearchTicketAch();
                AcheivementManager.Instance.updateArrestAch();

            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }

        }
        else if (arrest == InvestigationType.PassportIDSearch.ToString())
        {
            Debug.LogError("PassPort Search Result" + arrest);


            if (
                   IDCardResultsManager.Instance.NameerrorCheck
                || IDCardResultsManager.Instance.DateOfBirthCheck
                || IDCardResultsManager.Instance.NationalityerrorCheck
                || IDCardResultsManager.Instance.IdexpirederrorCheck
                || IDCardResultsManager.Instance.cardnumwrongCheck)
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateAskForIdAch();
                AcheivementManager.Instance.updateArrestAch();


            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }



        }
        else if (arrest == InvestigationType.AlcoholTest.ToString())
        {
            Debug.LogError("Alcohol Test Result" + arrest);
            if(AlcoholTestDataSetting.Instance.is_CDT_ratiofine && AlcoholTestDataSetting.Instance.is_BAC_rationFine)
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }
            else
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();
                AcheivementManager.Instance.updateArrestAch();


            }



        }
        else if (arrest == InvestigationType.Drugtest.ToString())
        {
            Debug.LogError("Drug Test Result" + arrest);
            if(DrugTestResult.Instance.is_THC_fine && DrugTestResult.Instance.is_CBD_Fine)
            {
                RightDecisionEconomyPlus(10);
                RightdecisionAmount.text = "+10 XP";
                rightDecisionPanel.SetActive(true);
                AcheivementManager.Instance.updateANDTestAch();
                AcheivementManager.Instance.updateArrestAch();


            }
            else
            {
                WrongDecisionPanel.SetActive(true);
                WrongDecisionAmount.text = "-10 XP";
                giveFine(10);
            }
        }
        //For Tutorial
       /* if (PlayerPrefs.GetInt("RotatingTutorial") != 1)
        {
            PlayerPrefs.SetInt("RotatingTutorial", 1);
            TutCompleted.SetActive(true);
            Debug.LogError("Tut");
        }*/


    }

    public void lowBGSound()
    {
        BGSOUND.GetComponent<AudioSource>().volume = 0.1f;
    }
    public void HighBGSound()
    {
        BGSOUND.GetComponent<AudioSource>().volume = 1f;

    }

  /*  public void GamePlayAvatarSystem()
    {
        NameTxt.text = PlayerPrefs.GetString("UserNamePref");
        int oldrank = PlayerPrefs.GetInt("PrefRankNumHolder", 0);
        UserNameRankSlider.value = oldrank;

        XPRankSlider.value = PlayerPrefs.GetInt("PlayerCoins");
        XPGainAndTotalText.text = ""+ XPRankSlider.value +" " +"/ 100";
    }*/
   
}
