using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private GameManager gameManager;

    public Animator anim;
    public AudioSource audioSource;

    //For testing
    public bool openDoor;

    public bool isDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(openDoor)
        {
            gameManager.xFoundValue = "1";
            gameManager.yFoundValue = "2";
            anim.SetBool("isConditionMet", true);
            audioSource.Play();
            isDoorOpen = true;
        }

        if (anim != null && audioSource != null)
        {
            if (gameManager.tutorialOfGameScene2 || gameManager.gameScene2)
            {
                if (gameManager.xFoundValue != "" && gameManager.yFoundValue != "" && !isDoorOpen)
                {
                    if (this.gameObject.name == "Door" + gameManager.questionNumber)
                    {
                        //Animate door open
                        anim.SetBool("isConditionMet", true);
                        audioSource.Play();
                        isDoorOpen = true;
                    }
                }
            }
        }
    }
}
