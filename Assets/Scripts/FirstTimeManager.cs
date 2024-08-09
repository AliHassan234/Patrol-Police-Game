using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeManager : Singleton<FirstTimeManager>
{
    //rcc objects
    public GameObject RccCanvas;
    public GameObject Rcccamera;
    public GameObject rcccar;

    //fps objects

    public GameObject fpscanvas;
    public GameObject firstpersoncontroller;
    public GameObject playerspwanafterfirsttime;

    public GameObject GoNearCartxt;
    public GameObject Objective;


    public GameObject NPcs;
    public GameObject otherchargegivingcharacter;
    public GameObject cameraothercharge;

    public GameObject[] AllFirstTimeItems;

    public string prefcheck = "FirstTimeChecker";
    private void Start()
    {
        if (PlayerPrefs.HasKey(prefcheck))
        {
            playerpositiontranfer(playerspwanafterfirsttime);
            ActiveFPS();
            foreach (var item in AllFirstTimeItems)
            {
                item.SetActive(false);
            }
            NPcs.SetActive(true);

        }
        else
        {
            rcccar.SetActive(true);
            RccCanvas.SetActive(false);
            Rcccamera.SetActive(false);
            fpscanvas.SetActive(true);
            firstpersoncontroller.SetActive(true);
            GoNearCartxt.SetActive(true);
            NPcs.SetActive(false);
        }


    }
    public void ActiveFPS()
    {
        RccCanvas.SetActive(false); 
        Rcccamera.SetActive(false);
        rcccar.SetActive(false);
        fpscanvas.SetActive(true);
        firstpersoncontroller.SetActive(true);



    }

    public void ActiveRcc()
    {
        RccCanvas.SetActive(true);
        Rcccamera.SetActive(true);
        rcccar.SetActive(true);
        fpscanvas.SetActive(false);
        firstpersoncontroller.SetActive(false);

    }


    public void setkeyforfirsttime()
    {

        firstpersoncontroller.SetActive(false);
        fpscanvas.SetActive(false);
        cameraothercharge.SetActive(true);
        StartCoroutine(closethefirstscene());
    }
    public void playerpositiontranfer(GameObject to)
    {
        //firstpersoncontroller.transform.SetPositionAndRotation(to.transform.position, to.transform.rotation);
        firstpersoncontroller.transform.position = to.transform.position;
        firstpersoncontroller.transform.rotation = to.transform.rotation;
    }
    IEnumerator closethefirstscene()
    {
        yield return new WaitForSeconds(5f);
        Objective.SetActive(true);
     /*   cameraothercharge.SetActive(false);
        GoNearCartxt.SetActive(false);
        otherchargegivingcharacter.SetActive(false);
        ActiveFPS();
        NPcs.SetActive(true);*/
        PlayerPrefs.SetInt(prefcheck, 1);
       

    }
    

    public void carpositiontransform(GameObject to)
    {
        rcccar.transform.position = to.transform.position;
        
    }

}
