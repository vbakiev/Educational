using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;

    // Start is called before the first frame update
    void Start()
    {

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;


        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        pivot.transform.position = target.transform.position;

        //Mouse x position, follow the mouse
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        //Mouse y position
        float verital = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-verital, 0, 0);

        // Camera flick limit
        if (pivot.rotation.eulerAngles.x > 20f && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(20, pivot.eulerAngles.y, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 340f)
        {
            pivot.rotation = Quaternion.Euler(340f, pivot.eulerAngles.y, 0);
        }

        //Target move camera
        float yAngle = pivot.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);
       
        transform.position = target.position - (rotation * offset);


        //camera position to follow the player

 
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.1f, transform.position.z);
        }

        transform.LookAt(target);

    }
}
