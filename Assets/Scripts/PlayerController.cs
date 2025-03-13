using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Control
    private float inputX, inputY;
    private bool isCollect, isInAction, isDrop;

    [Header("Player Specifications")]
    [SerializeField] float moveSpeed = 4f;
    //private readonly float actionDelayDuration = 0.4f;

    [SerializeField] List<GameObject> objectNear;
    private int objectOwnedOffset;
    [SerializeField] int objectOwnedLen = 4;

    //private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameManager.instance.player = this;
        objectOwnedOffset = gameObject.transform.childCount;
    }

    internal void DoPlayerAction()
    {
        // Get input
        GetInput();

        // Do Action
        Move();
        Collect();
        Drop();
    }

    internal void GetInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        isCollect = Input.GetButtonDown("Collect");
        isInAction = Input.GetButtonDown("Action");
        isDrop = Input.GetButtonDown("Drop");
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
                    objectNear[0].GetComponent<Collectible>().BePickUp(gameObject);
                    //objectNear.RemoveAt(0);
                }
            }

            // Request UI manager to update object
            GameManager.instance.UpdateItemList();
        }
    }

    void Drop()
    {
        if(isDrop && (objectOwnedOffset < transform.childCount))
        {
            // Set collectible which will be dropped
            Transform collectibleTarget = transform.GetChild(transform.childCount - 1);
            Debug.Log(collectibleTarget + "Will be dropped");

            collectibleTarget.GetComponent<Collectible>().BeDrop();
            transform.GetChild(transform.childCount - 1).SetParent(null);

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
                //Debug.Log("Object near to player: " + objectNear.Count + ", Object touched: " + collision.GetComponent<Collectibles>().objectName);
                //collision.GetComponent<Collectibles>().Interaction();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectible"))
        {
            objectNear.Remove(collision.gameObject);
            //Debug.Log("Object near to player: " + objectNear.Count);
        }
    }
}
