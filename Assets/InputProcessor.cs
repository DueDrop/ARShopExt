using UnityEngine;
using System.Collections;

public class InputProcessor : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                checkTouch(Input.mousePosition);
            }
        }
    }

    private void checkTouch(Vector3 pos)
    {
        float rayMaxDistance = 100.0f;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayMaxDistance))
        {
            Debug.Log("Touch/Clicked: " + hit.collider.gameObject.name);
        }
    }
}
