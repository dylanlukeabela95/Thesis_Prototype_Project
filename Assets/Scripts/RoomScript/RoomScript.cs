using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SumCube")
        {
            if (this.gameObject.tag == "AddRoomCollider")
            {
                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.gameObject.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                gameManager.cubesInAddRoom.Add(sumCube);
                gameManager.cubesAddRoomGameObject.Add(other.gameObject);
            }
            else if (this.gameObject.tag == "MultiplyRoom")
            {
                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.gameObject.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                gameManager.cubesInMulRoom.Add(sumCube);
                gameManager.cubesMulRoomGameObject.Add(other.gameObject);
            }
            else if (this.gameObject.tag == "DivisionRoom")
            {
                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.gameObject.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                gameManager.cubesInDivRoom.Add(sumCube);
                gameManager.cubesDivRoomGameObject.Add(other.gameObject);
            }
            else if (this.gameObject.tag == "EqualRoom")
            {
                SumCube sumCube = new SumCube();
                sumCube.UniqueId = other.GetComponent<SumCubeObject>().cubeIndex;
                sumCube.MaterialOnCube = other.gameObject.GetComponent<MeshRenderer>().material;
                sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                gameManager.cubesInEqualRoom.Add(sumCube);
                gameManager.cubesEqualRoomGameObject.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SumCube")
        { 
            if (this.gameObject.tag == "AddRoomCollider")
            {
                if (gameManager.cubesInAddRoom.Count > 0)
                {
                    var loc = gameManager.cubesInAddRoom.FindIndex(x =>
                        x.UniqueId == other.GetComponent<SumCubeObject>().cubeIndex);
                    gameManager.cubesInAddRoom.RemoveAt(loc);
                    gameManager.cubesAddRoomGameObject.RemoveAt(loc);
                }
            }
            else if (this.gameObject.tag == "MultiplyRoom")
            {
                var loc = gameManager.cubesInMulRoom.FindIndex(x =>
                    x.UniqueId == other.GetComponent<SumCubeObject>().cubeIndex);
                gameManager.cubesInMulRoom.RemoveAt(loc);
                gameManager.cubesMulRoomGameObject.RemoveAt(loc);
            }
            else if (this.gameObject.tag == "DivisionRoom")
            {
                var loc = gameManager.cubesInDivRoom.FindIndex(x =>
                    x.UniqueId == other.GetComponent<SumCubeObject>().cubeIndex);
                gameManager.cubesInDivRoom.RemoveAt(loc);
                gameManager.cubesDivRoomGameObject.RemoveAt(loc);
            }
            else if (this.gameObject.tag == "EqualRoom")
            {
                var loc = gameManager.cubesInEqualRoom.FindIndex(x =>
                    x.UniqueId == other.GetComponent<SumCubeObject>().cubeIndex);
                gameManager.cubesInEqualRoom.RemoveAt(loc);
                gameManager.cubesEqualRoomGameObject.RemoveAt(loc);
            }
        }
    }
}
