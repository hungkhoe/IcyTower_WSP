using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBoardingManager : MonoBehaviour
{
    [Header("Page Sprite")]
    [SerializeField] private Sprite[] onBoardPages;

    [Header("Page Variable")]
    [SerializeField] private Transform firstOnboardPage;
    [SerializeField] private Transform secondOnboardPage;
    [SerializeField] private Transform onBoardParent;

    [Header("Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Page Stats")]
    [SerializeField] private float movingPageSpeed;

    private int indexPage = 0;
    private float screenWidth;

    private bool isMainPage = true;// check if main page is show or 2nd page is show
    private bool isChangePage = false;  

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        nextButton.onClick.AddListener(NextOnboardButton);
        previousButton.onClick.AddListener(PreviousOnboardButton);

        screenWidth = Screen.width;

        previousButton.interactable = false;
    }

    public void NextOnboardButton()
    {
        if (isChangePage)
            return;

        indexPage++;

        if(indexPage == onBoardPages.Length - 1)
            nextButton.interactable = false;

        if (indexPage != 0)
            previousButton.interactable = true;

        if (isMainPage)
        {   
            // re position other page to reuse it as pooling
            secondOnboardPage.localPosition = new Vector3(firstOnboardPage.localPosition.x + screenWidth, firstOnboardPage.localPosition.y, firstOnboardPage.localPosition.z);
            secondOnboardPage.GetComponent<Image>().sprite = onBoardPages[indexPage];
        }
        else
        {   
            // re position other page to reuse it as pooling
            firstOnboardPage.localPosition = new Vector3(secondOnboardPage.localPosition.x + screenWidth, secondOnboardPage.localPosition.y, secondOnboardPage.localPosition.z);
            firstOnboardPage.GetComponent<Image>().sprite = onBoardPages[indexPage];
        }

        isMainPage = !isMainPage;

        StartCoroutine(NextOnboardCouroutine());
    }
    IEnumerator NextOnboardCouroutine()
    {
        isChangePage = true;

        Vector3 desitnation = onBoardParent.transform.localPosition;
        desitnation.x -= screenWidth;

        while (MyUtilities.DistanceVector3(onBoardParent.transform.localPosition, desitnation) > MyUtilities.Square(0.1f))
        {
            onBoardParent.transform.localPosition = Vector3.MoveTowards(onBoardParent.transform.localPosition, desitnation, movingPageSpeed * Time.deltaTime);
            yield return null;
        }

        isChangePage = false;
    }

    public void PreviousOnboardButton()
    {
        if (isChangePage)
            return;

        indexPage--;      

        if(indexPage == 0)
            previousButton.interactable = false;

        if (indexPage != onBoardPages.Length - 1)
            nextButton.interactable = true;

        if (isMainPage)
        {
            // re position other page to reuse it as pooling
            secondOnboardPage.localPosition = new Vector3(firstOnboardPage.localPosition.x - screenWidth, firstOnboardPage.localPosition.y, firstOnboardPage.localPosition.z);
            secondOnboardPage.GetComponent<Image>().sprite = onBoardPages[indexPage];
        }
        else
        {
            // re position other page to reuse it as pooling
            firstOnboardPage.localPosition = new Vector3(secondOnboardPage.localPosition.x - screenWidth, secondOnboardPage.localPosition.y, secondOnboardPage.localPosition.z);
            firstOnboardPage.GetComponent<Image>().sprite = onBoardPages[indexPage];
        }

        isMainPage = !isMainPage;

        StartCoroutine(PreviousOnboardCouroutine());
    }
    IEnumerator PreviousOnboardCouroutine()
    {
        isChangePage = true;

        Vector3 desitnation = onBoardParent.transform.localPosition;
        desitnation.x += screenWidth;

        while (MyUtilities.DistanceVector3(onBoardParent.transform.localPosition, desitnation) > MyUtilities.Square(0.1f))
        {
            onBoardParent.transform.localPosition = Vector3.MoveTowards(onBoardParent.transform.localPosition, desitnation, movingPageSpeed * Time.deltaTime);
            yield return null;
        }

        isChangePage = false;
    }

    public void SkipButton()
    {
        gameObject.SetActive(false);
        GameManager.Instance.isPause = false;
    }
}
