using UnityEngine;
using UnityEngine.Events;

public class InteractiveZone : MonoBehaviour
{
    public UnityEvent eventWhenValidInput; // Interaction only valid object is placed
    public UnityEvent eventWhenInteraction; // Interaction whatever the case
    
    [Header("Interaction Condition")]
    [SerializeField] bool usingRequiredValidInput = true;
    private bool isValid = false;
    [SerializeField] bool usingKeyCode;
    [SerializeField] int keyCode;
    [SerializeField] string target;
    //private bool isPlayerIn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(usingRequiredValidInput) // If this interactiveZone required valid input
        {
            Collectible collectible = collision.GetComponent<Collectible>();
            if(collision.CompareTag("Collectible") && collectible)
            {
            // SetValid should be called the code or name should be same
                if(usingKeyCode)
                {
                    if(keyCode == collectible.keyCode)
                    {
                        SetValid();
                    }
                }
                else
                {
                    if(target == collectible.collectibleName)
                    {
                        SetValid();
                    }
                }
            }
        }

        /*
        if(collision.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
        */
    }
    
    void SetValid()
    {
        isValid = true;
        DoInteractionWhenValid();
    }

    /*
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // If this interactiveZone is operated only with player interaction
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if(pc && pc.isInteraction)
            {

            }
        }
    }
    */

    internal void DoInteractionCheckout()
    {
        Debug.Log("Player has interaction with this object: " + gameObject);

        // if usingRequiredValidInput is true, isAbleToDoValidInput should be true too
        // for activating interaction
        if(usingRequiredValidInput)
        {
            if(isValid){
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
        Debug.Log("Drop Zone is able to activate Event with valid input");
        eventWhenValidInput.Invoke();
    }

    void DoInteraction()
    {
        eventWhenInteraction.Invoke();
    }
}
