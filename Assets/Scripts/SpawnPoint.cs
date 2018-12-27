using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Public Variables
    public Vector3 Spawn = new Vector3();

    // Global Variables
      //  public  GenVarStorage GVS;
    public Rigidbody m_CharacterController;
    
    // Use this for initialization
	private void Start ()
    {
        // Initialize the WorldGenerator and Player variables
   //  m_CharacterController = GetComponent<Rigidbody>();
   ///  GVS = GameObject.Find("Generation Variable Storage").GetComponent<GenVarStorage>();

        // Setup the player's spawn
      //  SetupSpawn();        
	}
	
	private void SetupSpawn()
    {//
        // int size = 100;
        // Set the spawn Vector3 to the middle of the map
      //  Spawn = new Vector3(Mathf.Floor(size),Mathf.Floor(size),Mathf.Ceil(size));
        // Move the player to the spawn
     //   m_CharacterController.MovePosition(Spawn);
    }
}
