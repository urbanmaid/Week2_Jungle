using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

public class InteractiveZone : MonoBehaviour
{
    private static float interactionDelay = 0.32f;
    public static bool isInteractAble = true;
    public UnityEvent eventWhenValidInput; // Interaction only valid object is placed
    public UnityEvent eventWhenInteraction; // Interaction whatever the case
    public UnityEvent eventWhenPlayerEnter; // Interaction when player come to this area
    public UnityEvent eventWhenPlayerExit; // Interaction when player come to this area

    [Header("Interaction Condition")]
    [SerializeField] bool usingRequiredValidInput = true;
    private bool isValid = false;
    [SerializeField] bool usingKeyCode;
    [SerializeField] int keyCode;
    [SerializeField] string target;

    [Header("Localization")]
    [SerializeField] private LocalizedString localizedString;

    #region Verification
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (usingRequiredValidInput) // If this interactiveZone required valid input
        {
            Collectible collectible = collision.GetComponent<Collectible>();
            if (collision.CompareTag("Collectible") && collectible)
            {
                // SetValid should be called the code or name should be same
                if (usingKeyCode)
                {
                    if (keyCode == collectible.keyCode)
                    {
                        SetValid();
                        if (collectible.type == Collectible.CollectibleType.Key)
                        {
                            Destroy(collision.gameObject);
                        }
                    }
                }
                else
                {
                    if (target == collectible.collectibleName)
                    {
                        SetValid();
                        if (collectible.type == Collectible.CollectibleType.Key)
                        {
                            Destroy(collision.gameObject);
                        }
                    }
                }
            }
        }

        if (collision.CompareTag("Player") && (eventWhenPlayerEnter != null))
        {
            eventWhenPlayerEnter.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (eventWhenPlayerExit != null))
        {
            eventWhenPlayerExit.Invoke();
        }
    }

    void SetValid()
    {
        isValid = true;
        DoInteractionWhenValid();
    }
    #endregion
    #region Interaction
    internal void DoInteractionCheckout()
    {
        //Debug.Log("Player has interaction with this object: " + gameObject);

        // if usingRequiredValidInput is true, isAbleToDoValidInput should be true too
        // for activating interaction
        if (usingRequiredValidInput)
        {
            if (isValid)
            {
                //DoInteractionWhenValid();
                DoInteraction();
            }
        }
        else
        {
            DoInteraction();
        }
    }

    void DoInteractionWhenValid()
    {
        //Debug.Log("Drop Zone is able to activate Event with valid input");
        eventWhenValidInput.Invoke();
    }

    void DoInteraction()
    {
        if (isInteractAble)
        {
            eventWhenInteraction.Invoke();
            //StartCoroutine(DelayInteraction());
        }
    }
    #endregion
    #region Localization
    public void TriggerShowMemo(bool asFixed)
    {
        if (localizedString != null)
        {
            if (asFixed)
            {
                GameManager.instance.ShowMemoTextWithLocale(localizedString);
            }
            else
            {
                GameManager.instance.ShowMemoTextAsCaptionWithLocale(localizedString);
            }
        }
    }
    #endregion
}
