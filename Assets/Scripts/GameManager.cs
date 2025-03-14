using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] internal PlayerController player;
    public UIManager uiManager;
    [SerializeField] List<string> stageNameList;
    private readonly float stageIntroDuration = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        InspectComponentsReady();
    }

    void InspectComponentsReady()
    {
        if(player == null)
        {
            Debug.LogError("PlayerController(Player) is Missing");
        }
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

    public void ShowIntroOf(int index)
    {
        ShowStageIntro(index, stageNameList[index]);
    }
    public void ShowStageIntro(int index, string title)
    {
        uiManager.ShowMemoTextAsIntro(index, title);
        //Invoke(nameof(uiManager.ResetMemoText, ));
    }
}
