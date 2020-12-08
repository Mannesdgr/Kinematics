using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public int segmentsAmount;
    public float segmentSpeed;
    public GameObject segmentPrefab;
    private List<Segment> segments = new List<Segment>();
    public Transform target;

    private void Start()
    {
        CreateArm();
    }

    private void Update()
    {
        UpdatePositions();
    }

    private void CreateArm()
    {
        for (int i = 0; i < segmentsAmount; i++)
        {
            Segment segment = Instantiate(segmentPrefab, transform.position, transform.rotation).GetComponent<Segment>();
            segment.transform.SetParent(transform);
            segment.arm = this;
            segments.Add(segment);
        }
    }

    private void UpdatePositions()
    {
        /*
         * Since we're using inverse kinematics we start at the last segment in the arm and work our way back.
         */
        segments[segmentsAmount - 1].Follow(target);
        for (int i = segmentsAmount - 1; i >= 0; i--)
        {
            /*
             * We can see whether a segment still has another segment after it by checking if we can add one to it's index and still be within the bounds of the array.
             */
            if (i + 1 < segmentsAmount)
                segments[i].Follow(segments[i + 1].transform);
        }

        /*
         * After moving the segments we need to move them back to their base position.
         * We start by setting the position of the first segment in the arm to the position of the arm.
         * You could also use an alternative base position by entering another Vector3.
         */
        segments[0].Ground(transform.position);
        for (int i = 1; i < segmentsAmount; i++)
        {
            /*
             * We can find the base position of each other segment by using the end position, located at the far end of the segment, of the segment that came before it.
             */
            segments[i].Ground(segments[i-1].end.position);
        }
    }
}
