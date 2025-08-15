using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float uiChangeDuration = 2.5f;

    [Header("Ingame UI")]
    [SerializeField] GameObject remainedTimeUI;
    [SerializeField] TextMeshProUGUI remainedTimeText;

    [Header("> Collectible")]
    [SerializeField] GameObject itemArray;
    [SerializeField] Sprite itemArrayWhenEmpty;
    [SerializeField] Color colorWhenEmpty = Color.white;

    [Header("> Messege")]
    [SerializeField] GameObject memoContentBG;
    [SerializeField] TextMeshProUGUI memoContentText;

    [Header("> Explosive FX")]
    [SerializeField] GameObject explosiveFxBg;

    [Header("Game Over")]
    [SerializeField] GameObject gameOverBG;
    [SerializeField] TextMeshProUGUI gameOverTitleText;
    [SerializeField] TextMeshProUGUI gameOverDescText;

    [Header("Game Complete")]
    [SerializeField] GameObject gameCompleteBG;
    [SerializeField] GameObject gameCompleteDesc;
    [SerializeField] TextMeshProUGUI gameCompleteTimeText;

    [Header("Navigator Button")]
    [SerializeField] GameObject navigatorButtonUI;

    [Header("> Settings")]
    [SerializeField] TMP_Dropdown languageDropdown;

    void Start()
    {
        if (languageDropdown != null) StartCoroutine(SetupLocalesRoutine());
    }

    // Item UI Manangement
    internal void UpdateItemList()
    {
        int collectibleCount = GameManager.instance.player.transform.childCount;
        int collectibleArrayCount = itemArray.transform.childCount;
        //Debug.Log("collectibleCount: " + collectibleCount + ", collectibleArrayCount: " + collectibleArrayCount);

        // Apply sprite icon
        for (int i = 1; i < collectibleCount; i++)
        {
            Image targetIconApplied = itemArray.transform.GetChild(i).GetComponent<Image>(); // Define where the sprite applied
            Transform targetCollectible = GameManager.instance.player.transform.GetChild(i); // Define where the sprite refered

            if (collectibleCount <= collectibleArrayCount)
            {
                if (targetCollectible.GetComponent<SpriteRenderer>())
                {
                    // Changes image of icon
                    if (targetCollectible.GetComponent<Collectible>().iconSprite)  // If collectible has its thumb of item
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
        for (int i = collectibleCount; i < collectibleArrayCount; i++)
        {
            Image targetIconApplied = itemArray.transform.GetChild(i).GetComponent<Image>();
            targetIconApplied.sprite = itemArrayWhenEmpty;
            targetIconApplied.color = colorWhenEmpty;
        }
    }

    // Time Limits
    internal void ShowTimeLimit()
    {
        remainedTimeUI.SetActive(true);
    }
    internal void UpdateTimeText(int value)
    {
        remainedTimeText.text = value + "";
    }

    // Memo Text
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

    // Explosive FX
    internal void ShowExplosive()
    {
        explosiveFxBg.SetActive(true);
    }
    internal void ResetExplosive()
    {
        explosiveFxBg.SetActive(false);
    }

    // Game Over
    internal void ShowGameOverBG()
    {
        gameOverBG.SetActive(true);
    }
    internal void ShowGameOverTitle()
    {
        gameOverTitleText.gameObject.SetActive(true);
    }
    internal void ShowGameOverDesc()
    {
        gameOverDescText.gameObject.SetActive(true);
    }

    // Game Complete
    internal void ShowCompleteTitle()
    {
        gameCompleteBG.SetActive(true);
    }
    internal void ShowCompleteDesc()
    {
        gameCompleteDesc.SetActive(true);
    }
    internal void ShowCompleteTime(int second)
    {
        gameCompleteTimeText.text = second + "s";
        gameCompleteTimeText.gameObject.SetActive(true);
    }

    // Show Navigator Button
    internal void ShowNavigatorButton()
    {
        navigatorButtonUI.SetActive(true);
    }

    // Set Language
    private IEnumerator SetupLocalesRoutine()
    {
        // Wait until Localization is Initialized
        yield return LocalizationSettings.InitializationOperation;

        // Bring all locales
        var locales = LocalizationSettings.AvailableLocales.Locales;
        var options = new List<TMP_Dropdown.OptionData>();
        int currentLocaleIndex = 0;

        // Add locale name as dropdown
        for (int i = 0; i < locales.Count; i++)
        {
            var locale = locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
            {
                currentLocaleIndex = i;
            }
            options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
        }

        // Remove default options and replace
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(options);

        languageDropdown.SetValueWithoutNotify(currentLocaleIndex);
        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }

    public void SetLanguage(int index)
    {
        StartCoroutine(SetLocaleRoutine(index));
    }

    private IEnumerator SetLocaleRoutine(int index)
    {
        // Turn off Dropdown while changing language
        languageDropdown.interactable = false;

        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = selectedLocale;
        yield return LocalizationSettings.InitializationOperation;

        languageDropdown.interactable = true;
    }
}
