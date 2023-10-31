//using UnityEngine;
//using System.Collections;
///// MouseLook rotates the transform based on the mouse delta. 
///// Minimum and Maximum values can be used to constrain the possible rotation 
///// To make an FPS style character: 
///// - Create a capsule. 
///// - Add the MouseLook script to the capsule. 
/////   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it) 
///// - Add FPSInputController script to the capsule 
/////   -> A CharacterMotor and a CharacterController component will be automatically added. 
///// - Create a camera. Make the camera a child of the capsule. Reset it's transform. 
///// - Add a MouseLook script to the camera. 
/////   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.) 
//[AddComponentMenu("Camera-Control/Mouse Look")]
//public class MouseLook : MonoBehaviour
//{
//    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
//    public RotationAxes axes = RotationAxes.MouseXAndY;
//    public float sensitivityX = 15F;
//    public float sensitivityY = 15F;
//    public float minimumX = -360F;
//    public float maximumX = 360F;
//    public float minimumY = -60F;
//    public float maximumY = 60F;
//    public float rotationY = 0F;
//    void Update()
//    {
//        if (axes == RotationAxes.MouseXAndY)
//        {
//            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
//            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
//        }
//        else if (axes == RotationAxes.MouseX)
//        {
//            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
//        }
//        else
//        {
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
//            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
//        }
//    }
//    void Start()
//    {
//        Cursor.visible = false;
//        // Make the rigid body not change rotation 
//        if (GetComponent<Rigidbody>())
//            GetComponent<Rigidbody>().freezeRotation = true;
//    }
//} 



//using UnityEngine;

//[AddComponentMenu("Camera-Control/Mouse Look")]
//public class MouseLook : MonoBehaviour
//{
//    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
//    public RotationAxes axes = RotationAxes.MouseXAndY;
//    public float sensitivityX = 15F;
//    public float sensitivityY = 15F;
//    public float minimumX = -360F;
//    public float maximumX = 360F;
//    public float minimumY = -60F;
//    public float maximumY = 60F;
//    public float smoothSpeed = 5f;
//    private float rotationY = 0F;

//    private Quaternion originalRotation;

//    void Start()
//    {
//        Cursor.visible = false;
//        originalRotation = transform.localRotation;

//        // Make the rigid body not change rotation
//        if (GetComponent<Rigidbody>())
//            GetComponent<Rigidbody>().freezeRotation = true;
//    }

//    void Update()
//    {
//        if (axes == RotationAxes.MouseXAndY)
//        {
//            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            Quaternion targetRotation = Quaternion.Euler(-rotationY, rotationX, 0);
//            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothSpeed * Time.deltaTime);
//        }
//        else if (axes == RotationAxes.MouseX)
//        {
//            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
//        }
//        else
//        {
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            Quaternion targetRotation = Quaternion.Euler(-rotationY, transform.localEulerAngles.y, 0);
//            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothSpeed * Time.deltaTime);
//        }
//    }
//}


using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -35F;
    public float maximumY = 35F;
    public float smoothTime = 0.1f;
    private Vector2 currentRotation;
    private Vector2 targetRotation;
    private Vector2 rotationVelocity;

    void Start()
    {
        Cursor.visible = false;
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            targetRotation.x += Input.GetAxis("Mouse X") * sensitivityX;
            targetRotation.y += Input.GetAxis("Mouse Y") * sensitivityY;
            targetRotation.y = Mathf.Clamp(targetRotation.y, minimumY, maximumY);
            // Smoothly interpolate the current rotation to the target rotation
            currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, smoothTime);
            transform.localRotation = Quaternion.Euler(-currentRotation.y, currentRotation.x, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            targetRotation.y -= Input.GetAxis("Mouse Y") * sensitivityY;
            targetRotation.y = Mathf.Clamp(targetRotation.y, minimumY, maximumY);
            transform.localRotation = Quaternion.Euler(-targetRotation.y, transform.localEulerAngles.y, 0);
        }
    }
}