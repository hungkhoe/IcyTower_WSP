using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Transform clockWise;

    public float breaksDownSpeed;

    private float timePerRotation = 5;
    private int totalRevolution = 5;

    private void Start()
    {
        StartCoroutine(Rotate(totalRevolution));
    }
    IEnumerator Rotate(int _totalRevolition)
    {
        Quaternion startRot = clockWise.rotation;

        for(int i = 0; i < _totalRevolition; i++)
        {
            float t = 0.0f;

            while (t < timePerRotation)
            {
                t += Time.deltaTime;
                clockWise.rotation = startRot * Quaternion.AngleAxis(t / timePerRotation * 360f, Vector3.back);
                yield return null;
            }

            print("break");
            clockWise.rotation = startRot;
        }

        // TODO : add condition game over
        while (true)
        {
            clockWise.transform.Rotate(0, 0, breaksDownSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
