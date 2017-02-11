using UnityEngine;

public class ARObjectTag : MonoBehaviour
{
    public Camera m_Camera;
   
    void Update()
    {
        // behaving as a billboard to main camera.
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);

    }

 

}