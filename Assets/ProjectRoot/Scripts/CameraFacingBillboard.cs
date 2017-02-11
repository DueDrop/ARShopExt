using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;
    private Collider2D collider;

    void Start() {

        collider = GetComponent<Collider2D>();

    }
    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);

        ProcessInput();
    }

    public void OnClick() {

        Debug.Log("Tag clicked!");

    }

    void ProcessInput()
    {
        if (Input.touchCount > 0) 
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (collider.OverlapPoint(wp))
            {              
                Debug.Log("Tag clicked!");
            }
        }

        if (Input.GetMouseButtonDown(0)){

            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (collider.OverlapPoint(wp))
            {
                Debug.Log("Tag clicked!");
            }

        }           

    }

}