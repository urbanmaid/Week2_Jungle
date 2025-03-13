using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] internal PlayerController player;
    public UIManager uiManager;

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

    internal void UpdateItemList()
    {
        uiManager.UpdateItemList();
    }

    public void TeleportTo(GameObject target)
    {
        player.transform.position = target.transform.position;
    }
}
