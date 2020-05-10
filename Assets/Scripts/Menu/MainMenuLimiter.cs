using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLimiter : MonoBehaviour
{
    public static GameObject[] menuObjects;
   
    // checks if sub menus of a main menu are open and if so will not set the object active
    public void SetMenuActive()
    {
        menuObjects = GameObject.FindGameObjectsWithTag("SubMenu");
        foreach  (GameObject obj in menuObjects)
        {
            if (obj.activeSelf)
            {
                return;
            }
        }
        this.gameObject.SetActive(true);
    }
}
