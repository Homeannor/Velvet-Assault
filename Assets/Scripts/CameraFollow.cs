using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public float moveLerpSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = followTransform.position;
        targetPosition.z = transform.position.z;
        
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, moveLerpSpeed * Time.deltaTime);
        // Debug.Log("Player Position: " + followTransform.position);
        // Debug.Log("Current Camera Pos: " + gameObject.transform.position);
    }
}