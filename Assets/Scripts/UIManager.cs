using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject itemArray;
    [SerializeField] Sprite itemArrayWhenEmpty;
    [SerializeField] Color colorWhenEmpty = Color.white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    internal void UpdateItemList()
    {
        int collectibleCount = GameManager.instance.player.transform.childCount;
        int collectibleArrayCount = itemArray.transform.childCount;
        Debug.Log("collectibleCount: " + collectibleCount + ", collectibleArrayCount: " + collectibleArrayCount);

        // Apply sprite icon
        for(int i = 1; i < collectibleCount; i++)
        {
            Image targetIconApplied = itemArray.transform.GetChild(i).GetComponent<Image>(); // Define where the sprite applied
            Transform targetCollectible = GameManager.instance.player.transform.GetChild(i); // Define where the sprite refered

            if(collectibleCount <= collectibleArrayCount)
            {
                /*
                if(targetCollectible.GetComponent<Collectibles>().sprite != null)  // If collectible has its thumb of item
                {
                    targetIconApplied.sprite = targetCollectible.GetComponent<Collectibles>().sprite;
                }
                */
                //else
                if(targetCollectible.GetComponent<SpriteRenderer>())
                {
                    // Changes image of icon
                    targetIconApplied.sprite = targetCollectible.GetComponent<SpriteRenderer>().sprite;
                    targetIconApplied.color = targetCollectible.GetComponent<SpriteRenderer>().color;

                    // Changes color of icon
                    Color savedColor = targetIconApplied.color;
                    savedColor.a = 1f;
                    targetIconApplied.color = savedColor;
                }
                else
                {
                    Debug.LogError("This collectible has no Sprite, add Sprite on this object");
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
}
