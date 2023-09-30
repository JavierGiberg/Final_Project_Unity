using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform target;
    private Vector3 offset;


	// Use this for initialization
	void Awake () {
        offset = transform.position - target.position;
	}

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }

}
