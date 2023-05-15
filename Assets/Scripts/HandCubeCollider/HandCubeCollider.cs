using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCubeCollider : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SumCube")
        {
            if (this.gameObject.transform.parent.name == "RightHandRig")
            {
                player.hasCubeInHandRight = true;
            }
            
            if (this.gameObject.transform.parent.name == "LeftHandRig")
            {
                player.hasCubeInHandLeft = true;
            }
            
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SumCube")
        {
            if (this.gameObject.transform.parent.name == "RightHandRig")
            {
                player.hasCubeInHandRight = false;
            }
            
            if (this.gameObject.transform.parent.name == "LeftHandRig")
            {
                player.hasCubeInHandLeft = false;
            }
            
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
