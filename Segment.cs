using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public Arm arm;
    public Transform end;
    public float segmentLength;

    public void Follow(Transform target)
    {
        if(end.position != target.position)
        {
            /*
             * To find the position the segment needs to move towards we can subtract the length of the segment over the segment's forward axis from the targets position.
             */
            transform.LookAt(target);
            Vector3 targetPos = target.position - transform.forward * segmentLength;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, arm.segmentSpeed * Time.deltaTime);
        }
    }

    public void Ground(Vector3 basePosition)
    {
        transform.position = basePosition;
    }
}
