using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class FallPrevent : MonoBehaviour
 {
 
     // Attach this script as a component to the character
     private float Xorigin;
     private float Zorigin;
     private float Height;

    
     private void Awake()
     {
         Xorigin = transform.position.x;
         Zorigin = transform.position.z;
         Height = transform.position.y;
         if (Height < 0)
         {
             Height += 3;
         }
     }
     void Update ()
     {

         if (transform.position.y < 0)
         {
             transform.position = new Vector3(Xorigin, Height, Zorigin);
         }
         
     }
 }

