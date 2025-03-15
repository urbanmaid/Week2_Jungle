using UnityEngine;

public class Finisher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Game Finished!");
            GameManager.instance.SetGameComplete();
        }
    }
}
