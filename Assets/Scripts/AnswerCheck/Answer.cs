using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Answer : MonoBehaviour
{
    private UIManager uiManager;

    public TextMeshPro cubeText;

    // 0 - black, 1 - green, 2 - red 
    public Material[] material = new Material[3];

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        this.gameObject.name += "_" + uiManager.sumCount;
        cubeText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "AnswerCube")
        {
            //if text is the same as the text on the cube show correct
            SumCube sumCube = new SumCube();
            sumCube.TextOnCube = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
            if (this.gameObject.tag == "AnswerX")
            {
                if (sumCube.TextOnCube == "x = "+gameManager.xValue.ToString())
                {
                    cubeText.text = "Correct";
                    this.GetComponent<MeshRenderer>().material = material[1];
                    other.transform.GetComponent<XRGrabInteractable>().enabled = false;
                    gameManager.xFoundValue = gameManager.xValue;
                    uiManager.OnClickResetSum();
                   
                }
                else
                {
                    cubeText.text = "Incorrect";
                    this.GetComponent<MeshRenderer>().material = material[2];
                }
            }
            else if (this.gameObject.tag == "AnswerY")
            {
                if (sumCube.TextOnCube == "y = "+gameManager.yValue.ToString())
                {
                    cubeText.text = "Correct";
                    this.GetComponent<MeshRenderer>().material = material[1];
                    other.transform.GetComponent<XRGrabInteractable>().enabled = false;
                    gameManager.yFoundValue = gameManager.yValue;
                    uiManager.OnClickResetSum();
                    
                }
                else
                {
                    cubeText.text = "Incorrect";
                    this.GetComponent<MeshRenderer>().material = material[2];
                }
            }
        }
    }
    
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "AnswerCube")
        {
            if (this.gameObject.tag == "AnswerX")
            {
                cubeText.text = "";
                this.GetComponent<MeshRenderer>().material = material[0];
                other.transform.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else if (this.gameObject.tag == "AnswerY")
            {
                cubeText.text = "";
                this.GetComponent<MeshRenderer>().material = material[0];
                other.transform.GetComponent<XRGrabInteractable>().enabled = true;
            }
        }
    }
}
