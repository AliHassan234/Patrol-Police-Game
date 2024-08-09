using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcheivementManager : Singleton<AcheivementManager>
{
    public readonly static string prefforaskforidAch = "AskForIDAchment";
    public readonly static string AskIDClaimed = "ASKIDCLAIMED";

    [Header("****************   Acheivement One  **************")]

    public Button AskForIDAchmentClaimBTN;
    public Text DoneTasksNum;
    public int rewardamountXP;
    public int MaxTaskNumForAskforid;
    public GameObject ClaimedText;



    public readonly static string prefforParkingTicket = "prefForParkingTicket";
    public readonly static string ParkingTicketClaimed = "ParkingTicketClaim";

    [Header("****************   Acheivement Two  **************")]

    public Button ParkingTicketClaimBTN;
    public Text DoneTasksNumParking;
    public int rewardamountXPParking;
    public int MaxTaskNumForparking;
    public GameObject ClaimedTextACH2;


    public readonly static string prefforSearch = "prefForSearching";
    public readonly static string SearchClaimed = "SearchClaimedPref";

    [Header("****************   Acheivement Three  **************")]

    public Button SearchTicketClaimBTN;
    public Text DoneTasksNumSearch;
    public int rewardamountXPsearch;
    public int MaxTaskNumForsearch;
    public GameObject ClaimedTextACH3;


    public readonly static string prefforAndTest = "prefForAndtest";
    public readonly static string AndtestClaimed = "AndtestClaimed";

    [Header("****************   Acheivement Four  **************")]

    public Button AndTestClaimBTN;
    public Text DoneTasksNumAndTest;
    public int rewardamountXPAndTest;
    public int MaxTaskNumForAndTest;
    public GameObject ClaimedTextACH4;


    public readonly static string prefforArrest = "prefForArrest";
    public readonly static string ArrestClaimed = "ArrestClaimed";

    [Header("****************   Acheivement Five  **************")]

    public Button ArrestClaimBTN;
    public Text DoneTasksNumArrest;
    public int rewardamountXPArrest;
    public int MaxTaskNumForArrest;
    public GameObject ClaimedTextACH5;


    private void Start()
    {

        //For ID Scanning
        AskForIDAchmentClaimBTN.onClick.RemoveAllListeners();
        AskForIDAchmentClaimBTN.onClick.AddListener(onAskForIDClaim);
        //For Parking
        ParkingTicketClaimBTN.onClick.RemoveAllListeners();
        ParkingTicketClaimBTN.onClick.AddListener(ParkingTicketsClaim);
        //For Searching
        SearchTicketClaimBTN.onClick.RemoveAllListeners();
        SearchTicketClaimBTN.onClick.AddListener(SearchTicketsClaim);
        //For And Test
        AndTestClaimBTN.onClick.RemoveAllListeners();
        AndTestClaimBTN.onClick.AddListener(ANDTESTACHClaim);
        //For Arrest
        ArrestClaimBTN.onClick.RemoveAllListeners();
        ArrestClaimBTN.onClick.AddListener(ArrestACHClaim);
    }

    public void OnEnable()
    {
        //For Checking Ach one....  ID Search
        if (PlayerPrefs.GetInt(prefforaskforidAch) >= MaxTaskNumForAskforid)
        {
            if (!PlayerPrefs.HasKey(AskIDClaimed))
            {
                AskForIDAchmentClaimBTN.interactable = true;
                DoneTasksNum.text = "" + MaxTaskNumForAskforid + " / " + MaxTaskNumForAskforid;

            }
            else
            {
                AskForIDAchmentClaimBTN.interactable = false;
                ClaimedText.SetActive(true);
                DoneTasksNum.text = "" + MaxTaskNumForAskforid + " / " + MaxTaskNumForAskforid;

            }

        }
        else
        {
            AskForIDAchmentClaimBTN.interactable = false;
            if(PlayerPrefs.GetInt(prefforaskforidAch)> MaxTaskNumForAskforid)
            {
                DoneTasksNum.text = "" + MaxTaskNumForAskforid + " / " + MaxTaskNumForAskforid;

            }
            else
            {
                DoneTasksNum.text = "" + PlayerPrefs.GetInt(prefforaskforidAch) + " / " + MaxTaskNumForAskforid;

            }
        }

        //For Checking Ach TWO....    Parking Tickets
        if (PlayerPrefs.GetInt(prefforParkingTicket) >= MaxTaskNumForparking)
        {

            if (!PlayerPrefs.HasKey(ParkingTicketClaimed))
            {
                ParkingTicketClaimBTN.interactable = true;
                DoneTasksNumParking.text = "" + MaxTaskNumForparking + " / " + MaxTaskNumForparking;

            }
            else
            {
                ParkingTicketClaimBTN.interactable = false;
                ClaimedTextACH2.SetActive(true);
                DoneTasksNumParking.text = "" + MaxTaskNumForparking + " / " + MaxTaskNumForparking;

            }
        }
        else
        {
            ParkingTicketClaimBTN.interactable = false;
            if (PlayerPrefs.GetInt(prefforParkingTicket) > MaxTaskNumForparking)
            {
                DoneTasksNumParking.text = "" + MaxTaskNumForparking + " / " + MaxTaskNumForparking;

            }
            else
            {
                DoneTasksNumParking.text = "" + PlayerPrefs.GetInt(prefforParkingTicket) + " / " + MaxTaskNumForparking;

            }
        }

        //For Checking Ach 3...        Search
        if (PlayerPrefs.GetInt(prefforSearch) >= MaxTaskNumForsearch)
        {
            if (!PlayerPrefs.HasKey(SearchClaimed))
            {
                SearchTicketClaimBTN.interactable = true;
                DoneTasksNumSearch.text = "" + MaxTaskNumForsearch + " / " + MaxTaskNumForsearch;

            }
            else
            {
                SearchTicketClaimBTN.interactable = false;
                ClaimedTextACH3.SetActive(true);
                DoneTasksNumSearch.text = "" + MaxTaskNumForsearch + " / " + MaxTaskNumForsearch;

            }

        }
        else
        {
            SearchTicketClaimBTN.interactable = false;
            if (PlayerPrefs.GetInt(prefforSearch) > MaxTaskNumForsearch)
            {
                DoneTasksNumSearch.text = "" + MaxTaskNumForsearch + " / " + MaxTaskNumForsearch;

            }
            else
            {
                DoneTasksNumSearch.text = "" + PlayerPrefs.GetInt(prefforSearch) + " / " + MaxTaskNumForsearch;

            }
        }

        //For Checking Ach 4...     AND Tests
        Debug.LogError("And Test Pref " +PlayerPrefs.GetInt(prefforAndTest));
        if (PlayerPrefs.GetInt(prefforAndTest) >= MaxTaskNumForAndTest)
        {
            if (!PlayerPrefs.HasKey(AndtestClaimed))
            {
                AndTestClaimBTN.interactable = true;
                DoneTasksNumAndTest.text = "" + MaxTaskNumForAndTest + " / " + MaxTaskNumForAndTest;

            }
            else
            {
                AndTestClaimBTN.interactable = false;
                ClaimedTextACH4.SetActive(true);
                DoneTasksNumAndTest.text = "" + MaxTaskNumForAndTest + " / " + MaxTaskNumForAndTest;

            }

        }
        else
        {
            AndTestClaimBTN.interactable = false;
            if (PlayerPrefs.GetInt(prefforAndTest) > MaxTaskNumForAndTest)
            {
                DoneTasksNumAndTest.text = "" + MaxTaskNumForAndTest + " / " + MaxTaskNumForAndTest;

            }
            else
            {
                DoneTasksNumAndTest.text = "" + PlayerPrefs.GetInt(prefforAndTest) + " / " + MaxTaskNumForAndTest;

            }
        }

        //For Checking Ach 5...     Arrest Tests

        if (PlayerPrefs.GetInt(prefforArrest) >= MaxTaskNumForArrest)
        {
            if (!PlayerPrefs.HasKey(ArrestClaimed))
            {
                ArrestClaimBTN.interactable = true;
                DoneTasksNumArrest.text = "" + MaxTaskNumForArrest + " / " + MaxTaskNumForArrest;

            }
            else
            {
                ArrestClaimBTN.interactable = false;
                ClaimedTextACH5.SetActive(true);
                DoneTasksNumArrest.text = "" + MaxTaskNumForArrest + " / " + MaxTaskNumForArrest;
            }

        }
        else
        {
            ArrestClaimBTN.interactable = false;
            if (PlayerPrefs.GetInt(prefforArrest) > MaxTaskNumForArrest)
            {
                DoneTasksNumArrest.text = "" + MaxTaskNumForArrest + " / " + MaxTaskNumForArrest;

            }
            else
            {
                DoneTasksNumArrest.text = "" + PlayerPrefs.GetInt(prefforArrest) + " / " + MaxTaskNumForArrest;

            }
        }
    }
    /// <summary>
    /// Below Two Methods are for ID Acheivements;
    /// </summary>
    public void onAskForIDClaim()
    {
        Debug.LogError("ASK ID CLAIM");
        AskForIDAchmentClaimBTN.interactable = false;
        PlayerPrefs.SetInt(AskIDClaimed, 1);
        //So that onEnable Condition not Runs Again And Again.
        updateAskForIdAch();
        updatecoinsui.instance.updatingUI(rewardamountXP);

    }
    public void updateAskForIdAch()
    {
        int old = PlayerPrefs.GetInt(prefforaskforidAch);
        old += 1;
        PlayerPrefs.SetInt(prefforaskforidAch,old);
        //OnEnable();
    }

    /// <summary>
    /// Below Two Methods are for Parking Tickets Acheivements;
    /// </summary>
    public void ParkingTicketsClaim()
    {
        Debug.LogError("Parking Ticket CLAIM");

        ParkingTicketClaimBTN.interactable = false;
        PlayerPrefs.SetInt(ParkingTicketClaimed, 1);

        //So that onEnable Condition not Runs Again And Again.
        updateParkingTicketAch();
        updatecoinsui.instance.updatingUI(rewardamountXPParking);

    }
    public void updateParkingTicketAch()
    {
        int old = PlayerPrefs.GetInt(prefforParkingTicket);
        old += 1;
        PlayerPrefs.SetInt(prefforParkingTicket, old);
        //OnEnable();

    }
    /// <summary>
    /// Below Two Methods are for ID Acheivements;
    /// </summary>

    public void SearchTicketsClaim()
    {
        Debug.LogError("Search Ticket CLAIM");

        SearchTicketClaimBTN.interactable = false;
        PlayerPrefs.SetInt(SearchClaimed, 1);

        //So that onEnable Condition not Runs Again And Again.
        updatesearchTicketAch();
        updatecoinsui.instance.updatingUI(rewardamountXPsearch);

    }
    public void updatesearchTicketAch()
    {
        int old = PlayerPrefs.GetInt(prefforSearch);
        old += 1;
        PlayerPrefs.SetInt(prefforSearch, old);
        //OnEnable();

    }


    /// <summary>
    /// Below Two Methods are for ANDTEST Acheivements;
    /// </summary>

    public void ANDTESTACHClaim()
    {
        Debug.LogError("And Test CLAIM");

        AndTestClaimBTN.interactable = false;
        PlayerPrefs.SetInt(AndtestClaimed, 1);

        //So that onEnable Condition not Runs Again And Again.
        updateANDTestAch();
        updatecoinsui.instance.updatingUI(rewardamountXPAndTest);

    }
    public void updateANDTestAch()
    {
        int old = PlayerPrefs.GetInt(prefforAndTest);
        old += 1;
        PlayerPrefs.SetInt(prefforAndTest, old);
        //OnEnable();

    }

    /// <summary>
    /// Below Two Methods are for ANDTEST Acheivements;
    /// </summary>

    public void ArrestACHClaim()
    {
        Debug.LogError("Arrest CLAIM");

        ArrestClaimBTN.interactable = false;
        PlayerPrefs.SetInt(ArrestClaimed, 1);

        //So that onEnable Condition not Runs Again And Again.
        updateArrestAch();
        updatecoinsui.instance.updatingUI(rewardamountXPArrest);

    }
    public void updateArrestAch()
    {
        int old = PlayerPrefs.GetInt(prefforArrest);
        old += 1;
        PlayerPrefs.SetInt(prefforArrest, old);
        //OnEnable();

    }

}

