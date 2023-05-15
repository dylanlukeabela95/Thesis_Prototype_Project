using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warper : MonoBehaviour
{
    private Vector3 playerSpawn;

    public bool isTutorialSceneGame2;
    public bool isSceneGame2;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(isTutorialSceneGame2)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null ) 
            { 
                playerSpawn = player.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTutorialSceneGame2)
        {
            if(other.gameObject.tag == "Player")
            {
                other.transform.position = playerSpawn;
                Debug.Log("HIT");
            }
        }
    }
}
