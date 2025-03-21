using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Key,
        Card,
        Memo,
        Misc,
        Unknown
    }

    public string collectibleName = "Prop";
    [SerializeField] internal Sprite iconSprite;
    [SerializeField] internal int keyCode;
    public CollectibleType type;
    public UnityEvent eventWhenInteraction;

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

    internal void BeInteraction()
    {
        Debug.Log("Interaction with Collectible has been called");
        eventWhenInteraction.Invoke();
    }
}
