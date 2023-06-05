using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movepoint : MonoBehaviour
{
    public bool isTesting = false;
    public float x_, y_, z_;
    public static float x, y, z;
    void Update()
    {
        if (isTesting)
        {
            x = x_;
            y = y_;
            z = z_;
        }
      //  transform.position = new Vector3(x, y, z);
    }
}
