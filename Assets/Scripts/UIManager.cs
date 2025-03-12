using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject itemArray;
    [SerializeField] Sprite itemArrayWhenEmpty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateItemList()
    {
        int collectibleCount = GameManager.instance.player.transform.childCount;
        int collectibleArrayCount = itemArray.transform.childCount;
        //Debug.Log("collectibleCount: " + collectibleCount + ", collectibleArrayCount: " + collectibleArrayCount);
        for(int i = 1; i < collectibleCount; i++)
        {
            if(collectibleCount <= collectibleArrayCount)
            {
                itemArray.transform.GetChild(i).GetComponent<Image>().sprite
                = GameManager.instance.player.transform.GetChild(i).GetComponent<Collectibles>().icon;
            }
            else
            {
                Debug.LogWarning("There is no space for showing collectibles!");
            }
        }
    }
}
