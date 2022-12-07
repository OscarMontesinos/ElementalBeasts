using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Camera cam;
    public float speedZoom;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize += speedZoom; 
            if (cam.orthographicSize > 25)
            {
                cam.orthographicSize = 25;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize -= speedZoom; 
            if (cam.orthographicSize < 1)
            {
                cam.orthographicSize = 2;
            }

        }
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + speed * Time.deltaTime, cam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - speed * Time.deltaTime, cam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cam.transform.position = new Vector3(cam.transform.position.x + speed * Time.deltaTime, cam.transform.position.y, cam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.position = new Vector3(cam.transform.position.x - speed * Time.deltaTime, cam.transform.position.y, cam.transform.position.z);
        }
    }
}
