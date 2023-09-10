using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placing : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] float scrollStrength = 0.5f;
    [SerializeField] float distance = 8;
    [SerializeField] float minDistance = 4;
    [SerializeField] float maxDistance = 12;
    [SerializeField] float gridSize = 1;

    [SerializeField] Transform cameraTransform;

    [Header("Materials")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material transparentMaterial;

    public bool canPlace = false;

    GameObject trapObject;

    void Update()
    {
        if (GameManager.currentState != GameManager.State.PLACING)
            return;
        if (!trapObject && Input.GetMouseButtonDown(1))
        {
            Vector3 position = transform.position + cameraTransform.forward * distance;
            trapObject = Instantiate(objectPrefab, position, Quaternion.identity);
            trapObject.GetComponent<MeshRenderer>().material = transparentMaterial;
            

        }

        if (trapObject)
        {
            if(Input.GetMouseButton(0))
            {
                trapObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                trapObject = null;
                return;
            }

            Vector3 position = transform.position + cameraTransform.forward * distance;
            position.x = position.x - position.x % gridSize;
            position.y = position.y - position.y % gridSize;
            position.z = position.z - position.z % gridSize;
            trapObject.transform.position = position;
        }

        //Debug.Log(Mathf.Clamp(distance + Input.mouseScrollDelta.x * scrollStrength, minDistance, maxDistance) + " " + Input.mouseScrollDelta.x);
        distance = Mathf.Clamp(distance + Input.mouseScrollDelta.y * scrollStrength, minDistance, maxDistance);
    }
}
