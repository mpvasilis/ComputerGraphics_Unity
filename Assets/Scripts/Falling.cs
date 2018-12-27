     using UnityEngine;
     public class Falling : MonoBehaviour
     {
         public float fallingThreshold = 1f;
         public float maxFallingThreshold = 20f;
         public float initialDistance = 1f;
         private RaycastHit hit;
         void Start()
         {
             var dist = 0f;
             GetHitDistance(out dist);
             initialDistance = dist;
         }
         bool GetHitDistance(out float distance)
         {
             distance = 0f;
             Ray downRay = new Ray(transform.position, -Vector3.up); // this is the downward ray
             if (Physics.Raycast(downRay, out hit))
             {
                 distance = hit.distance;
                 return true;
             }
             return false;
         }
         void Update()
         {
             var dist = 0f;
             if (GetHitDistance(out dist))
             {
                 if (initialDistance < dist)
                 {
                     //Get relative distance
                     var relDistance = dist - initialDistance;
                     //Are we actually falling?
                     if (relDistance > fallingThreshold)
                     {
                         //How far are we falling
                         if (relDistance > maxFallingThreshold) Debug.Log("Fell off "+relDistance+" cubes");
                         else Debug.Log("basic falling!");
                     }
                 }
             }
             else
             {
                 Debug.Log("Infinite Fall");
             }
         }
     }