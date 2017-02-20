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
        transform.LookAt(transform.position + mCamera.transform.rotation * Vector3.forward,
            mCamera.transform.rotation * Vector3.up);

    }

}