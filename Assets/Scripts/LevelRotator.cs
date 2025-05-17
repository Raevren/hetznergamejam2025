using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelRotator : MonoBehaviour
{
    [SerializeField] private PlayerSpeed speedScript;
    
    public void Rotate(float newAngle, float distance)
    {
        StopAllCoroutines();
        StartCoroutine(RotateOverTime(newAngle, distance));
    }

    private IEnumerator RotateOverTime(float newAngle, float distance)
    {
        var startRotation = transform.rotation;
        var endRotation = Quaternion.Euler(0, newAngle, 0);
        var distanceDone = 0f;
        var wantedDistance = Mathf.PI * distance / 2f;

        while (distanceDone < wantedDistance)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, distanceDone / wantedDistance);
            distanceDone += speedScript.Speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = endRotation;
    }
}
