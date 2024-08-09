using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IDCardScanner : Singleton<IDCardScanner>
{

    public GameObject Scanner;
    public GameObject IDCard;
    public GameObject PositionofCardObj;

    public Vector3 PositionofCard;
    public Vector3 ScaleofCard;
    public bool isclicked=false;
    private float buttonPressStartTime = 0f;
    public float timeForSecondsPressed=3f;


    // Update is called once per frame
    void Update()
    {
        //Debug.LogError("Working");
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.LogError("Inside Working");

            Scanner.gameObject.SetActive(true);
            isclicked = true;

            buttonPressStartTime = Time.time;
            GameManager.Instance.lowBGSound();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isclicked = false;
            Scanner.gameObject.SetActive(false);
            GameManager.Instance.HighBGSound();
            
        }

        if (isclicked && Time.time - buttonPressStartTime >= timeForSecondsPressed)
        {
            // Button has been pressed for 3 seconds
            Debug.LogError("Button has been pressed for 3 seconds");
            setcardPosition();
            isclicked = false;
            GameManager.Instance.HighBGSound();

        }

        //if (isclicked)
        //{
        //    Scanner.gameObject.SetActive(true);
        //}
        //else
        //{
        //    Scanner.gameObject.SetActive(false);

        //}
    }


    public void setcardPosition()
    {
        GameManager.Instance.IDCardScanningResults.SetActive(true);
        Scanner.SetActive(false);
        IDCard.transform.parent = PositionofCardObj.transform;
        IDCard.transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.Linear);
        IDCard.transform.DOScale(ScaleofCard, 1f).SetEase(Ease.Linear);
        GameManager.Instance.IDCardScanning.SetActive(false);


    }

    public void resetcard()
    {
        IDCard.transform.parent = this.transform;
        IDCard.transform.localPosition = Vector3.zero;
    }



}
