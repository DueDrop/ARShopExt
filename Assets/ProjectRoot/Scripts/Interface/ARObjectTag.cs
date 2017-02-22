using UnityEngine;

public class ARObjectTag : MonoBehaviour
{
    public static Camera mCamera;

    void Awake()
    {
        if (mCamera == null) mCamera = Camera.main;
    }

    void Update()
    {
        // behaving as a billboard to main camera.
        Vector3 target = transform.position + mCamera.transform.rotation * Vector3.forward;
        Quaternion rotation = Quaternion.LookRotation(target, mCamera.transform.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);        

    }

}