using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward : MonoBehaviour
{
    private playerController player;
    
    [HideInInspector]
    public bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D door)
    {
        if(door.tag == "Player")
        {
            if(player.followingKey != null)
            {
                player.followingKey.followTarget = transform;
                
                if (playerController.keyCount >= 3)
                {
                    hasWon = true;
                    Debug.Log("Won");
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                hasWon = false;
            }
        }
    }
}
