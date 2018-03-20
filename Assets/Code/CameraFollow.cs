using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance;
    public float angle;
    public float radius;
    private float maxDistance;

    Vector3 offset;

    private void Start()
    {
        maxDistance = distance;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        //Ray ray = new Ray(target.position, offset);
        //Debug.DrawLine(ray.origin,offset);

        //if(Physics.Raycast(ray, distance))
        //{
        //    distance -= 0.1f;
        //}
        //else if (distance < maxDistance && !Physics.Raycast(ray, distance))
        //{
        //    distance += 0.1f;
        //}

        RaycastHit hit;
        
        float distanceToObstacle = 0;

        //Calculate the offset position of the Camera relative to the player's position.
        offset = target.position - transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, offset, 10 * Time.deltaTime);

        // Cast a sphere wrapping character controller maxDistance backwards
        // to see if it is about to hit anything.
        if (Physics.SphereCast(target.position, radius, offset, out hit, maxDistance))
        {
            distanceToObstacle = hit.distance;

            if(distanceToObstacle > 0.5f)
                distance = distanceToObstacle;
        }
        
        Debug.DrawLine(target.position, offset - Vector3.down);
        Debug.DrawLine(target.position, offset - Vector3.up);

        //Rotate the Camera according to the player, and the set angle.
        //90 - angle, so that we get the desired angle to the inspector. 
        //(when we insert 90, the camera is on the ground, and when 0 the camera is on top of the player.)
        Quaternion localRotation = Quaternion.Euler(90.0f - angle, target.rotation.eulerAngles.y, 0.0f);
        transform.rotation = localRotation;
    }
    
}