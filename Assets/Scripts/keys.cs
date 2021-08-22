using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public bool isFollowing;
    public bool Touch;
    public float followSpeed;
    public Transform followTarget;
   
    // Start is called before the first frame update
    void Start()
    {
        isFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D glide)
    {  
        if (glide.tag == "Player" && Touch == false)
        {
            playerController.keyCount += 1;
            Debug.Log("Count" + playerController.keyCount);
            if (!isFollowing)
            {
                playerController player = FindObjectOfType<playerController>();
                Touch = true;
                followTarget = player.keyFollowPoint;
                isFollowing = true; 
                player.followingKey = this;
            }
        }
    }
}
