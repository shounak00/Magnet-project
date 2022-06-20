using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public float smooth;
    public Vector3 offset;


    private void LateUpdate()
    {
        Vector3 corePosition = Player.position + offset;
        Vector3  newPosition = Vector3.Lerp(transform.position, corePosition, smooth);
        transform.position = newPosition;
        transform.LookAt(Player);
    }
}
