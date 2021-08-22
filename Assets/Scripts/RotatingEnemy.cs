using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;


    public LineRenderer lineofSight;
    public Gradient redColor;
    public Gradient greenColor;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            lineofSight.SetPosition(1, hitInfo.point);
            lineofSight.colorGradient = redColor;

            if (hitInfo.collider.CompareTag("Player"))
            {
                Destroy(hitInfo.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            lineofSight.SetPosition(1, transform.position + transform.right * distance);
            lineofSight.colorGradient = greenColor;
        }

        lineofSight.SetPosition(0, transform.position);
    }
}
