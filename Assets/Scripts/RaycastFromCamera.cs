using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RaycastFromCamera :Singleton<RaycastFromCamera>
{
    public float raycastRange = 10f; // The range of the raycast

    public Camera mainCamera;
    public GameObject nextplayercam;
  


    public GameObject Hand;
    public GameObject PlayerInFront;
    public GameObject Mode2NotOpenedPanel;
    public static bool issomeplayerstopped = false;
    public float yrotation = 0f;

    public Vector3 Rotation;

    public bool isDetectedThingIsPerson = true;
    public CarparkedStatus CPSscript;

    public float rotationSpeed;
    private NPCManager getrefNPCManager;

    public float xvalue;
    public float zvalue;
    //private void Start()
    //{
    //  //  mainCamera = Camera.main; // Get the main camera

    //}

    private void Update()
    {
        // Cast a ray from the camera
        //  Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(mainCamera.transform.position,mainCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits something within the specified range
      
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            GameObject hitObject = hit.collider.gameObject;
            if(hit.collider.gameObject.tag== "Emerald AI")
            {
                PlayerInFront = hit.collider.gameObject;
                
              //  StopFrontPlayer();
                isDetectedThingIsPerson = true;

                if (!PlayerInFront.GetComponent<characterstopper>().isplayerarrested)
                {
                      Hand.SetActive(true);

                }
                else
                {
                    Hand.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.tag == "Car")
            {
                Hand.SetActive(true);
                //if (TryGetComponent(out CPSscript))
                //{
                    
                //}
                CPSscript = hit.collider.gameObject.GetComponent<CarparkedStatus>();
                isDetectedThingIsPerson = false;

            }else if (hit.collider.gameObject.tag == "Mode2Walls")
            {
                Mode2NotOpenedPanel.SetActive(true);
            }

            else
            {
                Hand.SetActive(false);
                Mode2NotOpenedPanel.SetActive(false);

                ResumeFrontPlayer();

                
            }
         //   Debug.Log("Hit object: " + hitObject.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
            // Here you can do whatever you want with the hit object
        }
        else
        {
            Hand.SetActive(false);
            ResumeFrontPlayer();
            Mode2NotOpenedPanel.SetActive(false);

            // Debug.Log("No object within range.");
        }


        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Vector3 newpos = PlayerInFront.transform.Find("PolicePos").transform.position;
        //    newpos += new Vector3(xvalue, 0f, zvalue);
        //    Debug.LogError("New Pos: " + newpos);
        //    GameManager.Instance.policeCharacter.transform.position = newpos;
        //}
    }

    public void StopFrontPlayer()
    {

        if (!issomeplayerstopped)
        {
             PlayerInFront.GetComponent<characterstopper>().StopPlayer();
             PlayerInFront.transform.LookAt(this.transform);

             // RotateTowards(this.transform);

             PlayerInFront.transform.eulerAngles = new Vector3(0, PlayerInFront.transform.eulerAngles.y, PlayerInFront.transform.eulerAngles.z);
            nextplayercam.SetActive(true);
     /*       nextplayercam.transform.position = PlayerInFront.transform.Find("campos").transform.position;
            nextplayercam.transform.rotation = PlayerInFront.transform.Find("campos").transform.rotation;*/
            nextplayercam.transform.DOMove(PlayerInFront.transform.Find("campos").transform.position, 0.3f).SetEase(Ease.Linear);
            nextplayercam.transform.DORotateQuaternion(PlayerInFront.transform.Find("campos").transform.rotation, 0.3f).SetEase(Ease.Linear);

            //  PlayerInFront.transform.DOLookAt(this.transform.position, 1f).SetEase(Ease.Linear);
            //PlayerInFront.transform.DOLookAt(this.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() =>
            //{
            //    PlayerInFront.transform.eulerAngles = new Vector3(0, PlayerInFront.transform.eulerAngles.y, PlayerInFront.transform.eulerAngles.z);

            //});s



            //  PlayerInFront.transform.DOBlendableRotateBy(new Vector3(0f, PlayerInFront.transform.eulerAngles.y, PlayerInFront.transform.eulerAngles.z),1f).SetEase(Ease.Linear);




            //  StartCoroutine(waitforlookat());
            //yrotation = PlayerInFront.transform.rotation.y;
            //Debug.LogError("Yrotation Saving" + yrotation);
            //Quaternion quaternion = PlayerInFront.transform.rotation;
            //Debug.LogError("Quaternion: "+quaternion);


            //PlayerInFront.transform.localRotation = Quaternion.EulerAngles(0f, yrotation,quaternion.z);
            //Debug.Log("Before Y"+PlayerInFront.transform.rotation.y);
        }
        //  PlayerInFront.transform.LookAt(this.gameObject.transform);
    }

    public void ResumeFrontPlayer()
    {
        if (issomeplayerstopped)
        {
          PlayerInFront.GetComponent<characterstopper>().ResumePlayer();
            nextplayercam.transform.localPosition = Vector3.zero;
            nextplayercam.transform.localRotation = Quaternion.identity;

        }

    }


    //public IEnumerator waitforlookat()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Rotation = PlayerInFront.transform.rotation.eulerAngles;

    //    // Quaternion quaternion = PlayerInFront.transform.rotation;

    //    PlayerInFront.transform.Rotate(Rotation);

    //    //Quaternion quaternion = Quaternion.Euler(0f, yrotation, 0f) ;
    //    //PlayerInFront.transform.rotation = quaternion;
    //    ////PlayerInFront.transform.Rotate(0f, yrotation, 0f);
    //    Debug.LogError("Rotate: " + PlayerInFront.transform.rotation);




    //    //yield return null;
    //}

    public void onHandClick()
    {
        if(isDetectedThingIsPerson)
        {
            //Start Dialouge System here..

            StopFrontPlayer();

            GameManager.Instance.AskIdBTN.interactable = true;
            GameManager.Instance.SearchBTN.interactable = true;
            GameManager.Instance.AndTesTBTN.interactable = true;
            GameManager.Instance.TicketsPunish.interactable = false;

            GameManager.Instance.cotroller.canMove = false;
            GameManager.Instance.cotroller.canRotate = false;
            GameManager.Instance.Handparent.SetActive(false);
            GameManager.Instance.DialoguePanel.SetActive(true);
            GameManager.Instance.cf2canvas.SetActive(false);
            
            GameManager.Instance.Dialogue1();
        }
        else
        {
            //Show Car Ticket Here..
            GameManager.Instance.TicketsPunish.interactable = true;

            GameManager.Instance.MainSelectionScreen.SetActive(true);
            GameManager.Instance.AskIdBTN.interactable = false;
            GameManager.Instance.SearchBTN.interactable = false;
            GameManager.Instance.AndTesTBTN.interactable = false;


        }
    }



   public void EndIvestigation()
    {
        PlayerInFront.GetComponent<characterstopper>().ResumePlayer();
        GameManager.Instance.cotroller.canMove = true;
        GameManager.Instance.cotroller.canRotate = true;
        GameManager.Instance.cf2canvas.SetActive(true);
        GameManager.Instance.Handparent.SetActive(true);


    }

    //This is For Alcohol Test

    public void PerformSharabTest()
    {
        GameManager.Instance.policeCharacter.transform.position = PlayerInFront.transform.Find("PolicePos").transform.position;
        GameManager.Instance.cotroller.canMove = false;
        GameManager.Instance.cotroller.canRotate = false;
        GameManager.Instance.Handparent.SetActive(false);
        GameManager.Instance.cf2canvas.SetActive(false);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        GameManager.Instance.policeCharacter.SetActive(true);
        GameManager.Instance.camera2nd.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        GameManager.Instance.PoliceAnimator.Play("Sharabi");
        PlayerInFront.GetComponent<Animator>().Play("AlcoholTestResponse");
        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(true);
        GameManager.Instance.SharabiTestsMain.SetActive(false);

        StartCoroutine(CompletedSharabTest());

    }

    IEnumerator CompletedSharabTest()
    {
        yield return new WaitForSeconds(6f);

        GameManager.Instance.cotroller.canMove = true;
        GameManager.Instance.cotroller.canRotate = true;
        GameManager.Instance.Handparent.SetActive(true);
        GameManager.Instance.cf2canvas.SetActive(true);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        GameManager.Instance.policeCharacter.SetActive(false);
        GameManager.Instance.camera2nd.SetActive(false);
        GameManager.Instance.PoliceAnimator.Play("Idle");
        PlayerInFront.GetComponent<Animator>().Play("Idle States");

        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(false);
        GameManager.Instance.AlcoholTestResult.SetActive(true);
        GameManager.Instance.SharabiTestsMain.SetActive(false);

        getrefNPCManager = PlayerInFront.GetComponent<NPCManager>();
        AlcoholTestDataSetting.Instance.init(getrefNPCManager.myAlcohalTest.BACLevel, getrefNPCManager.myAlcohalTest.CTDLevel, getrefNPCManager.myAlcohalTest.isBACLevelWrong,getrefNPCManager.myAlcohalTest.isCTDLevelWrong);
        
    }

    //This is For Drug Test
    public void PerformDrugTest()
    {
        GameManager.Instance.policeCharacter.transform.position = PlayerInFront.transform.Find("PolicePos").transform.position;
        GameManager.Instance.cotroller.canMove = false;
        GameManager.Instance.cotroller.canRotate = false;
        GameManager.Instance.Handparent.SetActive(false);
        GameManager.Instance.cf2canvas.SetActive(false);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        GameManager.Instance.policeCharacter.SetActive(true);
        GameManager.Instance.camera2nd.SetActive(true);
        GameManager.Instance.PoliceAnimator.Play("Sharabi");
        PlayerInFront.GetComponent<Animator>().Play("AlcoholTestResponse");

        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(true);
        GameManager.Instance.SharabiTestsMain.SetActive(false);

        StartCoroutine(CompletedDrugTest());

    }

    IEnumerator CompletedDrugTest()
    {
        yield return new WaitForSeconds(6f);

        GameManager.Instance.cotroller.canMove = true;
        GameManager.Instance.cotroller.canRotate = true;
        GameManager.Instance.Handparent.SetActive(true);
        GameManager.Instance.cf2canvas.SetActive(true);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        GameManager.Instance.policeCharacter.SetActive(false);
        GameManager.Instance.camera2nd.SetActive(false);
        GameManager.Instance.PoliceAnimator.Play("Idle");
        PlayerInFront.GetComponent<Animator>().Play("Idle States");

        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(false);
        GameManager.Instance.DrugtestResultpanel.SetActive(true);
        GameManager.Instance.SharabiTestsMain.SetActive(false);

        getrefNPCManager = PlayerInFront.GetComponent<NPCManager>();
        DrugTestResult.Instance.init(getrefNPCManager.myDrugTest.THCLevel, getrefNPCManager.myDrugTest.CBDLevel, getrefNPCManager.myDrugTest.isTHCLevelWrong, getrefNPCManager.myDrugTest.isCBDLevelWrong);
       
    }


    //This is For Searching In Pedistrain
    public void PerformSearchOnNextPlayer()
    {
        Vector3 newpos= PlayerInFront.transform.Find("SearchPos").transform.position;
        //newpos += new Vector3(0.18f, 0f,0.16f);
        //Debug.LogError("New Pos: " + newpos);
        GameManager.Instance.policeCharacter.transform.position = newpos;
        GameManager.Instance.cotroller.canMove = false;
        GameManager.Instance.cotroller.canRotate = false;
        GameManager.Instance.Handparent.SetActive(false);
        GameManager.Instance.cf2canvas.SetActive(false);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        GameManager.Instance.policeCharacter.SetActive(true);
        GameManager.Instance.camerasearch.SetActive(true);
        RaycastFromCamera.Instance.nextplayercam.SetActive(false);

        GameManager.Instance.PoliceAnimator.Play("PoliceChecking");
        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(false);
        GameManager.Instance.SharabiTestsMain.SetActive(false);

        StartCoroutine(SearchPerformed());

    }

    IEnumerator SearchPerformed()
    {
        yield return new WaitForSeconds(11f);

        GameManager.Instance.cotroller.canMove = true;
        GameManager.Instance.cotroller.canRotate = true;
        GameManager.Instance.Handparent.SetActive(true);
        GameManager.Instance.cf2canvas.SetActive(true);
        GameManager.Instance.MainSelectionScreen.SetActive(false);
        GameManager.Instance.policeCharacter.SetActive(false);
        GameManager.Instance.camerasearch.SetActive(false);
        GameManager.Instance.PoliceAnimator.Play("Idle");
        GameManager.Instance.NoteBookinHand.SetActive(false);
        GameManager.Instance.PencilInCharacterHand.SetActive(false);
        GameManager.Instance.AndTestMachine.SetActive(false);
        GameManager.Instance.itemsFoundinSearch.SetActive(true);
        GameManager.Instance.SharabiTestsMain.SetActive(false);
        getrefNPCManager = PlayerInFront.GetComponent<NPCManager>();
        //DrugTestResult.Instance.init(getrefNPCManager.myDrugTest.THCLevel, getrefNPCManager.myDrugTest.CBDLevel, getrefNPCManager.myDrugTest.isTHCLevelWrong, getrefNPCManager.myDrugTest.isCBDLevelWrong);
        SearchResultManager.Instance.init(getrefNPCManager.mySearch[0].itemName, getrefNPCManager.mySearch[1].itemName, getrefNPCManager.mySearch[2].itemName, getrefNPCManager.mySearch[3].itemName,
            getrefNPCManager.mySearch[0].itemSprite, getrefNPCManager.mySearch[1].itemSprite, getrefNPCManager.mySearch[2].itemSprite, getrefNPCManager.mySearch[3].itemSprite,
            getrefNPCManager.mySearch[0].isWrong, getrefNPCManager.mySearch[1].isWrong, getrefNPCManager.mySearch[2].isWrong, getrefNPCManager.mySearch[3].isWrong
            );

       
    }

    public void PerformIDCardScan()
    {
        getrefNPCManager = PlayerInFront.GetComponent<NPCManager>();
        IDCardTestManager.Instance.init(getrefNPCManager.myID.dateOfBrith,getrefNPCManager.myID.issueDate,getrefNPCManager.myID.expiryDate,getrefNPCManager.myID.name,getrefNPCManager.myID.gender,getrefNPCManager.myID.state,getrefNPCManager.myID.idCardNum,getrefNPCManager.myID.characterSP,getrefNPCManager.myID.signatureSP);
        IDCardResultsManager.Instance.init(false, getrefNPCManager.myID.isdateofbirthwrong, getrefNPCManager.myID.isstatewrong, getrefNPCManager.myID.isidcardexpired, getrefNPCManager.myID.isidcardnumwwrong);
    }

    

}