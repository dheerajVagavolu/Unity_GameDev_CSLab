using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;

    public bool useOffset;

    public float rotateSpeedx;

    public float rotateSpeedy;

    public Transform pivot;

    public float maxView = 20f;

    public float minView = -80f;

    public bool invertY = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!useOffset)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
        pivot.parent = null;
        pivot.transform.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        pivot.transform.position = target.transform.position;

        //Mouse positions
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeedx;
        pivot.Rotate(0, horizontal, 0);


        //Get the y po of the mouse and rotate the pivot

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeedy;


        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);

        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }


        //Limit the up/down camera rotation
        /*if (pivot.rotation.eulerAngles.x > maxView && pivot.rotation.eulerAngles.x < 180)
        {
            pivot.rotation = Quaternion.Euler(maxView, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minView)
        {
            pivot.rotation = Quaternion.Euler(360f + minView, 0, 0);
        }*/


        if (pivot.rotation.eulerAngles.x >= maxView && pivot.rotation.eulerAngles.x <= 180.0f)
        {
            pivot.rotation = Quaternion.Euler(maxView, pivot.eulerAngles.y, 0.0f);
        }

        if (pivot.rotation.eulerAngles.x >= 180.0f && pivot.rotation.eulerAngles.x <= minView)
        {
            pivot.rotation = Quaternion.Euler(minView, pivot.eulerAngles.y, 0.0f);
        }


        /*if (pivot.rotation.eulerAngles.x > maxView && pivot.rotation.eulerAngles.x < 180.0f)
        {
            pivot.rotation = Quaternion.Euler(maxView, pivot.eulerAngles.y, 0.0f);
        }

        if (pivot.rotation.eulerAngles.x > 180.0f && pivot.rotation.eulerAngles.x < 360f + minView)
        {
            pivot.rotation = Quaternion.Euler(360.0f + minView, pivot.eulerAngles.y, 0.0f);
        }*/



        //Camera Movement based on rotation
        float desiredYangle = pivot.eulerAngles.y;
        float desiredXangle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXangle, desiredYangle, 0);
        transform.position = target.position - (rotation * offset);


        //transform.position = target.position - offset;

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y-0.5f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
