using UnityEngine;
using UnityEngine.Events;

public class DropZone : MonoBehaviour
{
    public UnityEvent eventWhenValidInput;
    [SerializeField] bool usingKeyCode;
    [SerializeField] int keyCode;
    [SerializeField] string target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        Collectible collectible = collision.GetComponent<Collectible>();
        if(collision.CompareTag("Collectible") && collectible)
        {
            if(usingKeyCode)
            {
                if(keyCode == collectible.keyCode)
                {
                    DoValidInputAction();
                }
            }
            else
            {
                if(target == collectible.collectibleName)
                {
                    DoValidInputAction();
                }
            }
        }
    }

    void DoValidInputAction()
    {
        Debug.Log("Drop Zone is able to activate Event with valid input");
        eventWhenValidInput.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
