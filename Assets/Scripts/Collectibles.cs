using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public string objectName = "Prop";
    [SerializeField] internal Sprite icon;
    [SerializeField] bool isOwned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    internal void Interaction(GameObject parentObject)
    {
        Debug.Log("Player got an " + objectName);

        gameObject.transform.parent = parentObject.transform;
        gameObject.transform.localPosition = Vector3.zero;
        //gameObject.SetActive(false);
    }
}
