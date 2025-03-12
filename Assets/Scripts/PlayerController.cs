using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Control
    private float inputX, inputY;
    private bool isCollect, isInAction;

    [Header("Player Specifications")]
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] List<GameObject> objectNear;
    [SerializeField] GameObject[] objectOwned = new GameObject[4]; //Item owning limits are 4
    [SerializeField] int objectOwnedLen = 4;
    //private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameManager.instance.player = this;
    }

    internal void DoPlayerAction()
    {
        GetInput();

        Move();
        Collect();
    }

    internal void GetInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        isCollect = Input.GetButton("Collect");
        isInAction = Input.GetButton("Action");
    }

    void Move()
    {
        transform.Translate(
            Time.deltaTime * moveSpeed * inputX,
            Time.deltaTime * moveSpeed * inputY,
            0f
        );
    }

    void Collect()
    {
        if(isCollect && (objectNear.Count >= 1)) // If player pressed isCollect and there are more than 1 object
        {
            if(objectNear[0] == null) // If object is null
            {
                Debug.LogError("Index of list is not having objects");
            }
            else
            {
                if(objectOwnedLen < gameObject.transform.childCount + 0)
                // If current collectibles count is not bigger limits of owning collectibles
                {
                    Debug.LogWarning("Player have reached the limits of owning objects");
                }
                else{
                    objectNear[0].GetComponent<Collectibles>().Interaction(gameObject);
                    objectNear.RemoveAt(0);
                }
            }

            // Request UI manager to update object
            GameManager.instance.UpdateItemList();
        }
    }

    // Item collection trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(true)
        {
            if(collision.CompareTag("Collectible"))
            {
                objectNear.Add(collision.gameObject);
                Debug.Log("Object near to player: " + objectNear.Count + ", Object touched: " + collision.GetComponent<Collectibles>().objectName);
                //collision.GetComponent<Collectibles>().Interaction();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectible"))
        {
            objectNear.Remove(collision.gameObject);
            Debug.Log("Object near to player: " + objectNear.Count);
        }
    }
}
