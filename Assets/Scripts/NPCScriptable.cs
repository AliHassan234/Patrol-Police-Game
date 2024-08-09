using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NPCScriptableNS
{
    [CreateAssetMenu(fileName = "NPCScriptable", menuName = "Police Patrol/NPCScriptable")]
    public class NPCScriptable : ScriptableObject
    {
        public List<ID> maleGoodIds, femaleGoodIds, maleBadIds, femaleBadIds;
        public List<Search> searchGood, searchBad;
        public List<DrugTest> drugGoodTests, drugBadTests;
        public List<AlcohalTest> alcohalGoodTests, alcohalBadTests;
    }
    [Serializable]
    public struct ID
    {
        public string dateOfBrith, issueDate, expiryDate, name, gender, state, idCardNum;
        public int wrongNum;
        public Sprite characterSP, signatureSP;
        public bool isdateofbirthwrong, isidcardexpired, isstatewrong, isidcardnumwwrong;
    }

    [Serializable]
    public struct Search
    {
        public List<SearchItem> searchItems;
    }

    [Serializable]
    public struct SearchItem
    {
        public string itemName;
        public Sprite itemSprite;
        public bool isWrong;
    }

    [Serializable]
    public struct DrugTest
    {
        public string THCLevel, CBDLevel;
        public bool isTHCLevelWrong, isCBDLevelWrong;
    }

    [Serializable]
    public struct AlcohalTest
    {
        public string BACLevel, CTDLevel;
        public bool isBACLevelWrong, isCTDLevelWrong;
    }
}