using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotmanager : MonoBehaviour
{
    [Range(0, 4)] public int selectedDirection; // up down right left
     public int x, y;
    [SerializeField] private int x_, y_;
    private void FixedUpdate()
    {
     
        switch (selectedDirection)
        {
            case 1:
                x = 0;
                y = y_;
                break;
            case 2:
                x = 0;
                y = -y_;
                break;
            case 3:
                x = x_;
                y = 0;
                break;
            case 4:
                x = -x_;
                y = 0;
                break;
            default:
                x = 0;
                y = 0;
                break;

        }

        
    }
}
