using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controlls {

   public  static float mobileSenstitivity = 2f; //this should increase sensitivity, so I reach max acceleration with less mobile tilt

   public static float GetHorizontal()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // return Mathf.Clamp(Input.acceleration.x * mobileSenstitivity, -1, 1);      
            return Input.acceleration.x;
        }
        else
        {

            return Input.GetAxis("Horizontal");
           
        }
    }

    public static float GetVertical()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //return Mathf.Clamp(Input.acceleration.y * mobileSenstitivity, -1, 1);
            return Input.acceleration.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
