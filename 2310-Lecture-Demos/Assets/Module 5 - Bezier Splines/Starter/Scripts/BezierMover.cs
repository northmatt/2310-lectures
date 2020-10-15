//(c) Samantha Stahlke 2020
//Created for INFR 2310.
using System.Collections.Generic;
using UnityEngine;

public class BezierDemo
{
    //We'll use the non-recursive version of Bezier for this demo.
    public static Vector3 Bezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //TODO: Implement Bezier interpolation.
        float s = 1 / t - 1;
        return t * t * t * (s * (s * (p0 * s + 3 * p1) + 3 * p2) + p3);
        //return Vector3.Lerp(Bezier2(p0, p1, p2, t), Bezier2(p1, p2, p3, t), t);
    }

    public static Vector3 Bezier2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        //TODO: Implement Bezier.
        return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
    }
}

public class BezierMover : MonoBehaviour
{
    //List of waypoints.
    public List<Transform> points;
    //Time taken to complete a segment.
    //Just as with Catmull, we won't learn how to control speed directly until next week.
    public float segmentTime;

    private bool goodSegment = false;
    private int currentIndex = 0;
    private float t = 0f;
    private int p0, p1, p2, p3;

    void Start()
    {
        goodSegment = points.Count >= 6 && points.Count % 3 == 0;

        if (segmentTime <= 0)
            segmentTime = 1.0f;

        //TODO: Initialize our position.
        if (points.Count > 0)
            transform.position = points[0].transform.position;

        StartSegment(0);
    }

    void Update()
    {
        //It only makes sense to do a Bezier loop if we have at least 6 points.
        //(2 waypoints on path + 2 control points for each of 2 segments).
        if (goodSegment) {
            //TODO: What's our update look like?
            t += Time.deltaTime / segmentTime;

            if (t >= 1f)
                StartSegment(currentIndex + 3);

            transform.position = BezierDemo.Bezier3(points[p0].position,
                                                    points[p1].position,
                                                    points[p2].position,
                                                    points[p3].position, t);
        }
    }

    void StartSegment(int startIndex) {
        //TODO: Complete this function.
        currentIndex = (startIndex >= points.Count) ? 0 : startIndex;
        p0 = currentIndex;
        p1 = p0 + 1;
        if (p1 >= points.Count)
            p1 = 0;

        p2 = p1 + 1;
        if (p2 >= points.Count)
            p2 = 0;

        p3 = p2 + 1;
        if (p3 >= points.Count)
            p3 = 0;

        t -= 1f;
    }
}
