using System;
using System.Collections;
using System.Collections.Generic;
using NPCScriptableNS;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public ID myID;
    public List<SearchItem> mySearch;
    public DrugTest myDrugTest;
    public AlcohalTest myAlcohalTest;
    public NPCScriptable NPCScriptable
    {
        get { return GameManager.Instance.nPCScriptable; }
    }
    public bool isIDGood, isSearchGood, isDrugGood, isAlcohalGood;
    public bool isMale;
    public bool setMyData;

    private void OnValidate()
    {
        if (setMyData)
        {
            setMyData = false;
            isIDGood = Random.Range(0,2) == 1;
            isSearchGood = Random.Range(0,2) == 1;
            isDrugGood = Random.Range(0,2) == 1;
            isAlcohalGood = Random.Range(0,2) == 1;

            myID = isIDGood ? (isMale ? NPCScriptable.maleGoodIds[Random.Range(0, NPCScriptable.maleGoodIds.Count)] : NPCScriptable.femaleGoodIds[Random.Range(0, NPCScriptable.femaleGoodIds.Count)]) :
                (isMale ? NPCScriptable.maleBadIds[Random.Range(0, NPCScriptable.maleBadIds.Count)] : NPCScriptable.femaleBadIds[Random.Range(0, NPCScriptable.femaleBadIds.Count)]);

            mySearch = isSearchGood ? NPCScriptable.searchGood[Random.Range(0, NPCScriptable.searchGood.Count)].searchItems : NPCScriptable.searchBad[Random.Range(0, NPCScriptable.searchBad.Count)].searchItems;

            myDrugTest = isDrugGood ? NPCScriptable.drugGoodTests[Random.Range(0, NPCScriptable.drugGoodTests.Count)] : NPCScriptable.drugBadTests[Random.Range(0, NPCScriptable.drugBadTests.Count)];

            myAlcohalTest = isAlcohalGood ? NPCScriptable.alcohalGoodTests[Random.Range(0, NPCScriptable.alcohalGoodTests.Count)] : NPCScriptable.alcohalBadTests[Random.Range(0, NPCScriptable.alcohalBadTests.Count)];
        }
    }

}