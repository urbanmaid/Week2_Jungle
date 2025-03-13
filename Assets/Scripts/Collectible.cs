using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Key,
        Card,
        Misc,
        Unknown
    }

    public string collectibleName = "Prop";
    [SerializeField] internal int keyCode;
    public CollectibleType type;
    //[SerializeField] internal Sprite sprite;
    //[SerializeField] bool isOwned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    internal void BePickUp(GameObject parentObject)
    {
        //Debug.Log("Player got an " + objectName);

        gameObject.transform.parent = parentObject.transform;
        gameObject.transform.localPosition = Vector3.zero;

        // Set the sprite off
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer)
        {
            Color savedColor = spriteRenderer.color;
            savedColor.a = 0f;
            spriteRenderer.color = savedColor;
        }

        // Deactivate gameObject
        gameObject.SetActive(false);
    }

    internal void BeDrop()
    {
        // Set the sprite on
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer)
        {
            Color savedColor = spriteRenderer.color;
            savedColor.a = 1f;
            spriteRenderer.color = savedColor;
        }

        gameObject.SetActive(true);
    }
}
