using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap map;

    // Used to retrieve min & max bounds of the map
    private Vector3 min;
    private Vector3 max;

    // Used to retrieve the camera size
    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;

        // Define the camera size
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        // Retrieve map bounds and substract half of the camera size
        min = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        max = map.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);

        // Set the bounds of the map to the player
        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, min.x, max.x), Mathf.Clamp(target.position.y, min.y, max.y), transform.position.z);
    }
}
