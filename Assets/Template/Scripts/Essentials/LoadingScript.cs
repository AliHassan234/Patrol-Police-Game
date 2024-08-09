using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : SingletonDoNotDestroy<LoadingScript>
{
    public Image loadingFill;
    public Text loadingProgress;
    public float addition, wait;
    public GameObject appOpen, loader;
    //public IEnumerator Start()
    //{
    //    //appOpen.SetActive(true);
    //    //loader.SetActive(false);

    //    //StartCoroutine(appOpenAuto());

    //    //yield return new WaitUntil(() => AdsHandler.appOpenNumber > 0);

    //    //yield return new WaitForSeconds(4);
    //    //if (AdsHandler.instance && AdsHandler.instance.appOpenAd != null)
    //    //{
    //    //    AdsHandler.instance.ShowAppOpenAd();
    //    //}

    //    //appOpen.SetActive(false);
    //    loader.SetActive(true);
    //    //yield return new WaitUntil(()=> !AdsHandler.instance.isShowingAppOpenAd);

    //    if (SceneManager.GetActiveScene().buildIndex == 0)
    //        StartCoroutine(AsynchronousLoad(1));


    //}

    private void Start()
    {
        loader.SetActive(true);
        StartCoroutine(AsynchronousLoad(1));
    }

    public void loadscene(int index)
    {
        StartCoroutine(AsynchronousLoad(index));
    }
    //IEnumerator appOpenAuto()
    //{
    //    yield return new WaitForSeconds(6);
    //    AdsHandler.appOpenNumber = 3;
    //}
    //IEnumerator SetBanner()
    //{
    //    Time.timeScale = 1;
    //    yield return new WaitUntil(() => AdsHandler.instance && AdsHandler.bigBannerAvailable);
    //    Time.timeScale = 1;
    //    AdsHandler.instance.ShowBigBannerAds();
    //    Time.timeScale = 1;
    //    Time.timeScale = 1;
    //    if (AdsHandler.bannerAvailable)
    //    {
    //        Time.timeScale = 1;
    //        AdsHandler.instance.HideBannerAds();
    //        Time.timeScale = 1;
    //    }
    //}
    public IEnumerator AsynchronousLoad(int scene)
    {
        Debug.LogError("Loading Scene");
        
        //AdsHandler.instance.ShowBannerAds();
        Time.timeScale = 1;
     //   StartCoroutine(SetBanner());
        Time.timeScale = 1;
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        int percentage = 0;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;
        while (loadingFill.fillAmount < 1)
        {
            Time.timeScale = 1;
            percentage++;
            if (percentage < 101)
                loadingProgress.text = percentage + " %";
            loadingFill.fillAmount += addition;
            yield return new WaitForSeconds(wait);
        }

        Time.timeScale = 1;
        yield return new WaitUntil(() => ao.progress >= 0.9f);

        Time.timeScale = 1;
        ao.allowSceneActivation = true;

        Time.timeScale = 1;
        Invoke("Test", 2);
    }
    public void Test()
    {

        //AdsHandler.instance.HideBigBannerAds();

        //AdsHandler.instance.ShowBannerAds();
        Time.timeScale = 1;
        loadingProgress.text = 0 + " %";
        loadingFill.fillAmount = 0;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}