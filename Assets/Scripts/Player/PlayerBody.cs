using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PressurePlate")
        {
            player.isOnPressurePlate = true;
            player.pressurePlate = other.gameObject;
        }
    }
}
