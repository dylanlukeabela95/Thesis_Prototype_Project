using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mehroz;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public int correctAnswers, incorrectAnswers;

    [Header("Game 1 Scene")] public Vector3[] cubeAnswerSpawns = new Vector3[3];
    public GameObject[] cubeAnswerObjects = new GameObject[3];
    public GameObject answerCube;
    public Material[] materials = new Material[3];
    public GameObject cube1, cube2, cube3;
    public float maxHealth, currentHealth;
    public bool canHitCube = true;
    public float canHitCubeTimer = 1;
    public LocomotionSystem locomotionSystem;

    [Header("Game 2 Scene")] public GameObject questionCube;
    public List<GameObject> questionCubes = new List<GameObject>();
    public GameObject[] spawnLocations = new GameObject[6];
    public GameObject[] spawnLocations_2 = new GameObject[6];
    public GameObject[] spawnLocations_3 = new GameObject[6];
    public bool isDoorOpen = false;
    public Fraction startNewA, startNewB, startNewC, startNewD, startNewE, startNewF;
    public Fraction newA, newB, newC, newD, newE, newF;
    public Fraction sumAD, sumBE, sumCF, sumAC, sumBC, sumDF, sumEF;
    public int questionNumber = 0;

    public Fraction xValue, yValue;
    public string xFoundValue = "x", yFoundValue = "y";

    public bool clearWhiteboard = false;

    public List<SumCube> cubesInAddRoom;
    public List<GameObject> cubesAddRoomGameObject;
    public SumCube[] cubesInSubRoom;
    public GameObject[] cubesSubRoomGameObject;
    public List<SumCube> cubesInMulRoom;
    public List<GameObject> cubesMulRoomGameObject;
    public List<SumCube> cubesInDivRoom;
    public List<GameObject> cubesDivRoomGameObject;
    public List<SumCube> cubesInEqualRoom;
    public List<GameObject> cubesEqualRoomGameObject;

    [Header("Scene Management")] 
    public bool tutorialOfGameScene1;
    public bool tutorialOfGameScene2;
    public bool gameScene;
    public bool gameScene2;
    public bool startScene;

    [Header("Doors for Scene 1")]
    public GameObject[] doors;

    [Header("Check for Values Set")] public bool valueSet;

    //Array because we are going to spawn the cube depending on the sum count
    public GameObject[] subtractionCubeSpawn = new GameObject[4];

    public Player player;

    //For Scene 1
    public bool cubesCreated = false;

    //0 -> Pink
    //1 -> Blue
    //2 -> Cyan
    //3 -> Green
    //4 -> Orange
    //5 -> Red
    //6 -> Gold (Answer Color)
    //7 -> Black (Pod Color)
    public Material[] materialArray = new Material[8];
    public bool colorChosen = false;
    public bool textPlaced = false;
    public int[] colorChosenList = new int[3];

    public bool xEnabled, yEnabled;
    
    public bool additionPerformed;
    public bool subtractionPerformed;
    public bool multiplicationPerformed;
    public bool divisionPerformed;
    public bool equalPerformed;
    
    //Scene2
    public bool correctAnswerPodX, correctAnswerPodY;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();

        cubesInAddRoom = new List<SumCube>();
        cubesInSubRoom = new SumCube[3];
        cubesInMulRoom = new List<SumCube>();
        cubesInDivRoom = new List<SumCube>();
        cubesInEqualRoom = new List<SumCube>();

        cubesSubRoomGameObject = new GameObject[3];
        
        if (gameScene || tutorialOfGameScene1)
        {
            maxHealth = 100;
            currentHealth = 100;
        }
        else if (gameScene2 || tutorialOfGameScene2)
        {
            SpawnQuestionCubes(spawnLocations);
        }

        if(!startScene)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(newA);
        //Debug.Log(newB);
        //Debug.Log(newC);
        //Debug.Log(newD);
        //Debug.Log(newE);
        //Debug.Log(newF);
        //Debug.Log("------------------");

        if (gameScene || tutorialOfGameScene1)
        {
            SetCubeAnswerSpawnLocations();
            if (uiManager.simultaneousEquation1.text != null &&
                uiManager.simultaneousEquation1.text != "Proceed Forward" &&
                player.isOnPressurePlate)
            {
                foreach (var cubeObj in cubeAnswerObjects)
                {
                    if (cubeObj.transform.childCount == 0)
                    {
                        SpawnCube();
                    }
                }

                PaintAnswerCubes();
            }

            CanHitCubeTimerDecrease();
        }
        else if (gameScene2 || tutorialOfGameScene2)
        {
            if (!textPlaced && valueSet)
            {
                for (int i = 0; i < 6; i++)
                {
                    SetTextForCubes(i,0);
                }
            }

            if (!colorChosen)
            {
                SetColorForCubes(0);
            }


            if (gameScene2)
            {
                switch (questionNumber)
                {
                    case 1:
                        uiManager.SetEquationColor(uiManager.simultaneousEquation1List[0], 0);
                        uiManager.SetEquationColor(uiManager.simultaneousEquation2List[0], 1);
                        break;
                    case 2:
                        uiManager.SetEquationColor(uiManager.simultaneousEquation1List[1], 0);
                        uiManager.SetEquationColor(uiManager.simultaneousEquation2List[1], 1);
                        break;
                    case 3:
                        uiManager.SetEquationColor(uiManager.simultaneousEquation1List[2], 0);
                        uiManager.SetEquationColor(uiManager.simultaneousEquation2List[2], 1);
                        break;
                }
            }
            else
            {
                uiManager.SetEquationColor(uiManager.simultaneousEquation1, 0);
                uiManager.SetEquationColor(uiManager.simultaneousEquation2, 1);
            }
            

            if (additionPerformed)
            {
                GenerateNewAdditionCube();
                additionPerformed = false;
            }

            if (subtractionPerformed)
            {
                GenerateNewSubtractionCube();
                subtractionPerformed = false;
            }

            if (multiplicationPerformed)
            {
                GenerateMultiplicationCubes(uiManager.multiplyValue);
                multiplicationPerformed = false;
            }

            if (divisionPerformed)
            {
                GenerateDivisionCubes(uiManager.divideValue);
                divisionPerformed = false;
            }

            if (equalPerformed)
            {
                GenerateEqualCube();
                equalPerformed = false;
            }
            
        }

        //Debug.Log("YValue - " + yValue.ToString());
    }

    #region private void SetCubeAnswerSpawnLocations()

    private void SetCubeAnswerSpawnLocations()
    {
        for (int i = 0; i < cubeAnswerObjects.Length; i++)
        {
            cubeAnswerSpawns[i] = cubeAnswerObjects[i].transform.position;
        }
    }

    #endregion

    #region private void SpawnCube()

    private void SpawnCube()
    {
        int random = Random.Range(0, cubeAnswerObjects.Length);
        //Debug.Log(random);
        switch (random)
        {
            case 0:
                cube1 = Instantiate(answerCube, cubeAnswerSpawns[0], Quaternion.identity,
                    cubeAnswerObjects[0].transform);
                int randomRemaining = Random.Range(1, cubeAnswerObjects.Length);
                switch (randomRemaining)
                {
                    case 1:
                        cube2 = Instantiate(answerCube, cubeAnswerSpawns[1], Quaternion.identity,
                            cubeAnswerObjects[1].transform);
                        cube3 = Instantiate(answerCube, cubeAnswerSpawns[2], Quaternion.identity,
                            cubeAnswerObjects[2].transform);
                        break;
                    case 2:
                        cube2 = Instantiate(answerCube, cubeAnswerSpawns[2], Quaternion.identity,
                            cubeAnswerObjects[2].transform);
                        cube3 = Instantiate(answerCube, cubeAnswerSpawns[1], Quaternion.identity,
                            cubeAnswerObjects[1].transform);
                        break;
                }

                break;
            case 1:
                cube1 = Instantiate(answerCube, cubeAnswerSpawns[1], Quaternion.identity,
                    cubeAnswerObjects[1].transform);

                while (true)
                {
                    int randomRemaining2 = Random.Range(0, cubeAnswerObjects.Length);

                    if (randomRemaining2 != 1)
                    {
                        switch (randomRemaining2)
                        {
                            case 0:
                                cube2 = Instantiate(answerCube, cubeAnswerSpawns[0], Quaternion.identity,
                                    cubeAnswerObjects[0].transform);
                                cube3 = Instantiate(answerCube, cubeAnswerSpawns[2], Quaternion.identity,
                                    cubeAnswerObjects[2].transform);
                                break;
                            case 2:
                                cube2 = Instantiate(answerCube, cubeAnswerSpawns[2], Quaternion.identity,
                                    cubeAnswerObjects[2].transform);
                                cube3 = Instantiate(answerCube, cubeAnswerSpawns[0], Quaternion.identity,
                                    cubeAnswerObjects[0].transform);
                                break;
                        }

                        break;
                    }
                }

                break;
            case 2:
                cube1 = Instantiate(answerCube, cubeAnswerSpawns[2], Quaternion.identity,
                    cubeAnswerObjects[2].transform);
                int randomRemaining3 = Random.Range(0, cubeAnswerObjects.Length - 1);
                switch (randomRemaining3)
                {
                    case 0:
                        cube2 = Instantiate(answerCube, cubeAnswerSpawns[0], Quaternion.identity,
                            cubeAnswerObjects[0].transform);
                        cube3 = Instantiate(answerCube, cubeAnswerSpawns[1], Quaternion.identity,
                            cubeAnswerObjects[1].transform);
                        break;
                    case 1:
                        cube2 = Instantiate(answerCube, cubeAnswerSpawns[1], Quaternion.identity,
                            cubeAnswerObjects[1].transform);
                        cube3 = Instantiate(answerCube, cubeAnswerSpawns[0], Quaternion.identity,
                            cubeAnswerObjects[0].transform);
                        break;
                }

                break;
        }
    }

    #endregion

    #region public void PaintAnswerCubes()

    public void PaintAnswerCubes()
    {
        //Material 0 = Red
        //Material 1 = Blue
        //Material 2 = Green
        if (cube1 != null)
        {
            if (uiManager.isCorrectRed)
            {
                cube1.GetComponent<MeshRenderer>().material = materials[0];
            }
            else if (uiManager.isCorrectBlue)
            {
                cube1.GetComponent<MeshRenderer>().material = materials[1];
            }
            else if (uiManager.isCorrectGreen)
            {
                cube1.GetComponent<MeshRenderer>().material = materials[2];
            }

            if (!cube1.name.Contains("Correct"))
                cube1.name += "Correct";
        }

        if (cube2 != null)
        {
            if (uiManager.isWrongOneRed)
            {
                cube2.GetComponent<MeshRenderer>().material = materials[0];
            }
            else if (uiManager.isWrongOneBlue)
            {
                cube2.GetComponent<MeshRenderer>().material = materials[1];
            }
            else if (uiManager.isWrongOneGreen)
            {
                cube2.GetComponent<MeshRenderer>().material = materials[2];
            }

            if (!cube2.name.Contains("Wrong"))
                cube2.name += "WrongOne";
        }

        if (cube3 != null)
        {
            if (uiManager.isWrongTwoRed)
            {
                cube3.GetComponent<MeshRenderer>().material = materials[0];
            }
            else if (uiManager.isWrongTwoBlue)
            {
                cube3.GetComponent<MeshRenderer>().material = materials[1];
            }
            else if (uiManager.isWrongTwoGreen)
            {
                cube3.GetComponent<MeshRenderer>().material = materials[2];
            }

            if (!cube3.name.Contains("Wrong"))
                cube3.name += "WrongTwo";
        }

    }

    #endregion

    #region public void DestroyCubesOnFractionError()

    public void DestroyCubesOnFractionError()
    {
        if (cube1 != null && cube2 != null && cube3 != null)
        {
            Destroy(cube1);
            Destroy(cube2);
            Destroy(cube3);
        }

        uiManager.RefreshBooleanColors();
    }

    #endregion

    #region public void AdjustHealth(float amount, string choice)

    public void AdjustHealth(float amount, string choice)
    {
        switch (choice)
        {
            case "Increase":
                currentHealth += amount;
                break;
            case "Decrease":
                currentHealth -= amount;
                break;
        }
    }

    #endregion

    #region public void CanHitCubeTimerDecrease()

    public void CanHitCubeTimerDecrease()
    {
        if (!canHitCube)
        {
            canHitCubeTimer -= Time.deltaTime;
        }

        if (canHitCubeTimer <= 0)
        {
            canHitCube = true;
            canHitCubeTimer = 1;
        }
    }

    #endregion

    #region public void SetTextForCubes(int i)

    public void SetTextForCubes(int i, int start)
    {
        textPlaced = true;

        questionCubes[start].GetComponentsInChildren<TextMeshPro>()[i].text = newA.ToString() + "x";
        questionCubes[start+1].GetComponentsInChildren<TextMeshPro>()[i].text = newB.ToString() + "y";
        questionCubes[start+2].GetComponentsInChildren<TextMeshPro>()[i].text = newC.ToString();
        questionCubes[start + 3].GetComponentsInChildren<TextMeshPro>()[i].text = newD.ToString() + "x";
        questionCubes[start + 4].GetComponentsInChildren<TextMeshPro>()[i].text = newE.ToString() + "y";
        questionCubes[start + 5].GetComponentsInChildren<TextMeshPro>()[i].text = newF.ToString();

        if (xFoundValue != "")
        {
            questionCubes[start].GetComponentsInChildren<TextMeshPro>()[i].text = newA.ToString();
            questionCubes[start + 3].GetComponentsInChildren<TextMeshPro>()[i].text = newD.ToString();
        }

        if (yFoundValue != "")
        {
            questionCubes[start + 1].GetComponentsInChildren<TextMeshPro>()[i].text = newB.ToString();
            questionCubes[start + 4].GetComponentsInChildren<TextMeshPro>()[i].text = newE.ToString();
        }
    }

    #endregion

    #region public void SetColorForCubes(int start)

    public void SetColorForCubes(int start)
    {
        colorChosen = true;
        int randomColor = Random.Range(0, materialArray.Length-2);
        colorChosenList[0] = randomColor;

        for (int i = start; i <= start+2; i++)
        {
            questionCubes[i].GetComponent<MeshRenderer>().material = materialArray[randomColor];
        }

        while (true)
        {
            randomColor = Random.Range(0, materialArray.Length-2);
                    
            if (!colorChosenList.Contains(randomColor))
            {
                colorChosenList[1] = randomColor;
                for (int i = start+3; i <= start+5; i++)
                {
                    questionCubes[i].GetComponent<MeshRenderer>().material = materialArray[randomColor];
                }
                break;
            }
        }
    }

    #endregion
    
    #region public void GenerateNewAdditionCube()

    public void GenerateNewAdditionCube()
    {
        var cubeLocation = cubesAddRoomGameObject[0].transform.position;
        GameObject newQuestionCube = Instantiate(questionCube, cubeLocation, Quaternion.identity);
        xEnabled = false;
        yEnabled = false;

        Fraction value1 = 0;
        Fraction value2 = 0;
        Fraction total = 0;

        if (
            (
                (cubesAddRoomGameObject[0].name == "CubeA") && (cubesAddRoomGameObject[1].name == "CubeD") ||
                (cubesAddRoomGameObject[0].name == "CubeD") && (cubesAddRoomGameObject[1].name == "CubeA")
            )
        )
        {
            value1 = newA;
            value2 = newD;
            newQuestionCube.name = "SumAD";
            sumAD = value1 + value2;
            xEnabled = true;
        }
        else if (
            (
                (cubesAddRoomGameObject[0].name == "CubeB") && (cubesAddRoomGameObject[1].name == "CubeE") ||
                (cubesAddRoomGameObject[0].name == "CubeE") && (cubesAddRoomGameObject[1].name == "CubeB")
            )
        )
        {
            value1 = newB;
            value2 = newE;
            newQuestionCube.name = "SumBE";
            sumBE = value1 + value2;
            yEnabled = true;
        }
        else if (
            (
                (cubesAddRoomGameObject[0].name == "CubeC") && (cubesAddRoomGameObject[1].name == "CubeF") ||
                (cubesAddRoomGameObject[0].name == "CubeF") && (cubesAddRoomGameObject[1].name == "CubeC")
            )
        )
        {
            value1 = newC;
            value2 = newF;
            newQuestionCube.name = "SumCF";
            sumCF = value1 + value2;
        }
        else if ((cubesAddRoomGameObject[0].name == "CubeA") && (cubesAddRoomGameObject[1].name == "CubeC"))
        {
            value1 = newA;
            value2 = newC;
            newQuestionCube.name = "SumAC";
            sumAC = value1 + value2;
        }
        else if ((cubesAddRoomGameObject[0].name == "CubeB") && (cubesAddRoomGameObject[1].name == "CubeC"))
        {
            value1 = newB;
            value2 = newC;
            newQuestionCube.name = "SumBC";
            sumBC = value1 + value2;
        }
        else if ((cubesAddRoomGameObject[0].name == "CubeD") && (cubesAddRoomGameObject[1].name == "CubeF"))
        {
            value1 = newD;
            value2 = newF;
            newQuestionCube.name = "SumDF";
            sumDF = value1 + value2;
        }
        else if ((cubesAddRoomGameObject[0].name == "CubeE") && (cubesAddRoomGameObject[1].name == "CubeF"))
        {
            value1 = newE;
            value2 = newF;
            newQuestionCube.name = "SumEF";
            sumEF = value1 + value2;
        }
        
        cubesAddRoomGameObject = new List<GameObject>();
        total = value1 + value2;

        newQuestionCube.GetComponent<MeshRenderer>().material = materialArray[6];

        if (xEnabled)
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString()+"x";
        }
        else if (yEnabled)
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString()+"y";
        }
        else
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString();
        }
    }
    #endregion

    #region public void GenerateNewSubtractionCube()

    public void GenerateNewSubtractionCube()
    {
        Vector3 cubeLocation = Vector3.zero;
        //Change cubeLocation depending on the sum
        switch (questionNumber)
        {
            case 0:
            case 1:
                cubeLocation = new Vector3(subtractionCubeSpawn[0].transform.position.x, subtractionCubeSpawn[0].transform.position.y + 1, subtractionCubeSpawn[0].transform.position.z);
                break;
            case 2:
                cubeLocation = new Vector3(subtractionCubeSpawn[1].transform.position.x, subtractionCubeSpawn[1].transform.position.y + 1, subtractionCubeSpawn[1].transform.position.z);
                break;
            case 3:
                cubeLocation = new Vector3(subtractionCubeSpawn[2].transform.position.x, subtractionCubeSpawn[2].transform.position.y + 1, subtractionCubeSpawn[2].transform.position.z);
                break;
        }

        GameObject newQuestionCube = Instantiate(questionCube, cubeLocation, Quaternion.identity);
        xEnabled = false;
        yEnabled = false;

        Fraction value1 = 0;
        Fraction value2 = 0;
        Fraction total = 0;

        if (
            (
                (cubesSubRoomGameObject[0].name == "CubeA") && (cubesSubRoomGameObject[1].name == "CubeD")
            )
        )
        {
            value1 = newA;
            value2 = newD;
            newQuestionCube.name = "SumAD";
            sumAD = value1 - value2;
            xEnabled = true;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeD") && (cubesSubRoomGameObject[1].name == "CubeA")
        )
        {
            value1 = newD;
            value2 = newA;
            newQuestionCube.name = "SumAD";
            sumAD = value1 - value2;
            xEnabled = true;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeB") && (cubesSubRoomGameObject[1].name == "CubeE")
            )
        )
        {
            value1 = newB;
            value2 = newE;
            newQuestionCube.name = "SumBE";
            sumBE = value1 - value2;
            yEnabled = true;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeE") && (cubesSubRoomGameObject[1].name == "CubeB")
        )
        {
            value1 = newE;
            value2 = newB;
            newQuestionCube.name = "SumBE";
            sumBE = value1 - value2;
            yEnabled = true;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeC") && (cubesSubRoomGameObject[1].name == "CubeF")
            )
        )
        {
            value1 = newC;
            value2 = newF;
            newQuestionCube.name = "SumCF";
            sumCF = value1 - value2;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeF") && (cubesSubRoomGameObject[1].name == "CubeC")
        )
        {
            value1 = newF;
            value2 = newC;
            newQuestionCube.name = "SumCF";
            sumCF = value1 - value2;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeA") && (cubesSubRoomGameObject[1].name == "CubeC")
            )
        )
        {
            value1 = newA;
            value2 = newC;
            newQuestionCube.name = "SumAC";
            sumAC = value1 - value2;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeC") && (cubesSubRoomGameObject[1].name == "CubeA")
        )
        {
            value1 = newC;
            value2 = newA;
            newQuestionCube.name = "SumAC";
            sumAC = value1 - value2;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeB") && (cubesSubRoomGameObject[1].name == "CubeC")
            )
        )
        {
            value1 = newB;
            value2 = newC;
            newQuestionCube.name = "SumBC";
            sumBC = value1 - value2;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeC") && (cubesSubRoomGameObject[1].name == "CubeB")
        )
        {
            value1 = newC;
            value2 = newB;
            newQuestionCube.name = "SumBC";
            sumBC = value1 - value2;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeD") && (cubesSubRoomGameObject[1].name == "CubeF")
            )
        )
        {
            value1 = newD;
            value2 = newF;
            newQuestionCube.name = "SumDF";
            sumDF = value1 - value2;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeF") && (cubesSubRoomGameObject[1].name == "CubeD")
        )
        {
            value1 = newF;
            value2 = newD;
            newQuestionCube.name = "SumDF";
            sumDF = value1 - value2;
        }
        else if (
            (
                (cubesSubRoomGameObject[0].name == "CubeE") && (cubesSubRoomGameObject[1].name == "CubeF")
            )
        )
        {
            value1 = newE;
            value2 = newF;
            newQuestionCube.name = "SumEF";
            sumEF = value1 - value2;
        }
        else if (
            (cubesSubRoomGameObject[0].name == "CubeF") && (cubesSubRoomGameObject[1].name == "CubeE")
        )
        {
            value1 = newF;
            value2 = newE;
            newQuestionCube.name = "SumEF";
            sumEF = value1 - value2;
        }
        
        cubesSubRoomGameObject = new GameObject[4];
        total = value1 - value2;

        newQuestionCube.GetComponent<MeshRenderer>().material = materialArray[6];

        if (xEnabled)
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString()+"x";
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString()+"x";
        }
        else if (yEnabled)
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString()+"y";
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString()+"y";
        }
        else
        {
            newQuestionCube.transform.GetChild(0).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(1).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(2).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(3).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(4).GetComponent<TextMeshPro>().text = total.ToString();
            newQuestionCube.transform.GetChild(5).GetComponent<TextMeshPro>().text = total.ToString();
        }
    }
    #endregion
    
    #region public void GenerateMultiplicationCubes()

    public void GenerateMultiplicationCubes(int multiplyingValue)
    {
        
        
        foreach (var cube in cubesMulRoomGameObject)
        {
            if (cube.name == "CubeA")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text =
                            (newA * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text =
                            (newA * multiplyingValue).ToString() + "x";
                    }
                    
                }
                newA *= multiplyingValue;
            }
            else if (cube.name == "CubeB")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newB * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newB * multiplyingValue).ToString()+"y";
                    }
                    
                }
                newB *= multiplyingValue;
            }
            else if (cube.name == "CubeC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newC * multiplyingValue).ToString();
                }
                newC *= multiplyingValue;
            }
            else if (cube.name == "CubeD")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text =
                            (newD * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text =
                            (newD * multiplyingValue).ToString() + "x";
                    }
                }
                newD *= multiplyingValue;
            }
            else if (cube.name == "CubeE")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newE * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newE * multiplyingValue).ToString()+"y";
                    }
                }
                newE *= multiplyingValue;
            }
            else if (cube.name == "CubeF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (newF * multiplyingValue).ToString();
                }
                newF *= multiplyingValue;
            }
            else if (cube.name == "SumAD")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumAD * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumAD * multiplyingValue).ToString()+"y";
                    }                }
                
                sumAD *= multiplyingValue;
            }
            else if (cube.name == "SumBE")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumBE * multiplyingValue).ToString();
                    }
                    else
                    {
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumBE * multiplyingValue).ToString()+"y";
                    }
                }
                
                sumBE *= multiplyingValue;
            }
            else if (cube.name == "SumCF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumCF * multiplyingValue).ToString();
                }
                
                sumCF *= multiplyingValue;
            }
            else if (cube.name == "SumAC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumAC * multiplyingValue).ToString();
                }
                
                sumAC *= multiplyingValue;
            }
            else if (cube.name == "SumBC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumBC * multiplyingValue).ToString();
                }
                
                sumBC *= multiplyingValue;
            }
            else if (cube.name == "SumDF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumDF * multiplyingValue).ToString();
                }
                
                sumDF *= multiplyingValue;
            }
            else if (cube.name == "SumEF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = (sumEF * multiplyingValue).ToString();
                }
                
                sumEF *= multiplyingValue;
            }
        }
    }
    #endregion
    
    #region public void GenerateDivisionCubes(int divisionValue)

    public void GenerateDivisionCubes(int divisionValue)
    {
        foreach (var cube in cubesDivRoomGameObject)
        {
            if (cube.name == "CubeA")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        var fraction = new Fraction((long)newA, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    }
                    else
                    {
                        var fraction = new Fraction((long)newA, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString() + "x";
                    }

                }

                newA = new Fraction((long)newA, divisionValue);
            }
            
            if (cube.name == "CubeB")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        var fraction = new Fraction((long)newB, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();   
                    }
                    else
                    {
                        var fraction = new Fraction((long)newB, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString() + "y";
                    }
                }

                newB = new Fraction((long)newB, divisionValue);
            }
            
            if (cube.name == "CubeC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)newC, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    
                }

                newC = new Fraction((long)newC, divisionValue);
            }
            
            if (cube.name == "CubeD")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        var fraction = new Fraction((long)newD, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    }
                    else
                    {
                        var fraction = new Fraction((long)newD, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString() + "x";
                    }
                    
                }

                newD = new Fraction((long)newD, divisionValue);
            }
            
            if (cube.name == "CubeE")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        var fraction = new Fraction((long)newE, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();   
                    }
                    else
                    {
                        var fraction = new Fraction((long)newE, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString() + "y";
                    }
                    
                }

                newE = new Fraction((long)newE, divisionValue);
            }
            
            if (cube.name == "CubeF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)newF, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    
                }

                newF = new Fraction((long)newF, divisionValue);
            }
            
            if (cube.name == "SumAD")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (xFoundValue != "")
                    {
                        var fraction = new Fraction((long)sumAD, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    }
                    else
                    {
                        var fraction = new Fraction((long)sumAD, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString()+"x";
                    }
                }
                
                sumAD = new Fraction((long)sumAD, divisionValue);
            }
            
            if (cube.name == "SumBE")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    if (yFoundValue != "")
                    {
                        var fraction = new Fraction((long)sumBE, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                    }
                    else
                    {
                        var fraction = new Fraction((long)sumBE, divisionValue);
                        cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString()+"y";
                    }
                }
                
                sumBE = new Fraction((long)sumBE, divisionValue);
            }
            
            if (cube.name == "SumCF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)sumCF, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                }
                
                sumCF = new Fraction((long)sumCF, divisionValue);
            }
            
            if (cube.name == "SumAC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)sumAC, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                }
                
                sumAC = new Fraction((long)sumAC, divisionValue);
            }
            
            if (cube.name == "SumBC")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)sumBC, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                }
                
                sumBC = new Fraction((long)sumBC, divisionValue);
            }
            
            if (cube.name == "SumDF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)sumDF, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                }
                
                sumDF = new Fraction((long)sumDF, divisionValue);
            }
            
            if (cube.name == "SumEF")
            {
                for (int i = 0; i < cube.transform.childCount; i++)
                {
                    var fraction = new Fraction((long)sumEF, divisionValue);
                    cube.transform.GetChild(i).GetComponent<TextMeshPro>().text = fraction.ToString();
                }
                
                sumEF = new Fraction((long)sumEF, divisionValue);
            }
        }
    }
    #endregion
    
    #region public void GenerateEqualCube()

    public void GenerateEqualCube()
    {
        var cubeLocation = cubesEqualRoomGameObject[0].transform.position;
        GameObject newQuestionCube = Instantiate(questionCube, cubeLocation, Quaternion.identity);
        newQuestionCube.tag = "AnswerCube";

        string equals = "";
        string letter = "";

        if (cubesInEqualRoom[0].TextOnCube == "1x")
        {
            letter = "x";
            equals = letter + " = " + cubesInEqualRoom[1].TextOnCube;
        }
        else if(cubesInEqualRoom[0].TextOnCube == "1y")
        {
            letter = "y";
            equals = letter + " = " + cubesInEqualRoom[1].TextOnCube;
        }
        else if (cubesInEqualRoom[1].TextOnCube == "1x")
        {
            letter = "x";
            equals = letter + " = " + cubesInEqualRoom[0].TextOnCube;
        }
        else if(cubesInEqualRoom[1].TextOnCube == "1y")
        {
            letter = "y";
            equals = letter + " = " + cubesInEqualRoom[0].TextOnCube;
        }

       

        cubesInEqualRoom = new List<SumCube>();
        cubesEqualRoomGameObject = new List<GameObject>();

        newQuestionCube.GetComponent<MeshRenderer>().material = materialArray[6];

        for (int i = 0; i < newQuestionCube.transform.childCount; i++)
        {
            newQuestionCube.transform.GetChild(i).GetComponent<TextMeshPro>().text = equals;
        }
    }

    #endregion
    
    
    #region public void SpawnQuestionCubes(GameObject[] spawnLocations)

    public void SpawnQuestionCubes(GameObject[] spawnLocations)
    {
        foreach (var spawnLocation in spawnLocations)
        {
            GameObject questionCubeGameObject = Instantiate(questionCube,
                new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y + 0.44f,
                    spawnLocation.transform.position.z), Quaternion.identity);
            questionCubes.Add(questionCubeGameObject);
            questionCube.GetComponent<SumCubeObject>().cubeIndex = questionCubes.Count;
                
            switch (questionCubes.Count)
            {
                case 1:
                case 7:
                case 13:
                    questionCubeGameObject.name = "CubeA";
                    break;
                case 2:
                case 8:
                case 14:
                    questionCubeGameObject.name = "CubeB";
                    break;
                case 3:
                case 9:
                case 15:
                    questionCubeGameObject.name = "CubeC";
                    break;
                case 4:
                case 10:
                case 16:
                    questionCubeGameObject.name = "CubeD";
                    break;
                case 5:
                case 11:
                case 17:
                    questionCubeGameObject.name = "CubeE";
                    break;
                case 6:
                case 12:
                case 18:
                    questionCubeGameObject.name = "CubeF";
                    break;
                            
            }
        }
    }
    #endregion

    #region public void SetCubesForNewSum(GameObject[] spawnLocation)
    public void SetCubesForNewSum(GameObject[] spawnLocation)
    {
        uiManager.sumChanged = false;
        uiManager.sumCalculated = false;

        xFoundValue = "";
        yFoundValue = "";

        uiManager.divideValue = 0;
        uiManager.multiplyValue = 0;

        uiManager.GameScreen();

        SpawnQuestionCubes(spawnLocation);
        //Debug.Log(xValue.ToString());
        //Debug.Log(yValue.ToString());

        if (questionNumber == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                SetTextForCubes(i, 6);
                SetColorForCubes(6);
            }
        }
        else if (questionNumber == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                SetTextForCubes(i, 6);
                SetColorForCubes(6);
            }
        }
    }
    #endregion
}
