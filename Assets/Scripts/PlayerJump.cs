using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpDuration = 2f;
    private bool isJumping = false;
    private float jumpStartTime;
    private Vector3 startPos;

    void jump()
    {
        StartJump();
    }

    private void Update()
    {
        if(!isJumping) return;
        HandleJump();
    }

    void StartJump()
    {
        isJumping = true;
        jumpStartTime = Time.time;
        startPos = transform.position;
    }

    void HandleJump()
    {
        float elapsed = Time.time - jumpStartTime;
        float halfDuration = jumpDuration / 2f;

        if (elapsed < jumpDuration)
        {
            // Parabolischer Jump: y = -4h/d² * (t - d/2)² + h
            float t = elapsed;
            float d = jumpDuration;
            float h = jumpHeight;
            float yOffset = -4 * h / (d * d) * (t - d / 2f) * (t - d / 2f) + h;

            transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
        }
        else
        {
            // Zurück zum Boden
            transform.position = startPos;
            isJumping = false;
        }
    }
}