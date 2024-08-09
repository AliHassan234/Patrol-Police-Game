using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoWriter : Singleton<AutoWriter>
{
    public Text textUI;
    public Text NameText;
    public float delayBetweenCharacters = 0.1f;

    public GameObject TypingSound;
    public string fullText;


    //  private string currentText = "";

    //void Start()
    //{
    //    StartCoroutine(WriteText());
    //}

    public void WriteTextinDialouge(string texttowrite)
    {
        GameManager.Instance.NextBTN.GetComponent<Button>().interactable = false;
        StartCoroutine(WriteText(texttowrite));
        TypingSound.SetActive(true);
        TypingSound.GetComponent<AudioSource>().Play();
        GameManager.Instance.lowBGSound();
        //Debug.Log("Writing Text");
    }

    IEnumerator WriteText(string currentText)
    {
        for (int i = 0; i <= currentText.Length; i++)
        {
            fullText = currentText.Substring(0, i);
            //Debug.LogError("Current Character" + currentText);
            textUI.text = fullText;
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
        GameManager.Instance.NextBTN.GetComponent<Button>().interactable = true;
        TypingSound.SetActive(false);
        TypingSound.GetComponent<AudioSource>().Stop();
        GameManager.Instance.HighBGSound();

    }

    public void SetnameText(string name)
    {
        NameText.text = name;
    }
}
