using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float uiChangeDuration = 2.5f;

    [Header("Collectible UI")]
    [SerializeField] GameObject itemArray;
    [SerializeField] Sprite itemArrayWhenEmpty;
    [SerializeField] Color colorWhenEmpty = Color.white;

    [Header("Messege UI")]
    [SerializeField] GameObject memoContentBG;
    [SerializeField] TextMeshProUGUI memoContentText;

    [Header("Stage Intro")]
    [SerializeField] GameObject stageIntroBG;
    [SerializeField] TextMeshProUGUI stageIntroIndexText, stageIntroTitleText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Item UI Manangement
    internal void UpdateItemList()
    {
        int collectibleCount = GameManager.instance.player.transform.childCount;
        int collectibleArrayCount = itemArray.transform.childCount;
        //Debug.Log("collectibleCount: " + collectibleCount + ", collectibleArrayCount: " + collectibleArrayCount);

        // Apply sprite icon
        for(int i = 1; i < collectibleCount; i++)
        {
            Image targetIconApplied = itemArray.transform.GetChild(i).GetComponent<Image>(); // Define where the sprite applied
            Transform targetCollectible = GameManager.instance.player.transform.GetChild(i); // Define where the sprite refered

            if(collectibleCount <= collectibleArrayCount)
            {
                if(targetCollectible.GetComponent<SpriteRenderer>())
                {
                    // Changes image of icon
                    if(targetCollectible.GetComponent<Collectible>().iconSprite)  // If collectible has its thumb of item
                    {
                        Debug.Log("UI has got its icon from iconSprite");
                        targetIconApplied.sprite = targetCollectible.GetComponent<Collectible>().iconSprite;
                    }
                    else
                    {
                        targetIconApplied.sprite = targetCollectible.GetComponent<SpriteRenderer>().sprite;
                    }
                    targetIconApplied.color = targetCollectible.GetComponent<SpriteRenderer>().color;

                    // Changes color of icon
                    Color savedColor = targetIconApplied.color;
                    savedColor.a = 1f;
                    targetIconApplied.color = savedColor;
                }
                else
                {
                    Debug.LogError("This collectible has no Sprite Renderer, add Sprite on this object");
                }
            }
            else
            {
                Debug.LogWarning("There is no space for showing collectibles!");
            }
        }

        // Apply empty icon
        for(int i = collectibleCount; i < collectibleArrayCount; i++)
        {
            Image targetIconApplied = itemArray.transform.GetChild(i).GetComponent<Image>();
            targetIconApplied.sprite = itemArrayWhenEmpty;
            targetIconApplied.color = colorWhenEmpty;
        }
    }

    /*
    public void ShowCaption(int index, string name)
    {
        stageIntroBG.SetActive(true);
        stageIntroIndexText.text = index + "";
        stageIntroTitleText.text = name;

        // Off after plenty of time
        StartCoroutine(ShowCaptionCo());
    }

    private IEnumerator ShowCaptionCo()
    {
        yield return new WaitForSeconds(uiChangeDuration);
        stageIntroBG.SetActive(false);
    }
    */

    internal void ShowMemoText(string contentString)
    {
        memoContentBG.SetActive(true);
        memoContentText.text = contentString;
    }

    internal void ShowMemoTextAsIntro(int index, string title)
    {
        memoContentBG.SetActive(true);
        memoContentText.text = index + "-" + title;
        StartCoroutine(ResetMemoTextAuto());
    }

    internal IEnumerator ResetMemoTextAuto()
    {
        yield return new WaitForSeconds(uiChangeDuration);
        ResetMemoText();
    }

    internal void ResetMemoText()
    {
        memoContentBG.SetActive(false);
        memoContentText.text = "";
    }
}
