using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public UIManager uiManager;
    [SerializeField] List<string> stageNameList;
    private readonly float stageIntroDuration = 3f;

    [Header("Gameplay")]
    [SerializeField] int second = 0;
    [SerializeField] int secondGameOver = 480;

    [SerializeField] internal int playStatus = 0;
    [SerializeField] private bool hasTimeLimit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        InspectComponentsReady();
        UpdateRemainedTime();
    }

    public void SetGameStart(bool useTimeLimit)
    {
        hasTimeLimit = useTimeLimit;
        playStatus = 2;
    }

    public void SetTimeLimitStart()
    {
        if(hasTimeLimit)
        {
            uiManager.ShowTimeLimit();
            second = 0;
            playStatus = 1;
        }
        StartCoroutine(CheckSecond());
    }

    void InspectComponentsReady()
    {
        if(player == null)
        {
            Debug.LogError("PlayerController(Player) is Missing");
        }
    }

    IEnumerator CheckSecond()
    {
        while(playStatus != 0)
        {
            yield return new WaitForSeconds(1f);
            second++;
            UpdateRemainedTime();
            if((second > secondGameOver) && hasTimeLimit)
            {
                Debug.LogError("Game Over!");
                playStatus = 0;
                SetGameOver();
            }
        }
    }

    void UpdateRemainedTime()
    {
        uiManager.UpdateTimeText(secondGameOver - second);
    }

    // Update is called once per frame
    void Update()
    {
        player.DoPlayerAction();
    }

    // Related with gameplay
    public void TeleportTo(GameObject target)
    {
        player.transform.position = target.transform.position;
    }

    // Related with UI
    internal void UpdateItemList()
    {
        uiManager.UpdateItemList();
    }

    public void ShowMemoText(string contentString)
    {
        uiManager.ShowMemoText(contentString);
    }
    public void ResetMemoText()
    {
        uiManager.ResetMemoText();
    }

    public void ShowMemoTextAsCaption(string contentString)
    {
        uiManager.ShowMemoText(contentString);
        Invoke(nameof(ResetMemoText), stageIntroDuration);
    }

    public void ShowIntroOf(int index)
    {
        ShowStageIntro(index, stageNameList[index]);
    }
    public void ShowStageIntro(int index, string title)
    {
        uiManager.ShowMemoTextAsIntro(index, title);
        //Invoke(nameof(uiManager.ResetMemoText, ));
    }

    public void ShowExplosiveFX(float fxDuration = 0.1f)
    {
        StartCoroutine(ShowExplosiveFXCo(fxDuration));
    }

    IEnumerator ShowExplosiveFXCo(float fxDuration)
    {
        uiManager.ShowExplosive();
        yield return new WaitForSeconds(fxDuration);
        uiManager.ResetExplosive();
    }

    // Game Over
    public void SetGameOver()
    {
        playStatus = 0;
        StartCoroutine(SetGameOverUICo());
    }
    IEnumerator SetGameOverUICo()
    {
        StartCoroutine(ShowExplosiveFXCo(0.1f));
        uiManager.ShowGameOverBG();
        yield return new WaitForSeconds(2f);
        uiManager.ShowGameOverTitle();
        yield return new WaitForSeconds(2f);
        uiManager.ShowGameOverDesc();
        uiManager.ShowNavigatorButton();
    }

    // Game Complete
    public void SetGameComplete()
    {
        playStatus = 0;
        StartCoroutine(SetGameCompleteUICo());
    }
    IEnumerator SetGameCompleteUICo()
    {
        yield return new WaitForSeconds(2f);
        uiManager.ShowCompleteTitle();
        yield return new WaitForSeconds(2f);
        uiManager.ShowCompleteDesc();
        yield return new WaitForSeconds(2f);
        uiManager.ShowCompleteTime(second);
        uiManager.ShowNavigatorButton();
    }

    // Game Status Management
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
