using UnityEngine;
using System.Collections;

public class TagDistanceChecker : MonoBehaviour {

    public float TagCollapseDistance = 1.0f;
    private Animator AnimatorCtrl;

    void Start () {
        AnimatorCtrl = GetComponent<Animator>();     
    }
	
	// Update is called once per frame
	void Update () {

        float distanceToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distanceToCamera > TagCollapseDistance - 0.25f)
        {
            AnimatorCtrl.SetBool("Near", false);

        }
        else if(distanceToCamera < TagCollapseDistance + 0.25f) {
            AnimatorCtrl.SetBool("Near", true);
        }
    }
}
