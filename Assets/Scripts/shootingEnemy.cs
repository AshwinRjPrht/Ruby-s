using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    public float Speed;
    public float pSpeed;
    private Transform target;
    public float distance;
    public float stoppingDistance;

    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public LineRenderer lineofSight;
    public Gradient redColor;
    public Gradient greenColor;
    private void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, pSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
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
                if (Vector2.Distance(transform.position, target.position) >= stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
                }
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
