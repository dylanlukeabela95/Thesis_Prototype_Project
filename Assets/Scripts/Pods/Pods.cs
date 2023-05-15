using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pods : MonoBehaviour
{
    private GameManager gameManager;

    private Player player;

    GameObject cube1,cube2;

    public bool isLeftPodPopulatedSub = false;
    public bool isRightPodPopulatedSub = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cube1 != null)
        {
            if (isLeftPodPopulatedSub && !cube1.GetComponent<SumCubeObject>().isGrabbed)
            {
                
                // cube1.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                isLeftPodPopulatedSub = false;
                cube1 = null;
            }
        }

        if (cube2 != null)
        {
            if (isRightPodPopulatedSub && !cube2.GetComponent<SumCubeObject>().isGrabbed)
            {
                
                // cube2.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                isRightPodPopulatedSub = false;
                cube2 = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "SubPodLeft")
        {
            if (other.gameObject.tag == "SumCube")
            {
                cube1 = other.gameObject;
                isLeftPodPopulatedSub = true;

                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;

                other.GetComponent<Rigidbody>().AddForce(Vector3.down * 50);
                
                this.GetComponent<MeshRenderer>().material = gameManager.materialArray[3];

                gameManager.cubesInSubRoom[0] = sumCube;
                gameManager.cubesSubRoomGameObject[0] = other.gameObject;
            }
        }
        
        if (this.gameObject.tag == "SubPodRight")
        {
            if (other.gameObject.tag == "SumCube")
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                cube2 = other.gameObject;
                isRightPodPopulatedSub = true;

                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                
                this.GetComponent<MeshRenderer>().material = gameManager.materialArray[3];
                other.GetComponent<Rigidbody>().AddForce(Vector3.down * 50);

                gameManager.cubesInSubRoom[1] = sumCube;
                gameManager.cubesSubRoomGameObject[1] = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.tag == "SubPodLeft")
        {
            if (other.gameObject.tag == "SumCube")
            {
                gameManager.cubesInSubRoom[0] = null;
                if (gameManager.cubesInSubRoom.Length > 0)
                {
                    gameManager.cubesSubRoomGameObject[0] = null;
                    this.GetComponent<MeshRenderer>().material = gameManager.materialArray[7];
                }
            }
        }
        
        if (this.gameObject.tag == "SubPodRight")
        {
            if (other.gameObject.tag == "SumCube")
            {
                gameManager.cubesInSubRoom[1] = null;
                if (gameManager.cubesInSubRoom.Length > 0)
                {
                    gameManager.cubesSubRoomGameObject[1] = null;
                    this.GetComponent<MeshRenderer>().material = gameManager.materialArray[7];
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.gameObject.tag == "SubPodLeft")
        {
            if (other.gameObject.tag == "SumCube")
            {
                this.GetComponent<MeshRenderer>().material = gameManager.materialArray[3];
            }
        }
        
        if (this.gameObject.tag == "SubPodRight")
        {
            if (other.gameObject.tag == "SumCube")
            {
                this.GetComponent<MeshRenderer>().material = gameManager.materialArray[3];
            }
        }
    }
}
