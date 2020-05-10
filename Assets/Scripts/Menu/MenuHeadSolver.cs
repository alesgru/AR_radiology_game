using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHeadSolver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraRot = Camera.main.transform.eulerAngles;
        //if (cameraRot.y - this.transform.eulerAngles.z <= 0)
        //{
        //    //return;
        //    //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -this.transform.eulerAngles.z);
        //}

        //Debug.Log(string.Format("Before cameraRot.y: {0}, Rotation.z: {1}", cameraRot.y, transform.eulerAngles.z));
        //this.transform.Rotate(0, 0, cameraRot.y - this.transform.eulerAngles.z);
        ////transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, cameraRot.y - this.transform.eulerAngles.z);
        //Debug.Log(string.Format("After cameraRot.y: {0}, Rotation.z: {1}", cameraRot.y, transform.eulerAngles.z));
        
        transform.LookAt(transform.position + Camera.main.transform.rotation * new Vector3(0,0,-1),Camera.main.transform.rotation * Vector3.up);
        transform.Rotate(0, 180, 0);
    }
}
