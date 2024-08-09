/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using static SharedPref;

public class TutorialManager : Singleton<TutorialManager>
{
    public GameObject moveTut,rotateTut;
    public RigidbodyFirstPersonController controller;

    public GameObject[] Tutorialobjects;
    public GameObject ClickOnThisHand;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Moving: " + PlayerPrefs.GetInt(MovingTutorial));
        Debug.LogError("Rotating: " + PlayerPrefs.GetInt(RotatingTutorial));

        if (moveTut==null && rotateTut == null)
            return;

        if(PlayerPrefs.GetInt(MovingTutorial)== 1 && PlayerPrefs.GetInt(RotatingTutorial) == 1)
        {
            controller.canMove = true;
            controller.canRotate = true;
            //Tutorialobjects.SetActive(false);
            foreach (var item in Tutorialobjects)
            {
                item.SetActive(false);
            }
        }

        
        if (PlayerPrefs.GetInt(MovingTutorial) != 1)
        {
            moveTut.SetActive(true);
            controller.canMove = true;
            controller.canRotate = false;
           // this.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {

        ShowRotationTutorial();
    }
    public void ShowRotationTutorial()
    {
        if (moveTut == null && rotateTut == null)
            return;
        if (PlayerPrefs.GetInt(RotatingTutorial) != 1)
        {
            rotateTut.SetActive(true);
            controller.canRotate = true;
        }
    }


    public void onfinishwhileplay()
    {
        foreach (var item in Tutorialobjects)
        {
            item.SetActive(false);
        }
    }
}
*/