using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // lokasi playernya supaya bisa ngikutin
    public Transform playerTarget;

    // kecepatan kamera
    public float cameraSpeed;

    // offset kameranya
    public Vector3 offset;

    private void LateUpdate()
    {
        // camera follow diletakkan disini biar smooth aja
        // ngikutin ke player dengan lerp biar kecepatannya tu linear alias biar halus
        // lerp itu dari cepet ke pelan
        transform.position = Vector3.Lerp(transform.position, playerTarget.position + offset, cameraSpeed * Time.deltaTime);
    }
}
