using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed;

    public UIManager uiManager;

    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Correct"))
        {
            Destroy(this.gameObject);
            
            uiManager.SetAnswerStatus("Correct");
            
            gameManager.canHitCube = false;

            uiManager.sumCalculated = false;

            gameManager.DestroyCubesOnFractionError();

            //Do a list of Doors that will only be used only on Game 1
            //Depending on the current answer, open the door
            //Destroy the script on the collided object to stop it from starting a new sum
            gameManager.player.pressurePlate.GetComponent<BoxCollider>().enabled = false;
            gameManager.player.isOnPressurePlate = false;
            gameManager.player.pressurePlate = null;
            gameManager.cubesCreated = false;
            gameManager.locomotionSystem.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 2.5f;


            if (gameManager.gameScene)
            {
                gameManager.doors[gameManager.questionNumber - 1].GetComponent<Animator>().SetBool("isConditionMet", true);
                gameManager.doors[gameManager.questionNumber - 1].GetComponent<AudioSource>().Play();
            }
            else if(gameManager.tutorialOfGameScene1)
            {
                gameManager.doors[gameManager.questionNumber].GetComponent<Animator>().SetBool("isConditionMet", true);
                gameManager.doors[gameManager.questionNumber].GetComponent<AudioSource>().Play();
            }
            
            foreach(var cube in gameManager.cubeAnswerObjects)
            {
                cube.GetComponent<BoxCollider>().enabled = false;
            }

            Destroy(gameManager.cube1);
            Destroy(gameManager.cube2);
            Destroy(gameManager.cube3);

            uiManager.UpdateHandBoardText();

            //Test if with the invisible colliders, the question number is being incremented as intended
            //If any issues are caused, place the increment question number here
        }
        else if (other.gameObject.name.Contains("Wrong"))
        {
            Destroy(this.gameObject);
            
            uiManager.SetAnswerStatus("Wrong");
        }

        if (other.gameObject.tag == "ClearWhiteBoard")
        {
            gameManager.clearWhiteboard = true;
            Destroy(this.gameObject);
        }
    }
}
