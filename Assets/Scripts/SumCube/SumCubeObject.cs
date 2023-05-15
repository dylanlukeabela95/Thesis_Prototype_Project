using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumCubeObject : MonoBehaviour
{
    public int cubeIndex;
    private GameManager gameManager;
    private Player player;
    public bool isGrabbed = false;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindObjectOfType<Player>();

        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
    }

    private void Update()
    {
        if (!player.grabCube)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
