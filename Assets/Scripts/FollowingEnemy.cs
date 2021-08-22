using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    public float Speed;
    public float pSpeed;
    private Transform target;
    public float distance;
    public float stoppingDistance;

    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpot;
    private int randomSpot;
 
    public LineRenderer lineofSight;
    public Gradient redColor;
    public Gradient greenColor;
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot[randomSpot].position, pSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpot[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpot.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawRay(transform.position, hitInfo.point, Color.red);
            lineofSight.SetPosition(1, hitInfo.point);
            lineofSight.colorGradient = redColor;

            if (hitInfo.collider.CompareTag("Player"))
            {
                Destroy(hitInfo.collider.gameObject);
                /*if(Vector2.Distance(transform.position, target.position) >= stoppingDistance)
                 {
                     transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
                 }*/
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.position + transform.right * distance, Color.green);
            lineofSight.SetPosition(1, transform.position + transform.right * distance);
            lineofSight.colorGradient = greenColor;
        }

        lineofSight.SetPosition(0, transform.position);
    }
}
