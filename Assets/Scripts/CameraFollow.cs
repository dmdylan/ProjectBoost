using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float zOffset;
    [SerializeField] float yOffset;
    GameObject target;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        target = GameObject.Find("Rocket Ship");
    }

    void Update()
    {
        Vector3 rocketInfo = target.transform.position;
        mainCamera.transform.position = new Vector3(rocketInfo.x, 23, rocketInfo.z - zOffset);
    }
}
