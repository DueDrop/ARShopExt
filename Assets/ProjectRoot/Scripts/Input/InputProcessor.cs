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
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
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
        if (Physics.Raycast(ray, out hit, rayMaxDistance, ~LayerMask.GetMask("ARBackground2")))
        {

            TagInfoProcessor trackedTag = hit.transform.gameObject.GetComponent<TagInfoProcessor>();
            if (trackedTag != null)
            {
                trackedTag.ProcessTagPress();
            }
            
        }
    }

}
