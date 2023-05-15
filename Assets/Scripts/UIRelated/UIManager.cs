using System;
using Mehroz;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    [Header("Simultaneous Equation 1 -> ax + by = c")]
    //ax + by = c
    public TextMeshPro simultaneousEquation1;

    [Header("Simultaneous Equation 2 -> dx + ey = f")]
    //dx + ey = f
    public TextMeshPro simultaneousEquation2;

    public TextMeshPro[] simultaneousEquation1List;
    public TextMeshPro[] simultaneousEquation2List;

    [Header("Answer Text")]
    public Text answerText;
    public GameObject answerTextPrefab;

    public int a, b, c, d, e, f;

    public bool sumCalculated = false;

    public bool sumAnswered = false;

    [Header("Answer Locations")]
    public GameObject[] answerLocations;

    [Header("Canvas")]
    public GameObject canvas;

    private int randomValue;

    public GameObject correctAnswer, wrongAnswer1, wrongAnswer2;

    [Header("Answer Status")]
    public GameObject answerCorrectIncorrectStatus;

    private bool buttonClicked = false;
    private bool correct = false;
    private bool wrong = false;

    [SerializeField]
    private bool isMainScreen = true;
    [SerializeField]
    private bool isGameScreen = false;
    [SerializeField]
    private bool isTutorialScreen = false;

    [Header("Application Screens")]
    public GameObject mainSection;
    public GameObject tutorialSection;

    [Header("TutorialScreen")]
    public GameObject type1Section;
    public GameObject type2Section;
    public GameObject type3Section;
    public GameObject type4Section;
    public GameObject type5Section;

    public GameObject type1SubSectionButtons;
    public GameObject type4SubSectionButtons;

    private bool tutorialType1, tutorialType2, tutorialType3, tutorialType4, tutorialType5;
    private bool tutorialType1_1, tutorialType1_2, tutorialType4_1, tutorialType4_2, tutorialType4_3, tutorialType4_4, tutorialType4_5, tutorialType4_6, tutorialType4_7, tutorialType4_8;
    private bool[] tutorialTypes = new bool[15];
    private bool tutorialButtonClicked = true;
    private bool valueButtonClicked = true;
    public Button valueLettersChangeButton;

    public TextMeshProUGUI tutorialTextPart1;
    public TextMeshProUGUI tutorialTextPart2;
    public TextMeshProUGUI typeDescription;

    public TextMeshProUGUI[] startButtons;
    public Button[] tutorialTypeButtons;

    private Color32 defaultColor = new Color32(255, 255, 255, 255);
    private Color32 selectedColor = new Color32(255, 0, 121, 255);
    private Color32 textColorDefault = new Color32(0, 161, 255, 255);
    private Color32 textColorSelected = new Color32(255, 0, 121, 255);

    public Text titleText;
    public bool isReachedMaxColor = false;

    public static bool isHover = false;
    public static bool isClicking = false;
    public Scene scene;

    [Header("Sum Hand Board")]
    public GameObject handBoard;
    
    [Header("Tutorial Hand Board")]
    public GameObject tutorialHandBoard;

    private bool tutorialHandBoardOpen = false;

    public bool isCorrectRed,
        isCorrectBlue,
        isCorrectGreen,
        isWrongOneRed,
        isWrongOneBlue,
        isWrongOneGreen,
        isWrongTwoRed,
        isWrongTwoBlue,
        isWrongTwoGreen;

    public GameManager gameManager;

    public TextMeshPro answerStatus;
    public TextMeshProUGUI examplesText;
    public bool isExamplesOpen = false;

    //public Image healthBarRed;
    //public float maxHealthBarImageHeight;
    //public float currentHealthBarImageHeight;

    public int sumCount = 0;

    public TextMeshProUGUI[] additionMessageText;
    public TextMeshProUGUI[] subtractionMessageText;
    public TextMeshProUGUI[] multiplicationMessageText;
    public TextMeshProUGUI[] divisionMessageText;
    public TextMeshProUGUI[] equalMessageText;
    
    public bool additionStatus;
    public bool subtractionStatus;
    public bool multiplicationStatus;
    public bool divisionStatus;
    public bool equalStatus;

    public TextMeshProUGUI[] multiplyByText;
    public int multiplyValue;

    public TextMeshProUGUI[] divideByText;
    public int divideValue;

    public Quaternion answerRot;

    public bool sumChanged = false;

    public GameObject[] subLeftPod;
    public GameObject[] subRightPod;

    // Start is called before the first frame update
    void Start()
    {
        //answerCorrectIncorrectStatus.GetComponent<Text>().text = "";

        tutorialType1 = true;
        tutorialType1_1 = true;

        gameManager = GameObject.FindObjectOfType<GameManager>();

        scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene")
        {
            SetButtonColorDefaultTutorial();
        }

        if (scene.name == "GameScene" || scene.name == "Game2Scene" || gameManager.tutorialOfGameScene1 || gameManager.tutorialOfGameScene2)
        {

            tutorialHandBoard.SetActive(false);
            tutorialHandBoardOpen = false;
            
            if(scene.name == "GameScene" || gameManager.tutorialOfGameScene1)
                answerStatus.text = "";
        }

        if(scene.name == "GameScene" || gameManager.tutorialOfGameScene1)
        {
            answerRot = correctAnswer.transform.rotation;
        }

        //currentHealthBarImageHeight = maxHealthBarImageHeight;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene")
        {
            ScreenSwitch(isMainScreen, isGameScreen, isTutorialScreen);

            if (isTutorialScreen && scene.name != "Game2TutorialScene")
            {
                SwitchTutorialTypes();
                SetTutorialTextType();
                SetTutorialBoolList(tutorialType1, tutorialType2, tutorialType3, tutorialType4, tutorialType5,
                    tutorialType1_1, tutorialType1_2, tutorialType4_1, tutorialType4_2, tutorialType4_3,
                    tutorialType4_4, tutorialType4_5, tutorialType4_6, tutorialType4_7, tutorialType4_8);
                if (tutorialButtonClicked)
                {
                    ChangeSelectedButtonColor();
                }
            }
        }
        else if (scene.name == "GameScene" || scene.name == "Game2Scene" || scene.name == "GameTutorialScene" || scene.name == "Game2TutorialScene")
        {
            isMainScreen = false;
            isGameScreen = true;
            ScreenSwitch(isMainScreen, isGameScreen, isTutorialScreen);


            if (scene.name != "Game2TutorialScene" && scene.name != "Game2Scene")
            {
                SwitchTutorialTypes();
                SetTutorialTextType();
                SetTutorialBoolList(tutorialType1, tutorialType2, tutorialType3, tutorialType4, tutorialType5,
                    tutorialType1_1, tutorialType1_2, tutorialType4_1, tutorialType4_2, tutorialType4_3,
                    tutorialType4_4, tutorialType4_5, tutorialType4_6, tutorialType4_7, tutorialType4_8);


                if (tutorialButtonClicked)
                {
                    ChangeSelectedButtonColor();
                }

                if (scene.name == "GameScene" || gameManager.tutorialOfGameScene1)
                {
                    if (isExamplesOpen)
                    {
                        examplesText.text = "Hide Examples";
                    }
                    else if (!isExamplesOpen)
                    {
                        examplesText.text = "Show Examples";
                    }
                }
            }
            else if (scene.name == "Game2Scene" || scene.name == "Game2TutorialScene")
            {
                if (scene.name == "Game2TutorialScene")
                {
                    ConstantlyUpdateSimultaneousEquation(0);
                }
                else
                {
                    switch (gameManager.questionNumber)
                    {
                        case 1:
                            ConstantlyUpdateSimultaneousEquation(0);
                            break;
                        case 2:
                            ConstantlyUpdateSimultaneousEquation(1);
                            break;
                        case 3:
                            ConstantlyUpdateSimultaneousEquation(2);
                            break;
                    }
                }
            }

        }

        if ((scene.name == "GameScene" || gameManager.tutorialOfGameScene1))
        {
            if (!gameManager.cubesCreated)
            {
                if (gameManager.player.isOnPressurePlate)
                {
                    gameManager.cubesCreated = true;
                    gameManager.locomotionSystem.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 0;

                    if (gameManager.cubeAnswerObjects[0].GetComponent<BoxCollider>().enabled == false)
                    {
                        foreach (var cube in gameManager.cubeAnswerObjects)
                        {
                            cube.GetComponent<BoxCollider>().enabled = true;
                        }
                    }

                    CreationOfSimultaneousEquations(a, b, c, simultaneousEquation1);
                    CreationOfSimultaneousEquations(d, e, f, simultaneousEquation2);

                    if (IsCorrectSum(a, b, c, d, e, f))
                    {
                        randomValue = Random.Range(0, answerLocations.Length);
                        var valueChosen = ValueChosen(randomValue);
                        var lastValue = LastValueLocation(randomValue, valueChosen);

                        //correctAnswer = Instantiate(answerTextPrefab, answerLocations[randomValue].transform.position, answerRot, handBoard.transform);
                        //wrongAnswer1 = Instantiate(answerTextPrefab, answerLocations[valueChosen].transform.position, answerRot, handBoard.transform);
                        //wrongAnswer2 = Instantiate(answerTextPrefab, answerLocations[lastValue].transform.position, answerRot, handBoard.transform);

                        correctAnswer.name = "Correct Answer";
                        wrongAnswer1.name = "Wrong Answer 1";
                        wrongAnswer2.name = "Wrong Answer 2";

                        correctAnswer.transform.position = answerLocations[randomValue].transform.position;
                        wrongAnswer1.transform.position = answerLocations[valueChosen].transform.position;
                        wrongAnswer2.transform.position = answerLocations[lastValue].transform.position;

                        //correctAnswer.transform.Rotate(0, handBoard.transform.rotation.y-90, 0);
                        //wrongAnswer1.transform.Rotate(0, handBoard.transform.rotation.y - 90, 0);
                        //wrongAnswer2.transform.Rotate(0, handBoard.transform.rotation.y - 90, 0);

                        var randomColor = Random.Range(0, 3);
                        switch (randomColor)
                        {
                            case 0:
                                correctAnswer.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                isCorrectRed = true;
                                var randomNextColorCase0 = Random.Range(0, 2);
                                switch (randomNextColorCase0)
                                {
                                    case 0:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                        isWrongOneGreen = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                        isWrongTwoBlue = true;
                                        break;
                                    case 1:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                        isWrongOneBlue = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                        isWrongTwoGreen = true;
                                        break;
                                }

                                break;
                            case 1:
                                correctAnswer.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                isCorrectGreen = true;
                                var randomNextColorCase1 = Random.Range(0, 2);
                                switch (randomNextColorCase1)
                                {
                                    case 0:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                        isWrongOneRed = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                        isWrongTwoBlue = true;
                                        break;
                                    case 1:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                        isWrongOneBlue = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                        isWrongTwoRed = true;
                                        break;
                                }

                                break;
                            case 2:
                                correctAnswer.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                isCorrectBlue = true;
                                var randomNextColorCase2 = Random.Range(0, 2);
                                switch (randomNextColorCase2)
                                {
                                    case 0:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                        isWrongOneGreen = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                        isWrongTwoRed = true;
                                        break;
                                    case 1:
                                        wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                        isWrongOneRed = true;
                                        wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                        isWrongTwoGreen = true;
                                        break;
                                }

                                break;

                        }
                        WorkingOutSum(a, b, c, d, e, f);
                    }
                    else
                    {
                        sumCalculated = false;

                    }
                }
                else
                {
                    UpdateHandBoardText();
                }
            }
        }
        else
        {
            WorkingOutSum(a, b, c, d, e, f);
        }

    }

    #region public void SetButtonColorDefaultTutorial()
    public void SetButtonColorDefaultTutorial()
    {
        foreach (var button in tutorialTypeButtons.Select((x, i) => new { x, i }))
        {
            if (button.i < 5)
            {
                button.x.GetComponentInChildren<TextMeshProUGUI>().color = textColorDefault;
            }
            else
            {
                button.x.GetComponentInChildren<TextMeshProUGUI>().color = textColorDefault;
            }
        }
    }
    #endregion

    #region public void SetButtonColorDefaultStart()
    public void SetButtonColorDefaultStart()
    {
        foreach(var button in startButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = textColorDefault;
        }
    }
    #endregion

    #region public void ScreenSwitch(bool isMainScreen, bool isGameScreen, bool isTutorialScreen)
    public void ScreenSwitch(bool isMainScreen, bool isGameScreen, bool isTutorialScreen)
    {
        this.isMainScreen = isMainScreen;
        this.isGameScreen = isGameScreen;
        this.isTutorialScreen = isTutorialScreen;

        if(isMainScreen)
        {
            MainScreen();
        }
        else if(isGameScreen)
        {
            GameScreen();
        }
        else if(isTutorialScreen)
        {
            TutorialScreen();
        }
    }
    #endregion

    #region public void SetTutorialBool(bool type1, bool type2, bool type3, bool type4, bool type 5)
    public void SetTutorialBool(bool type1, bool type2, bool type3, bool type4, bool type5)
    {
        tutorialType1 = type1;
        tutorialType2 = type2;
        tutorialType3 = type3;
        tutorialType4 = type4;
        tutorialType5 = type5;
    }
    #endregion

    #region public void SetTutorialSubSectionBool(bool type 1_1, bool type1_2)
    public void SetTutorialSubSectionBool(bool type1_1, bool type1_2)
    {
        tutorialType1_1 = type1_1;
        tutorialType1_2 = type1_2;
    }
    #endregion

    #region public void SetTutorialSubSectionBool(bool type4_1, bool type4_2, bool type4_3, bool type4_4, bool type4_5, bool type4_6, bool type4_7, bool type4_8)
    public void SetTutorialSubSectionBool(bool type4_1, bool type4_2, bool type4_3, bool type4_4, bool type4_5, bool type4_6, bool type4_7, bool type4_8)
    {
        tutorialType4_1 = type4_1;
        tutorialType4_2 = type4_2;
        tutorialType4_3 = type4_3;
        tutorialType4_4 = type4_4;
        tutorialType4_5 = type4_5;
        tutorialType4_6 = type4_6;
        tutorialType4_7 = type4_7;
        tutorialType4_8 = type4_8;
    }
    #endregion

    #region public void SetTutorialBoolList(bool type1, bool type2, bool type3, bool type4, bool type5, bool type1_1, bool type1_2, bool type4_1, bool type4_2, bool type4_3, bool type4_4, bool type4_5, bool type4_6, bool type4_7, bool type4_8)
    public void SetTutorialBoolList(bool type1, bool type2, bool type3, bool type4, bool type5, bool type1_1, bool type1_2, bool type4_1, bool type4_2, bool type4_3, bool type4_4, bool type4_5, bool type4_6, bool type4_7, bool type4_8)
    {
        tutorialTypes[0] = type1;
        tutorialTypes[1] = type2;
        tutorialTypes[2] = type3;
        tutorialTypes[3] = type4;
        tutorialTypes[4] = type5;
        tutorialTypes[5] = type1_1;
        tutorialTypes[6] = type1_2;
        tutorialTypes[7] = type4_1;
        tutorialTypes[8] = type4_2;
        tutorialTypes[9] = type4_3;
        tutorialTypes[10] = type4_4;
        tutorialTypes[11] = type4_5;
        tutorialTypes[12] = type4_6;
        tutorialTypes[13] = type4_7;
        tutorialTypes[14] = type4_8;
    }
    #endregion

    #region public void SwitchTutorialTypes()
    public void SwitchTutorialTypes()
    {
        type1Section.SetActive(tutorialType1);
        type2Section.SetActive(tutorialType2);
        type3Section.SetActive(tutorialType3);
        type4Section.SetActive(tutorialType4);
        type5Section.SetActive(tutorialType5);

        type1SubSectionButtons.SetActive(tutorialType1);
        type4SubSectionButtons.SetActive(tutorialType4);
    }
    #endregion

    #region public void ChangeSelectedButtonColor()
    public void ChangeSelectedButtonColor()
    {
        buttonClicked = false;

        if (!isHover)
        {
            foreach (var button in tutorialTypeButtons.Select((x, i) => new { x, i }))
            {
                if (button.i < 5)
                {
                    button.x.GetComponentInChildren<TextMeshProUGUI>().color = textColorDefault;
                    //button.x.GetComponentInChildren<Text>().fontSize = 20;
                }
                else
                {
                    button.x.GetComponentInChildren<TextMeshProUGUI>().color = textColorDefault;
                    //button.x.GetComponentInChildren<Text>().fontSize = 14;
                }
            }
        }

        //x is the value, i is the index
        foreach (var typeBool in tutorialTypes.Select((x, i) => new { x, i }))
        {
            if (typeBool.i < 5)
            {
                SetColorButtons(typeBool.x, tutorialTypeButtons, typeBool.i, selectedColor, textColorSelected, 26);
            }
            else
            {
                SetColorButtons(typeBool.x, tutorialTypeButtons, typeBool.i, selectedColor, textColorSelected, 18);
            }
        }
    }
    #endregion

    #region public void SetColorButtons(bool selectedButton Button[] button, int index, Color32 selectedColor, Color32 selectedTextColor, int fontSize)
    public void SetColorButtons(bool selectedButton, Button[] button, int index, Color32 selectedColor, Color32 selectedTextColor, int fontSize)
    {
        if(selectedButton)
        {
            //button[index].GetComponent<Image>().color = selectedColor;
            button[index].GetComponentInChildren<TextMeshProUGUI>().color = selectedTextColor;
            //button[index].GetComponentInChildren<Text>().fontSize = fontSize;
        }
    }
    #endregion

    #region public void SetTutorialTextType()
    public void SetTutorialTextType()
    {
        if (tutorialType1)
        {
            if (tutorialType1_1)
            {
                typeDescription.text = "Coefficients of x are the same in both equations and they have different signs (one positive, one negative)";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->   2x + 3y = 5\n" +
                     "Equation 2 -> -2x + 5y = 7\n\n" +
                     "Notice that both x coefficients are the <color=white>same value but have different signs</color>\n" +
                     "Then, the next step is: \n\nEquation 1 + Equation 2\n\n" +
                     "Then, we have something like this:\n\n" +
                     "(2 - 2)x + (3 + 5)y = 5 + 7\n\n" +
                     "Then adding everything together, we come to the following result:\n\n" +
                     "0x + 8y = 12\n\n" +
                     "We usually ignore x or y when their respective coefficient is 0\n" +
                     "Then, the new equation is:\n\n" +
                     "8y = 12\n\n";

                tutorialTextPart2.text =
                    "Then, we divide by 8 both sides and we find <color=white>y</color>\n\n" +
                     "y = 12 / 8 which when simplified, it becomes:\n\n" +
                     "<color=white>y = 4 / 3</color>\n\n" +
                     "We can now find x\n" +
                     "Therefore, choose any equation you want (I tend to avoid equations with a lot of negative signs in it\nTherefore, I will choose Equation 1)\n\n" +
                     "Equation 1 -> 2x + 3( 4 / 3 )  = 5\n" +
                     "\t        -> 2x + 4\t      = 5\n" +
                     "\t        -> 2x\t\t      = 5 - 4\n" +
                     "\t        -> 2x\t\t      = 1\n" +
                     "\t        ->  x\t\t      = 1 / 2\n" +
                     "Therefore:\n\n" +
                     "<color=white>x = 1 / 2</color>";

            }
            else if (tutorialType1_2)
            {
                typeDescription.text = "Coefficients of y are the same in both equations and they have different signs (one positive, one negative)";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->   4x + 3y = 15\n" +
                     "Equation 2 ->  3x - 3y = 6\n\n" +
                     "Notice that both y coefficients are the <color=white>same value but have different signs</color>\n" +
                     "Then, the next step is: \n\nEquation 1 + Equation 2\n\n" +
                     "Then, we have something like this:\n\n" +
                     "(4 + 3)x + (3 - 3)y = 15 + 6\n\n" +
                     "Then adding everything together, we come to the following result:\n\n" +
                     "7x + 0y = 21\n\n" +
                     "We usually ignore x or y when their respective coefficient is 0\n" +
                     "Then, the new equation is:\n\n" +
                     "7x = 21\n\n";

                tutorialTextPart2.text =
                    "Then, we divide by 7 both sides and we find <color=white>x</color>\n\n" +
                     "x = 21 / 7 which when simplified, it becomes:\n\n" +
                     "<color=white>x = 3</color>\n\n" +
                     "We can now find y\n" +
                     "Therefore, choose any equation you want (I tend to avoid equations with a lot of negative signs in it\nTherefore, I will choose Equation 1)\n\n" +
                     "Equation 1 -> 4( 3 ) + 3y  = 15\n" +
                     "\t        -> 12 + 3y\t = 15\n" +
                     "\t        -> 3y\t\t = 15 - 12\n" +
                     "\t        -> 3y\t\t = 3\n" +
                     "\t        ->  y\t\t = 1\n" +
                     "Therefore:\n\n" +
                     "<color=white>y = 1</color>";
            }
        }
        else if (tutorialType2)
        {
            typeDescription.text = "Coefficients of x are the same in both equations BUT they have same signs (both positive or both negative)";
            tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->   2x + 5y = 10\n" +
                     "Equation 2 ->  2x + 4y = 5\n\n" +
                     "Notice that both x coefficients are the <color=white>same value and have the same sign</color>\n" +
                     "Then, the next step is: \n\nEquation 1 - Equation 2\n\n" +
                     "Then, we have something like this:\n\n" +
                     "(2 - 2)x + (5 - 4)y = 10 - 5\n\n" +
                     "Then subtracting everything together, we come to the following result:\n\n" +
                     "0x + 1y = 5\n\n" +
                     "We usually ignore x or y when their respective coefficient is 0\n" +
                     "Then, the new equation is:\n\n" +
                     "1y = 5\n\n";

            tutorialTextPart2.text =
                "Then, we divide by 1 both sides and we find <color=white>y</color>\n\n" +
                 "y = 5 / 1 which when simplified, it becomes:\n\n" +
                 "<color=white>y = 5</color>\n\n" +
                 "We can now find x\n" +
                 "Therefore, choose any equation you want (I tend to avoid equations with a lot of negative signs in it)\n\n" +
                 "Equation 1 -> 2x + 5 ( 5 )\t = 10\n" +
                 "\t        -> 2x + 25\t = 10\n" +
                 "\t        -> 2x\t\t = 10 - 25\n" +
                 "\t        -> 2x\t\t = -15\n" +
                 "\t        ->  x\t\t = -15 / 2\n" +
                 "Therefore:\n\n" +
                 "<color=white>x = -15 / 2</color>";
        }
        else if (tutorialType3)
        {
            typeDescription.text = "Coefficients of y are the same in both equations BUT they have same signs (both positive or both negative)";
            tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->   7x + 3y = 15\n" +
                     "Equation 2 ->  3x + 3y = 8\n\n" +
                     "Notice that both y coefficients are the <color=white>same value and have the same sign</color>\n" +
                     "Then, the next step is: \n\nEquation 1 - Equation 2\n\n" +
                     "Then, we have something like this:\n\n" +
                     "(7 - 3)x + (3 - 3)y = 15 - 8\n\n" +
                     "Then subtracting everything together, we come to the following result:\n\n" +
                     "4x + 0y = 7\n\n" +
                     "We usually ignore x or y when their respective coefficient is 0\n" +
                     "Then, the new equation is:\n\n" +
                     "4x = 7\n\n";

            tutorialTextPart2.text =
                "Then, we divide by 4 both sides and we find <color=white>x</color>\n\n" +
                 "<color=white>x = 7 / 4</color>\n\n" +
                 "We can now find y\n" +
                 "Therefore, choose any equation you want (I tend to avoid equations with a lot of negative signs in it)\n\n" +
                 "Equation 1 -> 7( 7 / 4 ) + 3y  = 15\n" +
                 "\t        -> 49 / 4 + 3y\t      = 15\t[Multiply by 4 both sides]\n" +
                 "\t        -> 49 + 12y\t      = 60\n" +
                 "\t        -> 12y\t\t      = 60 - 49\n" +
                 "\t        -> 12y\t\t      = 11\n" +
                 "\t        ->     y\t\t      = 11 / 12\n" +
                 "Therefore:\n\n" +
                 "<color=white>y = 11 / 12</color>";

        }
        else if (tutorialType4)
        {
            if (tutorialType4_1)
            {
                typeDescription.text = "Coefficient of x in Equation 1 is larger than that of Equation 2, even when ignoring the sign. Coefficients of x are divisors meaning that\n" +
                    "one coefficient of x divide by the other will leave no remainder. Also, coefficient of x in Equation 2 is negative while the other is positive";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->   4x + 7y = 2\n" +
                     "Equation 2 ->  2x + 5y = 10\n\n" +
                     "Firstly, we need to find which coefficient of x is larger in both equations\n" +
                     "In this case, the coefficient of x in Equation 2 is smaller\n" +
                     "Then, we need to check if somehow, we can get the coefficient of x in Equation 2 equal to the coefficient of x in Equation 1\n" +
                     "Since 4 is equal to 2 times 2, then we need to multiply by 2 across Equation 2, as follows:\n\n" +
                     "<color=white>2 x (Equation 2) -> 4x + 10y = 20</color>\n\n" +
                     "Let us <color=white>assume</color> that the <color=white>new equation</color> that we just obtained is <color=white>Equation 3</color>\n" +
                     "Therefore, since both x coefficients match in both equations, we can proceed with the next step\n" +
                     "Let us rewrite the equations\n\n" +
                     "Equation 1 ->  4x +  7y = 2\n" +
                     "Equation 3 -> 4x + 10y = 20\n";
                
                tutorialTextPart2.text =
                    "Now we can eliminate the coefficient of x by doing the following:\n\n" +
                    "<color=white>Equation 1 - Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 4 - 4 )x + ( 7 - 10 )y = 2 - 20\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "0x - 3y = -18\n" +
                    "We ignore x since it is 0, therefore we can find <color=white>y</color>\n" +
                    "- 3y = -18, therefore\n\n" +
                    "<color=white>y = 6</color>\n\n" +
                    "Now we can find <color=white>x</color> by choosing any equation and applying the newly found value of y into the equation\n" +
                    "Equation 1 -> 4x + 7( 6 ) = 2\n" +
                    "\t        -> 4x + 42      = 2\n" +
                    "\t        -> 4x\t          = 2 - 42\n" +
                    "\t        -> 4x\t          = -40\n" +
                    "\t        ->  x\t          = -10\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = -10</color>";
            }
            else if (tutorialType4_2)
            {
                typeDescription.text = "Coefficient of x in Equation 1 is larger than that of Equation 2, but when ignoring the sign, the coefficient of x in Equation 2 is larger.\n" +
                    "Coefficients of x are divisors meaning that one coefficient of x divide by the other will leave no remainder. Also, coefficient of x in Equation 2 is negative while the other is positive";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->    4x + 5y = 4\n" +
                     "Equation 2 ->  -8x + 3y = 6\n\n" +
                     "Firstly, we need to find which coefficient of x is larger in both equations\n" +
                     "In this case, the coefficient of x in Equation 2 is smaller\n" +
                     "<color=white>BUT</color> we cannot multiply Equation 2 because if we remove the negative out of the coefficient of x\n" +
                     "Then the coefficient of x in Equation 2 will be larger than the one in Equation 1\n" +
                     "Therefore, we must use Equation 1 and multiply it by some value so that the coefficient of x in Equation 1 matches the one in Equation 2\n" +
                     "In order for the coefficient of x in Equation 1 to become like the coefficient of x in Equation 2, we must multiply Equation 1 by 2\n" +
                     "Therefore:\n" +
                     "<color=white>2 x ( Equation 1 )</color> so if we simplify it, it will become as follows:\n" +
                     "2 x ( Equation 1 ) -> 8x + 10y = 8\n" +
                     "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                     "<color=white>Equation 3 -> 8x + 10y = 8</color>\n\n" +
                     "Now we can carry on normally with solving this simultaneous equation\n" +
                     "Let us rewrite both equations:\n\n" +
                     "Equation 3 ->    8x + 10y = 8\n" +
                     "Equation 2 ->   -8x +  3y = 6";

                tutorialTextPart2.text =
                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 3 + Equation 2</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 8 - 8 )x + ( 10 + 3 )y = 8 + 6\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "0x + 13y = 14\n" +
                    "We ignore x since it is 0, therefore we can find <color=white>y</color>\n" +
                    "13y = 14, therefore\n\n" +
                    "<color=white>y = 14 / 13</color>\n\n" +
                    "Now we can find <color=white>x</color> by choosing any equation and applying the newly found value of y into the equation\n" +
                    "Equation 1 ->   4x + 5( 14 / 13 ) = 4\n" +
                    "\t        ->   4x + (70 / 13)\t = 4  [ Multiply by 13 on both sides ]\n" +
                    "\t        -> 52x + 70\t\t = 52\n" +
                    "\t        -> 52x\t\t\t = 52 - 70\n" +
                    "\t        -> 52x\t\t\t = -18\n" +
                    "\t        ->     x\t\t\t = -18 / 52\n" +
                    "\t        ->     x\t\t\t =  -9 / 26\n"+
                    "Therefore:\n\n" +
                    "<color=white>x = -9 / 26</color>";
            }
            else if (tutorialType4_3)
            {
                typeDescription.text = "Coefficient of x in Equation 1 is smaller than that of Equation 2, even when ignoring the sign. Coefficients of x are divisors meaning that\n" +
                    "one coefficient of x divide by the other will leave no remainder. Also, coefficient of x in Equation 1 is negative while the other is positive";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->  -2x + 7y = 1\n" +
                     "Equation 2 ->   4x + 4y = 5\n\n" +
                     "Firstly, we need to find which coefficient of x is larger in both equations\n" +
                     "In this case, the coefficient of x in Equation 1 is smaller\n" +
                     "<color=white>AND</color> when we ignore the sign of the coefficient of x in both equations,Equation 1 still has the smaller coefficient of x\n" +
                     "Therefore, we must use Equation 1 and multiply it by some value so that the coefficient of x in Equation 1 matches the one in Equation 2\n" +
                     "In order for the coefficient of x in Equation 1 to become like the coefficient of x in Equation 2, we must multiply Equation 1 by 2\n" +
                     "Therefore:\n" +
                     "<color=white>2 x ( Equation 1 )</color> so if we simplify it, it will become as follows:\n" +
                     "2 x ( Equation 1 ) -> -4x + 14y = 2\n" +
                     "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                     "<color=white>Equation 3 -> -4x + 14y = 2</color>\n\n" +
                     "Now we can carry on normally with solving this simultaneous equation\n" +
                     "Let us rewrite both equations:\n\n" +
                     "Equation 3 ->   -4x + 14y = 2\n" +
                     "Equation 2 ->     4x +  4y = 5";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 3 + Equation 2</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( -4 + 4 )x + ( 14 + 4 )y = 2 + 5\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "0x + 18y = 7\n" +
                    "We ignore x since it is 0, therefore we can find <color=white>y</color>\n" +
                    "18y = 7, therefore\n\n" +
                    "<color=white>y = 7 / 18</color>\n\n" +
                    "Now we can find <color=white>x</color> by choosing any equation and applying the newly found value of y into the equation\n" +
                    "Equation 2  ->   4x + 4( 7 / 18 )\t= 5\n" +
                    "\t         ->   4x + (28 / 18)\t= 5  \n" +
                    "\t         ->   4x + ( 14 / 9 )\t= 5 \t[ Multiply by 9 on both sides ]\n" +
                    "\t         -> 36x + 14\t\t= 45\n" +
                    "\t         -> 36x\t\t\t= 45 - 14\n" +
                    "\t         -> 36x\t\t\t= 31\n" +
                    "\t         ->     x\t\t\t=  31 / 36\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = -9 / 26</color>";
            }
            else if (tutorialType4_4)
            {
                typeDescription.text = "Coefficient of x in Equation 1 is smaller than that of Equation 2, but when ignoring the sign, the coefficient of x in Equation 1 is larger.\n" +
                    "Coefficients of x are divisors meaning that one coefficient of x divide by the other will leave no remainder. Also, coefficient of x in Equation 1 is negative while the other is positive";
                tutorialTextPart1.text =
                      "Consider the following examples with numbers:\n\n" +
                      "Equation 1 ->  -9x + 2y = 4\n" +
                      "Equation 2 ->   3x + 5y = 3\n\n" +
                      "Firstly, we need to find which coefficient of x is larger in both equations\n" +
                      "In this case, the coefficient of x in Equation 1 is smaller\n" +
                      "<color=white>BUT</color> when we ignore the sign of the coefficient of x in both equations,Equation 1 has the larger coefficient of x\n" +
                      "Therefore, we must use Equation 2 and multiply it by some value so that the coefficient of x in Equation 2 matches the one in Equation 1\n" +
                      "In order for the coefficient of x in Equation 2 to become like the coefficient of x in Equation 1, we must multiply Equation 2 by 3\n" +
                      "Therefore:\n" +
                      "<color=white>3 x ( Equation 2 )</color> so if we simplify it, it will become as follows:\n" +
                      "3 x ( Equation 2 ) ->  9x +  15y = 9\n" +
                      "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                      "<color=white>Equation 3 -> 9x + 15y = 9</color>\n\n" +
                      "Now we can carry on normally with solving this simultaneous equation\n" +
                      "Let us rewrite both equations:\n\n" +
                      "Equation 1 ->\t-9x +  2y = 4\n" +
                      "Equation 3 ->\t 9x + 15y = 9";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 1 + Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( -9 + 9 )x + ( 2 + 15 )y = 4 + 9\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "0x + 17y = 13\n" +
                    "We ignore x since it is 0, therefore we can find <color=white>y</color>\n" +
                    "17y = 13, therefore\n\n" +
                    "<color=white>y = 13 / 17</color>\n\n" +
                    "Now we can find <color=white>x</color> by choosing any equation and applying the newly found value of y into the equation\n" +
                    "Equation 2  ->   3x + 4( 13 / 17 )\t = 3\n" +
                    "\t         ->   3x + ( 52 / 17 )\t\t = 3 \t[ Multiply by 17 both sides ]\n " +
                    "\t         -> 51x + 52\t\t\t = 51  \n" +
                    "\t         -> 51x   \t\t\t = 51 - 52 \n" +
                    "\t         -> 51x   \t\t\t = -1\n" +
                    "\t         ->     x\t\t\t\t = -1 / 51\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = -1 / 51</color>";
            }
            else if (tutorialType4_5)
            {
                typeDescription.text = "Coefficient of y in Equation 1 is larger than that of Equation 2, even when ignoring the sign. Coefficients of y are divisors meaning that\n" +
                   "one coefficient of y divide by the other will leave no remainder. Also, coefficient of y in Equation 2 is negative while the other is positive";
                tutorialTextPart1.text =
                     "Consider the following examples with numbers:\n\n" +
                     "Equation 1 ->\t2x + 4y = 9\n" +
                     "Equation 2 ->\t3x - 2y  = 8\n\n" +
                     "Firstly, we need to find which coefficient of y is larger in both equations\n" +
                     "In this case, the coefficient of y in Equation 2 is smaller\n" +
                     "<color=white>AND</color> when we ignore the sign of the coefficient of y in both equations,Equation 1 still remains the larger coefficient of y\n" +
                     "Therefore, we must use Equation 2 and multiply it by some value so that the coefficient of y in Equation 2 matches the one in Equation 1\n" +
                     "In order for the coefficient of y in Equation 2 to become like the coefficient of y in Equation 1, we must multiply Equation 2 by 2\n" +
                     "Therefore:\n" +
                     "<color=white>2 x ( Equation 2 )</color> so if we simplify it, it will become as follows:\n" +
                     "2 x ( Equation 2 ) ->  6x -  4y = 16\n" +
                     "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                     "<color=white>Equation 3 -> 6x - 4y = 16</color>\n\n" +
                     "Now we can carry on normally with solving this simultaneous equation\n" +
                     "Let us rewrite both equations:\n\n" +
                     "Equation 1 ->\t 2x + 4y =  9\n" +
                     "Equation 3 ->\t 6x - 4y = 16";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 1 + Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 2 + 6 )x + ( 4 - 4 )y = 9 + 16\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "8x + 0y = 25\n" +
                    "We ignore y since it is 0, therefore we can find <color=white>x</color>\n" +
                    "8x = 25, therefore\n\n" +
                    "<color=white>x = 25 / 8</color>\n\n" +
                    "Now we can find <color=white>y</color> by choosing any equation and applying the newly found value of x into the equation\n" +
                    "Equation 1  ->  2x + 4( 25 / 8 )\t= 9 \n" +
                    "\t         ->  2x + 100 / 8\t= 9  \n" +
                    "\t         ->  2x + 25 / 2 \t= 9 \t[ Multiply by 2 on both sides ]\n" +
                    "\t         ->  4x + 25  \t\t= 18\n" +
                    "\t         ->  4x\t\t\t= 18 - 25\n" +
                    "\t         ->  4x\t\t\t= -7\n" +
                    "\t         ->    x\t\t\t= -7 / 4\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = -7 / 4</color>";
            }
            else if (tutorialType4_6)
            {
                typeDescription.text = "Coefficient of y in Equation 1 is larger than that of Equation 2, but when ignoring the sign, the coefficient of y in Equation 2 is larger.\n" +
                    "Coefficients of y are divisors meaning that one coefficient of y divide by the other will leave no remainder. Also, coefficient of y in Equation 2 is negative while the other is positive";
                tutorialTextPart1.text =
                      "Consider the following examples with numbers:\n\n" +
                      "Equation 1 ->\t2x + 4y = 9\n" +
                      "Equation 2 ->\t3x - 8y  = 8\n\n" +
                      "Firstly, we need to find which coefficient of y is larger in both equations\n" +
                      "In this case, the coefficient of y in Equation 2 is smaller\n" +
                      "<color=white>BUT</color> when we ignore the sign of the coefficient of y in both equations,Equation 2 has the larger coefficient of y\n" +
                      "Therefore, we must use Equation 1 and multiply it by some value so that the coefficient of y in Equation 1 matches the one in Equation 2\n" +
                      "In order for the coefficient of y in Equation 1 to become like the coefficient of y in Equation 2, we must multiply Equation 1 by 2\n" +
                      "Therefore:\n" +
                      "<color=white>2 x ( Equation 1 )</color> so if we simplify it, it will become as follows:\n" +
                      "2 x ( Equation 1 ) ->  4x +  8y = 18\n" +
                      "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                      "<color=white>Equation 3 -> 4x + 8y = 18</color>\n\n" +
                      "Now we can carry on normally with solving this simultaneous equation\n" +
                      "Let us rewrite both equations:\n\n" +
                      "Equation 2 ->\t 3x  - 8y =  8\n" +
                      "Equation 3 ->\t 4x + 8y = 18";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 2 + Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 3 + 4 )x + ( -8 + 8 )y = 8 + 18\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "7x + 0y = 26\n" +
                    "We ignore y since it is 0, therefore we can find <color=white>x</color>\n" +
                    "7x = 26, therefore\n\n" +
                    "<color=white>x = 26 / 7</color>\n\n" +
                    "Now we can find <color=white>y</color> by choosing any equation and applying the newly found value of x into the equation\n" +
                    "Equation 1  ->    2x + 4( 26 / 7 )\t= 9 \n" +
                    "\t         ->    2x + 104 / 7\t= 9 \t[ Multiply by 7 on both sides ] \n" +
                    "\t         ->  14x + 104 \t= 63 \n" +
                    "\t         ->  14x \t\t= 63 - 104\n" +
                    "\t         ->  14x\t\t= - 41\n" +
                    "\t         ->      x\t\t= - 41 / 14\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = - 41 / 14</color>";
            }
            else if (tutorialType4_7)
            {
                typeDescription.text = "Coefficient of y in Equation 1 is smaller than that of Equation 2, even when ignoring the sign. Coefficients of y are divisors meaning that\n" +
                   "one coefficient of y divide by the other will leave no remainder. Also, coefficient of y in Equation 1 is negative while the other is positive";
                tutorialTextPart1.text =
                      "Consider the following examples with numbers:\n\n" +
                      "Equation 1 ->\t2x -   3y  = 9\n" +
                      "Equation 2 ->\t3x + 12y  = 8\n\n" +
                      "Firstly, we need to find which coefficient of y is larger in both equations\n" +
                      "In this case, the coefficient of y in Equation 1 is smaller\n" +
                      "<color=white>AND</color> when we ignore the sign of the coefficient of y in both equations,Equation 2 still has the larger coefficient of y\n" +
                      "Therefore, we must use Equation 1 and multiply it by some value so that the coefficient of y in Equation 1 matches the one in Equation 2\n" +
                      "In order for the coefficient of y in Equation 1 to become like the coefficient of y in Equation 2, we must multiply Equation 1 by 4\n" +
                      "Therefore:\n" +
                      "<color=white>4 x ( Equation 1 )</color> so if we simplify it, it will become as follows:\n" +
                      "4 x ( Equation 1 ) ->  8x -  12y = 36\n" +
                      "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                      "<color=white>Equation 3 -> 8x - 12y = 36</color>\n\n" +
                      "Now we can carry on normally with solving this simultaneous equation\n" +
                      "Let us rewrite both equations:\n\n" +
                      "Equation 2 ->\t 3x + 12y =  8\n" +
                      "Equation 3 ->\t 8x - 12y = 36";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 2 + Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 3 + 8 )x + ( 12 - 12 )y = 8 + 36\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "11x + 0y = 44\n" +
                    "We ignore y since it is 0, therefore we can find <color=white>x</color>\n" +
                    "11x = 44 thus x = 44 / 11, therefore\n\n" +
                    "<color=white>x = 4</color>\n\n" +
                    "Now we can find <color=white>y</color> by choosing any equation and applying the newly found value of x into the equation\n" +
                    "Equation 2  ->  3x + 12( 4 )\t= 8 \n" +
                    "\t         ->  3x + 48\t\t= 8\n" +
                    "\t         ->  3x \t\t\t= 8 - 48 \n" +
                    "\t         ->  3x \t\t\t= -40\n" +
                    "\t         ->    x\t\t\t= - 40 / 3\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = - 40 / 3</color>";
            }
            else if (tutorialType4_8)
            {
                typeDescription.text = "Coefficient of y in Equation 1 is smaller than that of Equation 2, but when ignoring the sign, the coefficient of y in Equation 1 is larger.\n" +
                    "Coefficients of y are divisors meaning that one coefficient of y divide by the other will leave no remainder. Also, coefficient of y in Equation 1 is negative while the other is positive";
                tutorialTextPart1.text =
                      "Consider the following examples with numbers:\n\n" +
                      "Equation 1 ->\t2x -  8y  = 10\n" +
                      "Equation 2 ->\t3x + 2y  = 4\n\n" +
                      "Firstly, we need to find which coefficient of y is larger in both equations\n" +
                      "In this case, the coefficient of y in Equation 1 is smaller\n" +
                      "<color=white>BUT</color> when we ignore the sign of the coefficient of y in both equations,Equation 1 has the larger coefficient of y\n" +
                      "Therefore, we must use Equation 2 and multiply it by some value so that the coefficient of y in Equation 2 matches the one in Equation 1\n" +
                      "In order for the coefficient of y in Equation 2 to become like the coefficient of y in Equation 1, we must multiply Equation 2 by 4\n" +
                      "Therefore:\n" +
                      "<color=white>4 x ( Equation 2 )</color> so if we simplify it, it will become as follows:\n" +
                      "4 x ( Equation 2 ) ->  12x + 8y = 16\n" +
                      "Let us <color=white>assume</color> that the <color=white>new equation</color> is <color=white>Equation 3</color>, therefore:\n\n" +
                      "<color=white>Equation 3 -> 12x + 8y = 16</color>\n\n" +
                      "Now we can carry on normally with solving this simultaneous equation\n" +
                      "Let us rewrite both equations:\n\n" +
                      "Equation 1 ->\t  2x -  8y  = 10\n" +
                      "Equation 3 ->\t 12x +  8y = 16";

                tutorialTextPart2.text =

                    "Then, we do the following:\n\n" +
                    "<color=white>Equation 1 + Equation 3</color>\n\n" +
                    "Therefore, we should have something as follows:\n" +
                    "( 2 + 12 )x + ( -8 + 8 )y = 10 + 16\n" +
                    "If we simplify it, we should have something like as follows:\n" +
                    "14x + 0y = 26\n" +
                    "We ignore y since it is 0, therefore we can find <color=white>x</color>\n" +
                    "14x = 26 thus x = 26 / 14, therefore\n\n" +
                    "<color=white>x = 13 / 7</color>\n\n" +
                    "Now we can find <color=white>y</color> by choosing any equation and applying the newly found value of x into the equation\n" +
                    "Equation 2  ->    3x + 2( 13 / 7 )\t = 4 \n" +
                    "\t         ->    3x + 26 / 7\t = 4 \t[ Multiply by 7 both sides ]\n" +
                    "\t         ->  21x + 26\t\t = 28 \n" +
                    "\t         ->  21x \t\t = 28 - 26\n" +
                    "\t         ->  21x \t\t = 2\n" +
                    "\t         ->      x\t\t =  2 / 21\n" +
                    "Therefore:\n\n" +
                    "<color=white>x = 2 / 21</color>";
            }
        }
        else if (tutorialType5)
        {
            typeDescription.text = "Coefficients of either x or y in both equations do not match and they are not divisors of each other";
            tutorialTextPart1.text =
                      "Consider the following examples with numbers:\n\n" +
                      "Equation 1 ->\t2x + 3y  = 4\n" +
                      "Equation 2 ->\t3x + 2y  = 7\n\n" +
                      "Firstly, we need to identify which coefficient of x or y we are going to get rid of\n" +
                      "But we have an issue here as no values match or are divisors of one another\n" +
                      "In this case, we will multiply both Equation 1 and Equation 2\n" +
                      "Let us choose to get rid of x and <color=white>find y</color> [Note: We could have gotten rid of y and found x, we will still obtain the same answer]\n" +
                      "What we need to do now is basically multiply each Equation with the other Equations coefficient of x\n" +
                      "Therefore:\n\n" +
                      "3 x Equation 1 -> 3 (2x + 3y = 4) -> 6x + 9y = 12 [Assume this is <color=white>Equation 3</color>]\n" +
                      "2 x Equation 2 -> 2 (3x + 2y = 7) -> 6x + 4y = 14 [Assume this is <color=white>Equation 4</color>]\n\n" +
                      "Now, the coefficient of x in both Equations match, therefore we can find <color=white>y</color>\n" +
                      "Therefore, we can do the following:\n\n" +
                      "<color=white>Equation 3 - Equation 4</color>";

            tutorialTextPart2.text =
                "Therefore, we should have something as follows:\n" +
                "( 6 - 6 )x + ( 9 - 4 )y = 12 - 14\n" +
                "If we simplify it, we should have something like as follows:\n" +
                "0x + 5y = -2\n" +
                "We ignore x since it is 0, therefore we can find <color=white>y</color>\n" +
                "5x = -2, therefore\n\n" +
                "<color=white>x = -2 / 5</color>\n\n" +
                "Now we can find <color=white>x</color> by choosing any equation and applying the newly found value of y into the equation\n" +
                "Equation 2  ->    3x + 2( -2 / 5 )\t= 7 \n" +
                "\t         ->    3x - 4 / 5\t= 7 \t[ Multiply by 5 both sides ]\n" +
                "\t         ->  15x - 4\t\t= 35 \n" +
                "\t         ->  15x \t\t= 35 + 4\n" +
                "\t         ->  15x \t\t= 39\n" +
                "\t         ->      x\t\t= 39 / 15\n" +
                "Therefore:\n\n" +
                "<color=white>x = 39 / 15</color>";
        }
    }
    #endregion

    #region public void MainScreen()
    public void MainScreen()
    {
        mainSection.SetActive(true);
        tutorialSection.SetActive(false);
    }
    #endregion

    #region public void GameScreen()
    public void GameScreen()
    {
        if (!sumCalculated)
        {
            sumCalculated = true;
            sumChanged = true;
            switch (gameManager.questionNumber)
            {
                //Tutorial
                case 0:
                    a = 1;
                    b = 1;
                    c = 3;
                    d = 1;
                    e = 2;
                    f = 9;
                    break;
                case 1:
                    a = 1;
                    b = 3;
                    c = 4;
                    d = 1;
                    e = 1;
                    f = 10;
                    break;
                case 2:
                    a = 2;
                    b = 2;
                    c = 8;
                    d = 3;
                    e = 2;
                    f = 21;
                    break;
                case 3:
                    a = 3;
                    b = 5;
                    c = 10;
                    d = 4;
                    e = 3;
                    f = 7;
                    break;
            }

            gameManager.newA = a;
            gameManager.newB = b;
            gameManager.newC = c;
            gameManager.newD = d;
            gameManager.newE = e;
            gameManager.newF = f;

            gameManager.startNewA = a;
            gameManager.startNewB = b;
            gameManager.startNewC = c;
            gameManager.startNewD = d;
            gameManager.startNewE = e;
            gameManager.startNewF = f;

            gameManager.valueSet = true;

            if ((scene.name == "GameScene" || gameManager.tutorialOfGameScene1) && gameManager.player.isOnPressurePlate)
            {
                gameManager.locomotionSystem.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 0;

                CreationOfSimultaneousEquations(a, b, c, simultaneousEquation1);
                CreationOfSimultaneousEquations(d, e, f, simultaneousEquation2);


                if (IsCorrectSum(a, b, c, d, e, f))
                {
                    randomValue = Random.Range(0, answerLocations.Length);
                    var valueChosen = ValueChosen(randomValue);
                    var lastValue = LastValueLocation(randomValue, valueChosen);
                    //correctAnswer = Instantiate(answerTextPrefab, answerLocations[randomValue].transform.position, Quaternion.identity, handBoard.transform);
                    //wrongAnswer1 = Instantiate(answerTextPrefab, answerLocations[valueChosen].transform.position, Quaternion.identity, handBoard.transform);
                    //wrongAnswer2 = Instantiate(answerTextPrefab, answerLocations[lastValue].transform.position, Quaternion.identity, handBoard.transform);

                    correctAnswer.name = "Correct Answer";
                    wrongAnswer1.name = "Wrong Answer 1";
                    wrongAnswer2.name = "Wrong Answer 2";

                    correctAnswer.transform.position = answerLocations[randomValue].transform.position;
                    wrongAnswer1.transform.position = answerLocations[valueChosen].transform.position;
                    wrongAnswer2.transform.position = answerLocations[lastValue].transform.position;

                    var randomColor = Random.Range(0, 3);
                    switch (randomColor)
                    {
                        case 0:
                            correctAnswer.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                            isCorrectRed = true;
                            var randomNextColorCase0 = Random.Range(0, 2);
                            switch (randomNextColorCase0)
                            {
                                case 0:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                    isWrongOneGreen = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                    isWrongTwoBlue = true;
                                    break;
                                case 1:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                    isWrongOneBlue = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                    isWrongTwoGreen = true;
                                    break;
                            }

                            break;
                        case 1:
                            correctAnswer.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                            isCorrectGreen = true;
                            var randomNextColorCase1 = Random.Range(0, 2);
                            switch (randomNextColorCase1)
                            {
                                case 0:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                    isWrongOneRed = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                    isWrongTwoBlue = true;
                                    break;
                                case 1:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                                    isWrongOneBlue = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                    isWrongTwoRed = true;
                                    break;
                            }

                            break;
                        case 2:
                            correctAnswer.GetComponent<TextMeshPro>().color = new Color(0, 0, 1);
                            isCorrectBlue = true;
                            var randomNextColorCase2 = Random.Range(0, 2);
                            switch (randomNextColorCase2)
                            {
                                case 0:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                    isWrongOneGreen = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                    isWrongTwoRed = true;
                                    break;
                                case 1:
                                    wrongAnswer1.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                                    isWrongOneRed = true;
                                    wrongAnswer2.GetComponent<TextMeshPro>().color = new Color(0, 1, 0);
                                    isWrongTwoGreen = true;
                                    break;
                            }

                            break;

                    }

                    WorkingOutSum(a, b, c, d, e, f);
                }
                else
                {
                    sumCalculated = false;
                }
            }
            else
            {
                WorkingOutSum(a, b, c, d, e, f);
            }
        }
    }
    #endregion

    #region public void TutorialScreen()
    public void TutorialScreen()
    {
        mainSection.SetActive(false);
        tutorialSection.SetActive(true);
    }
    #endregion

    #region private void ChangeValueToLettersViceVersa()
    private void ChangeValueToLettersViceVersa()
    {
        if(!valueButtonClicked)
        {
            valueLettersChangeButton.GetComponentInChildren<Text>().text = "See Example with Values";
        }
        else
        {
            valueLettersChangeButton.GetComponentInChildren<Text>().text = "See Example with Letters";
        }
    }
    #endregion

    #region private int ValueRemaining(int randomValue)
    private int ValueChosen(int randomValue)
    {
        switch (randomValue)
        {
            case 0:
                return Random.Range(1, answerLocations.Length);
            case 1:
                while (true)
                {
                    var value = Random.Range(0, answerLocations.Length);
                    if (value != 1)
                    {
                        return value;
                    }
                }
            case 2:
                return Random.Range(0, answerLocations.Length - 1);
        }
        return -1;
    }
    #endregion

    #region private int LastValueLocation(int randomValue, int secondChosenValue)
    private int LastValueLocation(int randomValue, int secondChosenValue)
    {
        if(
            (randomValue == 0 && secondChosenValue == 1)
            ||
            (randomValue == 1 && secondChosenValue == 0)
          )
        {
            return 2;
        }
        else if(
                (randomValue == 1 && secondChosenValue == 2)
                ||
                (randomValue == 2 && secondChosenValue == 1)
               )
        {
            return 0;
        }
        else if(
                (randomValue == 0 && secondChosenValue == 2)
                ||
                (randomValue == 2 && secondChosenValue == 0)
               )
        {
            return 1;
        }

        return -1;
    }
    #endregion

    #region private bool CheckSum()
    bool IsCorrectSum(int a, int b, int c, int d, int e, int f)
    {
        if (
            (a == 0 && b != 0 && d != 0 && e == 0)
            ||
            (a != 0 && b == 0 && d == 0 && e != 0)
          )
        {
            return false;
        }
        if (a == 0 && b == 0)
        {
            return false;
        }

        if (d == 0 && e == 0)
        {
            return false;
        }

        if((a != 0 && b == 0) ||(a == 0 && b != 0))
        {
            return false;
        }

        if ((d != 0 && e == 0) || (d == 0 && e != 0))
        {
            return false;
        }

        return true;
    }
    #endregion

    #region private long MaxValue(int v1, int v2)
    long MaxValue(int v1, int v2)
    {
        if(v1 < v2)
        {
            if(-v1 < v2)
            {
                return v2;
            }
            else if(-v1 > v2)
            {
                return v1;
            }
        }
        else if(v2 > v1)
        {
            if(-v2 < v1)
            {
                return v1;
            }
            else if(-v2 > v1)
            {
                return v2;
            }
        }
        return 0;
    }
    #endregion

    #region private long MultiplyMatch(int value1, int value2)
    bool MultiplyMatch(int value1, int value2)
    {
        var maxValue = MaxValue(value1, value2);

        if (maxValue == value1)
        {
            if (value2 != 0)
            {
                var division = value1 / value2;
                var multiply = value2 * division;
                if (multiply == value1)
                {
                    return true;
                }
            }
        }
        else if(maxValue == value2)
        {
            if (value1 != 0)
            {
                var division = value2 / value1;
                var multiply = value1 * division;
                if (multiply == value2)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    #endregion

    #region private void CreationOfSimultaneousEquations(int v1, int v2, int sum, Text simultaneousEquation)
    void CreationOfSimultaneousEquations(int v1, int v2, int sum, TextMeshPro simultaneousEquation)
    {
        if (v1 == 1 && v2 == 1)
        {
            simultaneousEquation.text = "x " + "+ y = " + sum;
        }
        else if(v1 == 0 && v2 == 1)
        {
            simultaneousEquation.text = "y = " + sum;
        }
        else if(v1 == 0 && v2 != 1)
        {
            if (v2 == -1)
            {
                simultaneousEquation.text = "-y = " + sum;
            }
            else
            {
                simultaneousEquation.text = v2 + "y = " + sum;
            }
        }
        else if(v1 == 1 && v2 == 0)
        {
            simultaneousEquation.text = "x = " + sum;
        }
        else if(v1 != 1 && v2 == 0)
        {
            if(v1 == -1)
            {
                simultaneousEquation.text = "-x = " + sum;
            }
            else
            {
                simultaneousEquation.text = v1 + "x = " + sum;
            }
        }
        else if(v1 != 1 && v2 == 1)
        {
            if (v1 == -1)
            {
                simultaneousEquation.text = "-x + y = " + sum;
            }
            else
            {
                simultaneousEquation.text = v1 + "x + y = " + sum;
            }
        }
        else if(v1 == 1 && v2 != 1)
        {
            if(v2 < 0)
            {
                if (v2 == -1)
                {
                    simultaneousEquation.text = "x - y = " + sum;
                }
                else
                {
                    int tempValue = -v2;
                    simultaneousEquation.text = "x - " + tempValue + "y = " + sum;
                }
            }
            else
            {
                simultaneousEquation.text = "x + " + v2 + "y = " + sum;
            }
            
        }
        else if(v1 != 1 && v2 != 1)
        {
            if (v1 == -1)
            {
                if (v2 < 0)
                {
                    if (v2 == -1)
                    {
                        simultaneousEquation.text = "-x - y = " + sum;
                    }
                    else
                    {
                        int tempValue = -v2;
                        simultaneousEquation.text = "-x - " + tempValue + "y = " + sum;
                    }
                }
                else
                {
                    simultaneousEquation.text = "-x + " + v2 + "y = " + sum;
                }
            }
            else
            {
                if (v2 < 0)
                {
                    if (v2 == -1)
                    {
                        simultaneousEquation.text = v1+"x - y = " + sum;
                    }
                    else
                    {
                        int tempValue = -v2;
                        simultaneousEquation.text = v1+"x - " + tempValue + "y = " + sum;
                    }
                }
                else
                {
                    simultaneousEquation.text = v1+"x + " + v2 + "y = " + sum;
                }
            }
        }
        
    }
    #endregion
    
    #region private string CreationOfSimultaneousEquations(string x, string y, int v1, int v2, int sum)

    private string CreationOfSimultaneousEquations(string x, string y, Fraction v1, Fraction v2, Fraction sum)
    {
        string text = "";

        if (x == "" && y == "")
        {
            text = v1.ToString() + "x" + " + " + v2.ToString() + "y" + " = " + sum.ToString();
        }
        else if (x != "" && y == "")
        {
            text = v1.ToString() + " + " + v2.ToString() + "y" + " = " + sum.ToString();
        }
        else if (x == "" && y != "")
        {
            text = v1.ToString() + "x" + " + " + v2.ToString() + " = " + sum.ToString();
        }
        else
        {
            text = v1.ToString() + " + " + v2.ToString() + " = " + sum.ToString();
        }

        return text;
    }

    #endregion

    #region private void WorkingOutSum(int a, int b, int c, int d, int e, int f)
    void WorkingOutSum(int a, int b, int c, int d, int e, int f)
    {
        //Special Case when either a or b is 0
        //OR d or e is 0
        if(
            (a == 0 && b != 0) 
            || 
            (a != 0 && b == 0) 
            || 
            (d == 0 && e != 0)
            ||
            (d != 0 && e == 0)
          )
        {
            SpecialCase(a, b, c, d, e, f);
        }
        //type 0 [a = -d] OR [-a = d]
        else if ((a == -d) || (-a == d))
        {
            Type1_1(a, b, c, d, e, f);
        }
        else if ((b == -e) || (-b == e))
        {
            Type1_2(a, b, c, d, e, f);
        }
        //type 1 [a = d]
        else if (a == d)
        {
            Type2(a, b, c, d, e, f);
        }
        //type 2 [b = e]
        else if (b == e)
        {
            Type3(a, b, c, d, e, f);
        }
        else if (((a % d == 0) || (d % a == 0)) && (a != d) && a != 0 && d != 0)
        {
            //check if a > d
            //check if value of a is still greater than -d, assuming d is negative
            //then multiply equation 2
            //else multiply equation 1
            if (a > d)
            {
                if (a > -d)
                {
                    Type4_1(a, b, c, d, e, f);
                }
                else if (a < -d)
                {
                    Type4_2(a, b, c, d, e, f);
                }
            }
            //check if a < d
            //check if value of a is still smaller than -d
            //then multiply equation 2
            //else multiply equation 1
            else if (a < d)
            {
                if (-a < d)
                {
                    Type4_3(a, b, c, d, e, f);
                }
                else if (-a > d)
                {
                    Type4_4(a, b, c, d, e, f);
                }
            }
        }
        else if (((b % e == 0) || (e % b == 0)) && (b != e))
        {
            //check if b > e
            //check if value of b is still greater than -e, assuming e is negative
            //then multiply equation 2
            //else multiply equation 1
            if (b > e)
            {
                if (b > -e)
                {
                    Type4_5(a, b, c, d, e, f);
                }
                else if (b < -e)
                {
                    Type4_6(a, b, c, d, e, f);
                }
            }
            //check if b < e
            //check if value of b is still smaller than -e
            //then multiply equation 2
            //else multiply equation 1
            else if (b < e)
            {
                if (-b < e)
                {
                    Type4_7(a, b, c, d, e, f);
                }
                else if (-b > e)
                {
                    Type4_8(a, b, c, d, e, f);
                }
            }
        }
        else
        {
            Type5(a, b, c, d, e, f);
        }
    }
    #endregion

    #region private void SpecialCase(int a, int b, int c, int d, int e, int f)
    void SpecialCase(int a, int b, int c, int d, int e, int f)
    {
        //Set the type
        if (a == 0 && b != 0 && d != 0 && e != 0)
        {
            //Value of Y -> by = c ==> y = c / b 
            var fractionY = new Fraction(c, b);
            var fractionIncorrectOneY = CreateIncorrectAnswers(b, c, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(b, c, fractionY, fractionIncorrectOneY, 2);

            //Value of X in equation 2 -> dx + e(c/b) = f ==> x = (bf - ec) / db
            var mulBF = b * f;
            var mulEC = e * c;
            var mulDB = d * b;

            var totalNumSum = mulBF - mulEC;

            var fractionX = new Fraction(totalNumSum, mulDB);
            var fractionIncorrectOneX = CreateIncorrectAnswers(mulDB, totalNumSum, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(mulDB, totalNumSum, fractionX, fractionIncorrectOneX, 2);

            DisplayAnswers(fractionX, fractionY, "correct");

            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
        }
        else if (a != 0 && b == 0 && d != 0 && e != 0)
        {
            //Value of X -> ax = c ==> x = c / a
            var fractionX = new Fraction(c, a);
            var fractionIncorrectOneX = CreateIncorrectAnswers(a, c, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(a, c, fractionX, fractionIncorrectOneX, 2);

            //Value of Y in equation 2 -> d(c/a) + ey = f ==> y = (fa - dc) / ea
            var mulFA = f * a;
            var mulDC = d * c;
            var mulEA = e * a;

            var totalNumSum = mulFA - mulDC;

            var fractionY = new Fraction(totalNumSum, mulEA);
            var fractionIncorrectOneY = CreateIncorrectAnswers(mulEA, totalNumSum, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(mulEA, totalNumSum, fractionY, fractionIncorrectOneY, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
        }
        else if (a != 0 && b != 0 && d == 0 && e != 0)
        {
            //Value of Y -> ey = f ==> y = f / e
            var fractionY = new Fraction(f, e);
            var fractionIncorrectOneY = CreateIncorrectAnswers(e, f, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(e, f, fractionY, fractionIncorrectOneY, 2);

            //Value of X in equation 1 -> ax + b(f/e) = c ==> x = (ce - bf) / ea
            var mulCE = c * e;
            var mulBF = b * f;
            var mulAE = a * e;

            var totalNumSum = mulCE - mulBF;

            var fractionX = new Fraction(totalNumSum, mulAE);
            var fractionIncorrectOneX = CreateIncorrectAnswers(mulAE, totalNumSum, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(mulAE, totalNumSum, fractionX, fractionIncorrectOneX, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
        }
        else if(a != 0 && b != 0 && d != 0 && e == 0)
        {
            //Value of X -> dx = f ==> x = f / d
            var fractionX = new Fraction(f, d);
            var fractionIncorrectOneX = CreateIncorrectAnswers(d, f, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(d, f, fractionX, fractionIncorrectOneX, 2);

            //Value of Y in equation 1 -> a(f/d) + by = c ==> y = (cd - af) / bd
            var mulCD = c * d;
            var mulAF = a * f;
            var mulBD = b * d;

            var totalNumSum = mulCD - mulAF;

            var fractionY = new Fraction(totalNumSum, mulBD);
            var fractionIncorrectOneY = CreateIncorrectAnswers(mulBD, totalNumSum, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(mulBD, totalNumSum, fractionY, fractionIncorrectOneY, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
        }
    }
    #endregion

    #region private void Type1_1(int a, int b, int c,int d, int e, int f)
    //a = -d OR d = -a
    void Type1_1(int a, int b, int c, int d, int e, int f)
    {

        //Add (1) with (2)
        var sumBE = b + e;
        var sumCF = c + f;

        var mulBF = b * f;
        var mulEC = c * e;
        var mulAB = a * b;
        var mulAE = a * e;

        try
        {
            var fractionY = new Fraction(sumCF, sumBE);
            var fractionIncorrectOneY = CreateIncorrectAnswers(sumBE, sumCF, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(sumBE, sumCF, fractionY, fractionIncorrectOneY, 2);

            //Place it in equation 1 or 2
            var valueOfX_V2_Num = mulEC - mulBF;
            var valueOfX_V2_Den = mulAB + mulAE;

            var fractionX = new Fraction(valueOfX_V2_Num, valueOfX_V2_Den);
            var fractionIncorrectOneX = CreateIncorrectAnswers(valueOfX_V2_Den, valueOfX_V2_Num, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(valueOfX_V2_Den, valueOfX_V2_Num, fractionX, fractionIncorrectOneX, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");

            gameManager.xValue = fractionX;
            gameManager.yValue = fractionY;
        }
        catch (FractionException)
        {
            
            gameManager.DestroyCubesOnFractionError();
            sumCalculated = false;
        }
    }
    #endregion

    #region private void Type1_2(int a, int b, int c, int d, int e, int f)
    void Type1_2(int a, int b, int c, int d, int e, int f)
    {

        //Add (1) with (2)
        var sumAD = a + d;
        var sumCF = c + f;

        if (a + d == 0)
        {
            Debug.Log("SUM AD IS 0");
        }

        var mulCD = c * d;
        var mulAF = a * f;
        var mulAB = a * b;
        var mulBD = b * d;

        try
        {
            var fractionX = new Fraction(sumCF, sumAD);
            var fractionIncorrectOneX = CreateIncorrectAnswers(sumAD, sumCF, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(sumAD, sumCF, fractionX, fractionIncorrectOneX, 2);

            //Place it in equation 1 or 2
            var valueOfY_V2_Num = mulCD - mulAF;
            var valueOfY_V2_Den = mulAB + mulBD;

            var fractionY = new Fraction(valueOfY_V2_Num, valueOfY_V2_Den);
            var fractionIncorrectOneY = CreateIncorrectAnswers(valueOfY_V2_Den, valueOfY_V2_Num, fractionX, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(valueOfY_V2_Den, valueOfY_V2_Num, fractionX, fractionIncorrectOneY, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
            
            gameManager.xValue = fractionX;
            gameManager.yValue = fractionY;
        }
        catch(FractionException)
        {
            Debug.Log("Fraction Exception Encountered");
            gameManager.DestroyCubesOnFractionError();
            sumCalculated = false;
        }

    }
    #endregion

    #region private void Type2(int a, int b, int c, int d, int e, int f)
    //a = d
    void Type2(int a, int b, int c, int d, int e, int f)
    {
        //Set the type

        //Multiply by -1 equation (2)
        int tempValueD = -d;
        int tempValueE = -e;
        int tempSumF = -f;

        //Add (1) with (2)
        var sumBE = b + tempValueE;
        var sumCF = c + tempSumF;

        //To find x = bf - ec / (ab - ae)
        var mulBF = b * f;
        var mulEC = c * e;
        var mulAB = a * b;
        var mulAE = a * e;

        try
        {
            var fractionY = new Fraction(sumCF, sumBE);

            var fractionIncorrectOneY = CreateIncorrectAnswers(sumBE, sumCF, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(sumBE, sumCF, fractionY, fractionIncorrectOneY, 2);

            //Place it in equation 1 or 2
            // ax + b(ValueOfY) = c  => x = bf - ec / (ab - ae) 
            //Find value of x
            var valueOfX_V2_Num = mulBF - mulEC;
            var valueOfX_V2_Den = mulAB - mulAE;

            var fractionX = new Fraction(valueOfX_V2_Num, valueOfX_V2_Den);
            
            var fractionIncorrectOneX = CreateIncorrectAnswers(valueOfX_V2_Den, valueOfX_V2_Num, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(valueOfX_V2_Den, valueOfX_V2_Num, fractionX, fractionIncorrectOneX, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
            
            gameManager.xValue = fractionX;
            gameManager.yValue = fractionY;
        }
        catch(FractionException)
        {
            Debug.Log("Fraction Exception Encountered");
            gameManager.DestroyCubesOnFractionError();
            sumCalculated = false;
        }
    }
    #endregion

    #region private void Type3(int a, int b, int c, int d, int e, int f)
    //b = e
    void Type3(int a, int b, int c, int d, int e, int f)
    {
        //Set type

        //Multiply by -1 among equation 2
        int tempValueD = -d;
        int tempValueE = -e;
        int tempValueF = -f;

        //Add equation 1 with equation 2
        var sumAD = a + tempValueD;
        var sumCF = c + tempValueF;

        try
        {
            //Find the value of X
            var fractionX = new Fraction(sumCF, sumAD);
            var fractionIncorrectOneX = CreateIncorrectAnswers(sumAD, sumCF, fractionX, null, 1);
            var fractionIncorrectTwoX = CreateIncorrectAnswers(sumAD, sumCF, fractionX, fractionIncorrectOneX, 2);

            //Numerator for Fraction Y
            var mulAF = a * f;
            var mulCD = c * d;
            var mulAB = a * b;
            var mulBD = b * d;

            var numeratorY = mulAF - mulCD;
            var denominatorY = mulAB - mulBD;

            //Find the value of Y
            var fractionY = new Fraction(numeratorY, denominatorY);
            var fractionIncorrectOneY = CreateIncorrectAnswers(denominatorY, numeratorY, fractionY, null, 1);
            var fractionIncorrectTwoY = CreateIncorrectAnswers(denominatorY, numeratorY, fractionY, fractionIncorrectOneY, 2);

            DisplayAnswers(fractionX, fractionY, "correct");
            DisplayAnswers(fractionIncorrectOneX, fractionIncorrectOneY, "wrongOne");
            DisplayAnswers(fractionIncorrectTwoX, fractionIncorrectTwoY, "wrongTwo");
            
            gameManager.xValue = fractionX;
            gameManager.yValue = fractionY;
        }
        catch(FractionException)
        {
            Debug.Log("Fraction Exception Encountered");
            gameManager.DestroyCubesOnFractionError();
            sumCalculated = false;
        }
    }
    #endregion

    #region private void Type4_1(int a, int b, int c, int d, int e, int f)
    //a > d and a > -d and d is negative
    //For example
    //a = 4
    //d = -2
    void Type4_1(int a, int b, int c, int d, int e, int f)
    {
        //Set the type

        //multiply by a division by d in equation 2
        var multiplyingValue = a / d;

        //equation 2 * (a/d)
        int mulDDiv = d * multiplyingValue;
        int mulEDiv = e * multiplyingValue;
        int mulFDiv = f * multiplyingValue;

        //Substitute for Type 1
        Type2(a, b, c, mulDDiv, mulEDiv, mulFDiv);
    }
    #endregion

    #region private void Type4_2(int a, int b, int c, int d, int e, int f)
    // a > d BUT a < -d
    //assume the following values a = 4 and d = -8 thus -d = 8
    void Type4_2(int a, int b, int c, int d, int e, int f)
    {

        //multiply by division by d in equation 2
        var multiplyingValue = d / a;

        //equation 1 * (d/a)
        int mulADiv = a * multiplyingValue;
        int mulBDiv = b * multiplyingValue;
        int mulCDiv = c * multiplyingValue;

        //Substitute for Type 1
        Type2(mulADiv, mulBDiv, mulCDiv, d, e, f);

    }
    #endregion

    #region private void Type4_3(int a, int b, int c, int d, int e, int f)
    //a < d AND -a < d
    void Type4_3(int a, int b, int c, int d, int e, int f)
    {

        //find multiplying value
        int multiplyingValue = d / a;

        //place it in equation 1
        int mulADiv = a * multiplyingValue;
        int mulBDiv = b * multiplyingValue;
        int mulCDiv = c * multiplyingValue;

        Type2(mulADiv, mulBDiv, mulCDiv, d, e, f);
    }
    #endregion

    #region private void Type4_4(int a, int b, int c, int d, int e, int f)
    //a < d BUT -a > d
    void Type4_4(int a, int b, int c, int d, int e, int f)
    {

        //find multiplying value
        int multiplyingValue = a / d;

        //place it in equation 2
        int mulDDiv = d * multiplyingValue;
        int mulEDiv = e * multiplyingValue;
        int mulFDiv = f * multiplyingValue;

        Type2(a, b, c, mulDDiv, mulEDiv, mulFDiv);
    }
    #endregion

    #region private void Type4_5(int a, int b, int c, int d, int e, int f)
    //b > e and b > -e and e is negative
    //For example
    //b = 4
    //e = -2
    void Type4_5(int a, int b, int c, int d, int e, int f)
    {

        //multiply by a b / e in equation 2
        var multiplyingValue = b / e;

        //equation 2 * (b/e)
        int mulDDiv = d * multiplyingValue;
        int mulEDiv = e * multiplyingValue;
        int mulFDiv = f * multiplyingValue;

        //Substitute for Type 2
        Type2(a, b, c, mulDDiv, mulEDiv, mulFDiv);
    }
    #endregion

    #region private void Type4_6(int a, int b, int c, int d, int e, int f)
    //b > e but b < -e and e is negative
    //For Example
    //b = 4
    //e = -8
    void Type4_6(int a, int b, int c, int d, int e, int f)
    {

        //multiply by a division of e / b in equation 1
        var multiplyingValue = e / b;

        //equation 1 * (e/b)
        int mulADiv = a * multiplyingValue;
        int mulBDiv = b * multiplyingValue;
        int mulCDiv = c * multiplyingValue;

        //Substitute for Type 2
        Type2(mulADiv, mulBDiv, mulCDiv, d, e, f);
    }
    #endregion

    #region private void Type4_7(int a, int b, int c, int d, int e, int f)
    //b < e AND -b < e
    //For Example
    //b = -1
    //e = 2
    void Type4_7(int a, int b, int c, int d, int e, int f)
    {

        //multiply by a division of e / b
        var multiplyingValue = e / b;

        //equation 1 * (e/b)
        int mulADiv = a * multiplyingValue;
        int mulBDiv = b * multiplyingValue;
        int mulCDiv = c * multiplyingValue;

        //Substitute in Type 2
        Type2(mulADiv, mulBDiv, mulCDiv, d, e, f);

    }
    #endregion

    #region private void Type4_8(int a, int b, int c, int d, int e, int f)
    //b < e BUT -b > e
    //For Example
    //b = -3
    //e = 1
    void Type4_8(int a, int b, int c, int d, int e, int f)
    {

        //find the multiplicative value
        var multiplyingValue = b / e;

        //Multiply Equation 2  with multiplyingValue
        int mulDDiv = d * multiplyingValue;
        int mulEDiv = e * multiplyingValue;
        int mulFDiv = f * multiplyingValue;

        //Use Type 2
        Type2(a, b, c, mulDDiv, mulEDiv, mulFDiv);
    }
    #endregion

    #region private void Type5(int a, int b, int c, int d, int e, int f)
    void Type5(int a, int b, int c, int d, int e, int f)
    {
        //a != d AND b != e AND a % d != 0 AND b % e != 0 

        //store the values of a and d
        //BUT Check for negative values

        var tempA = a;
        var tempD = d;

        if (a < 0)
        {
            tempA = -a;
        }

        if (d < 0)
        {
            tempD = -d;
        }

        //multiply equation 1 with d and equation 2 with a
        int NewA = a * tempD;
        int NewB = b * tempD;
        int NewC = c * tempD;

        int NewD = d * tempA;
        int NewE = e * tempA;
        int NewF = f * tempA;

        if(NewA == NewD)
        {
            Type2(NewA, NewB, NewC, NewD, NewE, NewF);
        }
        else if((NewA == -NewD) || (-NewA == NewD))
        {
            Type1_1(NewA, NewB, NewC, NewD, NewE, NewF);
        }

    }
    #endregion

    #region private void DisplayAnswers(Fraction fractionX, Fraction fractionY)
    void DisplayAnswers(Fraction fractionX, Fraction fractionY, string option)
    {
        switch (option)
        {
            case "correct":
                {
                    if (correctAnswer != null)
                    {
                        correctAnswer.GetComponent<TextMeshPro>().text = "x = " + fractionX.ToString() + "\ny = " + fractionY.ToString();
                    }
                    break;
                }
            case "wrongOne":
                {
                    if(wrongAnswer1 != null)
                    {
                        wrongAnswer1.GetComponent<TextMeshPro>().text = "x = " + fractionX.ToString() + "\ny = " + fractionY.ToString();
                    }
                    break;
                }
            case "wrongTwo":
                {
                    if (wrongAnswer2 != null)
                    {
                        wrongAnswer2.GetComponent<TextMeshPro>().text = "x = " + fractionX.ToString() + "\ny = " + fractionY.ToString();
                    }
                    break;
                }
        }
    }
    #endregion

    #region private Fraction CreateIncorrectAnswers()
    Fraction CreateIncorrectAnswers(int denominator, int numerator, Fraction fraction1, Fraction fraction2, int option)
    {
        var count = 0;
        switch (option)
        {
            case 1:
                {
                    while (count != 10)
                    {
                        count++;
                        var randomDenominator = Random.Range(-denominator, denominator+10);
                        if (randomDenominator != 0)
                        {
                            var fractionIncorrect = new Fraction(Random.Range(-numerator, numerator+10), randomDenominator);
                            if (fractionIncorrect != fraction1)
                            {
                                return fractionIncorrect;
                            }
                        }
                    }
                    return new Fraction(0, 1);
                }
            case 2:
                {
                    while (count != 10)
                    {
                        count++;
                        var randomDenominator = Random.Range(-denominator, denominator+10);
                        if (randomDenominator != 0)
                        {
                            var fractionIncorrect = new Fraction(Random.Range(-numerator, numerator+10), randomDenominator);
                            if (fractionIncorrect != fraction1 && fractionIncorrect != fraction2)
                            {
                                return fractionIncorrect;
                            }
                        }
                    }
                    return new Fraction(0, 1);
                }
            default:
                return new Fraction(0, 1);
        }
    }
    #endregion

    #region OnClick Functions

    //For testing purposes
    #region public void OnClickChangeSum()
    public void OnClickChangeSum()
    {
        sumCalculated = false;
    }
    #endregion

    #region public void OnClickSelectAnswer()
    public void OnClickSelectAnswer(Button button)
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().buttonClicked = true;
        //Debug.Log(button.transform.parent.name);
        if (button.transform.parent.name == "Correct Answer")
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().correct = true;
            GameObject.Find("UIManager").GetComponent<UIManager>().wrong = false;
            GameObject.Find("UIManager").GetComponent<UIManager>().sumCalculated = false;
        }
        else
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().correct = false;
            GameObject.Find("UIManager").GetComponent<UIManager>().wrong = true;
        }
    }
    #endregion

    #region public void OnClickSwitchScreenButtons(Button button)
    public void OnClickSwitchScreenButtons(Button button)
    {
        switch (button.name)
        {
            case "StartGameButton1":
                SceneManager.LoadScene("GameTutorialScene");
                break;
            case "StartGameButton2":
                SceneManager.LoadScene("Game2TutorialScene");
                break;
            case "TutorialButton":
                ScreenSwitch(false, false, true);
                break;
            case "QuitButton":
                Application.Quit();
                break;

        }
    }
    #endregion

    #region public void OnClickTutorialButtons(Button button)
    public void OnClickTutorialButtons(Button button)
    {
        tutorialButtonClicked = true;
        isHover = false;
        isClicking = true;
        switch (button.name)
        {
            case "Type1":
                {
                    SetTutorialBool(true, false, false, false, false);
                    SetTutorialSubSectionBool(true, false);
                    break;
                }
            case "Type2":
                {
                    SetTutorialBool(false, true, false, false, false);
                    break;
                }
            case "Type3":
                {
                    SetTutorialBool(false, false, true, false, false);
                    break;
                }
            case "Type4":
                {
                    SetTutorialBool(false, false, false, true, false);
                    SetTutorialSubSectionBool(true, false, false, false, false, false, false, false);
                    break;
                }
            case "Type5":
                {
                    SetTutorialBool(false, false, false, false, true);
                    break;
                }
            case "Type1.1":
                {
                    SetTutorialSubSectionBool(true, false);
                    break;
                }
            case "Type1.2":
                {
                    SetTutorialSubSectionBool(false, true);
                    break;
                }
            case "Type4.1":
                {
                    SetTutorialSubSectionBool(true, false, false, false, false, false, false, false);
                    break;
                }
            case "Type4.2":
                {
                    SetTutorialSubSectionBool(false, true, false, false, false, false, false, false);
                    break;
                }
            case "Type4.3":
                {
                    SetTutorialSubSectionBool(false, false, true, false, false, false, false, false);
                    break;
                }
            case "Type4.4":
                {
                    SetTutorialSubSectionBool(false, false, false, true, false, false, false, false);
                    break;
                }
            case "Type4.5":
                {
                    SetTutorialSubSectionBool(false, false, false, false, true, false, false, false);
                    break;
                }
            case "Type4.6":
                {
                    SetTutorialSubSectionBool(false, false, false, false, false, true, false, false);
                    break;
                }
            case "Type4.7":
                {
                    SetTutorialSubSectionBool(false, false, false, false, false, false, true, false);
                    break;
                }
            case "Type4.8":
                {
                    SetTutorialSubSectionBool(false, false, false, false, false, false, false, true);
                    break;
                }
        }
    }
    #endregion

    #region public void OnClickChangeToValueLetters()
    public void OnClickChangeToValueLetters()
    {
        valueButtonClicked = !valueButtonClicked;
    }
    #endregion

    #region public void OnClickBackButton(Button button)
    public void OnClickBackButton(Button button)
    {
        switch(button.name)
        {
            case "BackArrowTutorial":
                ScreenSwitch(true, false, false);
                SetTutorialBool(true, false, false, false, false);
                SetTutorialSubSectionBool(true, false);
                SetButtonColorDefaultTutorial();
                SetButtonColorDefaultStart();
                break;
        }
    }
    #endregion
    
    #region public void OnClickOpenTutorialWindow()

    public void OnClickOpenTutorialWindow()
    {
        isExamplesOpen = !isExamplesOpen;

        if (isExamplesOpen)
        {
            tutorialHandBoardOpen = true;
            tutorialHandBoard.SetActive(true);
        }
        else if(!isExamplesOpen)
        {
            tutorialHandBoardOpen = false;
            tutorialHandBoard.SetActive(false);
        }

        isClicking = true;
    }
    #endregion
    
    #region public void OnClickResetSum()

    public void OnClickResetSum()
    {
        foreach (var cube in GameObject.FindGameObjectsWithTag("SumCube"))
        {
            Destroy(cube);
        }

        gameManager.questionCubes = new List<GameObject>();

        switch(gameManager.questionNumber)
        {
            case 0:
            case 1:
                gameManager.SpawnQuestionCubes(gameManager.spawnLocations);
                break;
            case 2:
                gameManager.SpawnQuestionCubes(gameManager.spawnLocations_2);
                break;
            case 3:
                gameManager.SpawnQuestionCubes(gameManager.spawnLocations_3);
                break;
        }
        gameManager.textPlaced = false;
        gameManager.colorChosen = false;

        if (gameManager.xFoundValue == "" && gameManager.yFoundValue == "")
        {
            gameManager.newA = gameManager.startNewA;
            gameManager.newB = gameManager.startNewB;
            gameManager.newC = gameManager.startNewC;
            gameManager.newD = gameManager.startNewD;
            gameManager.newE = gameManager.startNewE;
            gameManager.newF = gameManager.startNewF;
        }
        else if (gameManager.xFoundValue != "" && gameManager.yFoundValue == "")
        {
            gameManager.newA = gameManager.startNewA*gameManager.xFoundValue;
            gameManager.newB = gameManager.startNewB;
            gameManager.newC = gameManager.startNewC;
            gameManager.newD = gameManager.startNewD * gameManager.xFoundValue;
            gameManager.newE = gameManager.startNewE;
            gameManager.newF = gameManager.startNewF;
        }
        else if (gameManager.xFoundValue == "" && gameManager.yFoundValue != "")
        {
            gameManager.newA = gameManager.startNewA;
            gameManager.newB = gameManager.startNewB * gameManager.yFoundValue;
            gameManager.newC = gameManager.startNewC;
            gameManager.newD = gameManager.startNewD;
            gameManager.newE = gameManager.startNewE * gameManager.yFoundValue;
            gameManager.newF = gameManager.startNewF;
        }
        else
        {
            gameManager.newA = gameManager.startNewA * gameManager.xFoundValue;
            gameManager.newB = gameManager.startNewB * gameManager.yFoundValue;
            gameManager.newC = gameManager.startNewC;
            gameManager.newD = gameManager.startNewD * gameManager.xFoundValue;
            gameManager.newE = gameManager.startNewE * gameManager.yFoundValue;
            gameManager.newF = gameManager.startNewF;
        }
    }
    #endregion

    #region public void OnClickPerformAddition()

    public void OnClickPerformAddition()
    {
        if (gameManager.gameScene2)
        {
            additionMessageText[gameManager.questionNumber - 1].text = "";
            additionStatus = true;
            List<SumCube> cubesInAddition = new List<SumCube>();
            foreach (var cube in gameManager.cubesInAddRoom)
            {
                cubesInAddition.Add(cube);
            }

            if (cubesInAddition.Count > 2)
            {
                additionStatus = false;
                additionMessageText[gameManager.questionNumber - 1].text += "\nOnly two cubes must be in the room";
            }

            if (cubesInAddition.Count <= 1)
            {
                additionStatus = false;
                additionMessageText[gameManager.questionNumber - 1].text += "\nThere must be two cubes in the room";
            }


            if (cubesInAddition.Count == 2)
            {
                if (gameManager.xFoundValue == "x" && gameManager.yFoundValue == "y")
                {
                    if (cubesInAddition[0].MaterialOnCube.ToString() == cubesInAddition[1].MaterialOnCube.ToString())
                    {
                        additionStatus = false;
                        additionMessageText[gameManager.questionNumber - 1].text += "\nCubes must be of a different colour";
                    }
                }

                if (
                    (cubesInAddition[0].TextOnCube.Contains("x") && cubesInAddition[1].TextOnCube.Contains("y")) ||
                    (cubesInAddition[0].TextOnCube.Contains("y") && cubesInAddition[1].TextOnCube.Contains("x")) ||
                    (
                        (cubesInAddition[0].TextOnCube.Contains("x") || cubesInAddition[0].TextOnCube.Contains("y")) &&
                        (!cubesInAddition[1].TextOnCube.Contains("x") && !cubesInAddition[1].TextOnCube.Contains("y"))
                    )
                    ||
                    (
                        (!cubesInAddition[0].TextOnCube.Contains("x") && !cubesInAddition[0].TextOnCube.Contains("y")) &&
                        (cubesInAddition[1].TextOnCube.Contains("x") || cubesInAddition[1].TextOnCube.Contains("y"))
                    )
                )
                {
                    additionStatus = false;
                    additionMessageText[gameManager.questionNumber - 1].text += "\nCubes must contain the same symbol or no symbol (no x or y)";
                }
            }

            if (additionStatus)
            {
                additionMessageText[gameManager.questionNumber - 1].text += "\nADDITION PERFORMED";
                Destroy(gameManager.cubesAddRoomGameObject[0]);
                Destroy(gameManager.cubesAddRoomGameObject[1]);
                gameManager.cubesInAddRoom = new List<SumCube>();
                gameManager.additionPerformed = true;
            }
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            additionMessageText[gameManager.questionNumber].text = "";
            additionStatus = true;
            List<SumCube> cubesInAddition = new List<SumCube>();
            foreach (var cube in gameManager.cubesInAddRoom)
            {
                cubesInAddition.Add(cube);
            }

            if (cubesInAddition.Count > 2)
            {
                additionStatus = false;
                additionMessageText[gameManager.questionNumber].text += "\nOnly two cubes must be in the room";
            }

            if (cubesInAddition.Count <= 1)
            {
                additionStatus = false;
                additionMessageText[gameManager.questionNumber].text += "\nThere must be two cubes in the room";
            }


            if (cubesInAddition.Count == 2)
            {
                if (gameManager.xFoundValue == "x" && gameManager.yFoundValue == "y")
                {
                    if (cubesInAddition[0].MaterialOnCube.ToString() == cubesInAddition[1].MaterialOnCube.ToString())
                    {
                        additionStatus = false;
                        additionMessageText[gameManager.questionNumber].text += "\nCubes must be of a different colour";
                    }
                }

                if (
                    (cubesInAddition[0].TextOnCube.Contains("x") && cubesInAddition[1].TextOnCube.Contains("y")) ||
                    (cubesInAddition[0].TextOnCube.Contains("y") && cubesInAddition[1].TextOnCube.Contains("x")) ||
                    (
                        (cubesInAddition[0].TextOnCube.Contains("x") || cubesInAddition[0].TextOnCube.Contains("y")) &&
                        (!cubesInAddition[1].TextOnCube.Contains("x") && !cubesInAddition[1].TextOnCube.Contains("y"))
                    )
                    ||
                    (
                        (!cubesInAddition[0].TextOnCube.Contains("x") && !cubesInAddition[0].TextOnCube.Contains("y")) &&
                        (cubesInAddition[1].TextOnCube.Contains("x") || cubesInAddition[1].TextOnCube.Contains("y"))
                    )
                )
                {
                    additionStatus = false;
                    additionMessageText[gameManager.questionNumber].text += "\nCubes must contain the same symbol or no symbol (no x or y)";
                }
            }

            if (additionStatus)
            {
                additionMessageText[gameManager.questionNumber].text += "\nADDITION PERFORMED";
                Destroy(gameManager.cubesAddRoomGameObject[0]);
                Destroy(gameManager.cubesAddRoomGameObject[1]);
                gameManager.cubesInAddRoom = new List<SumCube>();
                gameManager.additionPerformed = true;
            }
        }
    }
    #endregion
    
    #region public void OnClickPerformSubtraction()

    public void OnClickPerformSubtraction()
    {
        if (gameManager.gameScene2)
        {
            subtractionMessageText[gameManager.questionNumber - 1].text = "";
            subtractionStatus = true;

            if (gameManager.cubesInSubRoom[0] == null && gameManager.cubesInSubRoom[1] == null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber - 1].text += "\nThere are no cubes on the pods";
            }
            else if (gameManager.cubesInSubRoom[0] != null && gameManager.cubesInSubRoom[1] == null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber - 1].text += "\nThere must be a cube on the right pod";
            }
            else if (gameManager.cubesInSubRoom[0] == null && gameManager.cubesInSubRoom[1] != null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber - 1].text += "\nThere must be a cube on the left pod";
            }
            else if (gameManager.cubesInSubRoom[0] != null && gameManager.cubesInSubRoom[1] != null)
            {
                //Check if it is the same colour
                if ((gameManager.xFoundValue == "x") && (gameManager.yFoundValue == "y"))
                {
                    if (gameManager.cubesInSubRoom[0].MaterialOnCube.ToString() ==
                        gameManager.cubesInSubRoom[1].MaterialOnCube.ToString())
                    {
                        subtractionStatus = false;
                        subtractionMessageText[gameManager.questionNumber - 1].text += "\nCubes must be of a different colour";
                    }
                }

                //Check if it is the same symbol or no symbol
                if (
                    (gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") && gameManager.cubesInSubRoom[1].TextOnCube.Contains("y")) ||
                    (gameManager.cubesInSubRoom[0].TextOnCube.Contains("y") && gameManager.cubesInSubRoom[1].TextOnCube.Contains("x")) ||
                    (
                        (gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") || gameManager.cubesInSubRoom[0].TextOnCube.Contains("y")) &&
                        (!gameManager.cubesInSubRoom[1].TextOnCube.Contains("x") && !gameManager.cubesInSubRoom[1].TextOnCube.Contains("y"))
                    )
                    ||
                    (
                        (!gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") && !gameManager.cubesInSubRoom[0].TextOnCube.Contains("y")) &&
                        (gameManager.cubesInSubRoom[1].TextOnCube.Contains("x") || gameManager.cubesInSubRoom[1].TextOnCube.Contains("y"))
                    )
                )
                {
                    subtractionStatus = false;
                    subtractionMessageText[gameManager.questionNumber - 1].text += "\nCubes must contain the same symbol or no symbol (no x or y)";
                }
            }

            if (subtractionStatus)
            {
                subtractionMessageText[gameManager.questionNumber - 1].text += "\nSUBTRACTION PERFORMED";


                subLeftPod[gameManager.questionNumber - 1].GetComponent<MeshRenderer>().material =
                    gameManager.materialArray[7];
                subRightPod[gameManager.questionNumber - 1].GetComponent<MeshRenderer>().material =
                    gameManager.materialArray[7];
                Destroy(gameManager.cubesSubRoomGameObject[0]);
                Destroy(gameManager.cubesSubRoomGameObject[1]);
                gameManager.cubesInSubRoom = new SumCube[3];
                gameManager.subtractionPerformed = true;
            }
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            subtractionMessageText[gameManager.questionNumber].text = "";
            subtractionStatus = true;

            if (gameManager.cubesInSubRoom[0] == null && gameManager.cubesInSubRoom[1] == null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber].text += "\nThere are no cubes on the pods";
            }
            else if (gameManager.cubesInSubRoom[0] != null && gameManager.cubesInSubRoom[1] == null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber].text += "\nThere must be a cube on the right pod";
            }
            else if (gameManager.cubesInSubRoom[0] == null && gameManager.cubesInSubRoom[1] != null)
            {
                subtractionStatus = false;
                subtractionMessageText[gameManager.questionNumber].text += "\nThere must be a cube on the left pod";
            }
            else if (gameManager.cubesInSubRoom[0] != null && gameManager.cubesInSubRoom[1] != null)
            {
                //Check if it is the same colour
                if ((gameManager.xFoundValue == "x") && (gameManager.yFoundValue == "y"))
                {
                    if (gameManager.cubesInSubRoom[0].MaterialOnCube.ToString() ==
                        gameManager.cubesInSubRoom[1].MaterialOnCube.ToString())
                    {
                        subtractionStatus = false;
                        subtractionMessageText[gameManager.questionNumber].text += "\nCubes must be of a different colour";
                    }
                }

                //Check if it is the same symbol or no symbol
                if (
                    (gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") && gameManager.cubesInSubRoom[1].TextOnCube.Contains("y")) ||
                    (gameManager.cubesInSubRoom[0].TextOnCube.Contains("y") && gameManager.cubesInSubRoom[1].TextOnCube.Contains("x")) ||
                    (
                        (gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") || gameManager.cubesInSubRoom[0].TextOnCube.Contains("y")) &&
                        (!gameManager.cubesInSubRoom[1].TextOnCube.Contains("x") && !gameManager.cubesInSubRoom[1].TextOnCube.Contains("y"))
                    )
                    ||
                    (
                        (!gameManager.cubesInSubRoom[0].TextOnCube.Contains("x") && !gameManager.cubesInSubRoom[0].TextOnCube.Contains("y")) &&
                        (gameManager.cubesInSubRoom[1].TextOnCube.Contains("x") || gameManager.cubesInSubRoom[1].TextOnCube.Contains("y"))
                    )
                )
                {
                    subtractionStatus = false;
                    subtractionMessageText[gameManager.questionNumber].text += "\nCubes must contain the same symbol or no symbol (no x or y)";
                }
            }

            if (subtractionStatus)
            {
                subtractionMessageText[gameManager.questionNumber].text += "\nSUBTRACTION PERFORMED";


                subLeftPod[gameManager.questionNumber].GetComponent<MeshRenderer>().material =
                    gameManager.materialArray[7];
                subRightPod[gameManager.questionNumber].GetComponent<MeshRenderer>().material =
                    gameManager.materialArray[7];
                Destroy(gameManager.cubesSubRoomGameObject[0]);
                Destroy(gameManager.cubesSubRoomGameObject[1]);
                gameManager.cubesInSubRoom = new SumCube[3];
                gameManager.subtractionPerformed = true;
            }
        }

    }
    #endregion
    
    #region public void OnClickPerformMultiplication()

    public void OnClickPerformMultiplication()
    {
        if (gameManager.gameScene2)
        {
            multiplicationMessageText[gameManager.questionNumber - 1].text = "";
            multiplicationStatus = true;
            List<SumCube> cubesInMultiply = new List<SumCube>();
            foreach (var cube in gameManager.cubesInMulRoom)
            {
                cubesInMultiply.Add(cube);
            }

            if (cubesInMultiply.Count == 0)
            {
                multiplicationStatus = false;
                multiplicationMessageText[gameManager.questionNumber - 1].text += "\nThere must be at least one cube in the room";
            }

            if (multiplyValue == 0)
            {
                multiplicationStatus = false;
                multiplicationMessageText[gameManager.questionNumber - 1].text += "\nCannot multiply by 0. Change the multiply value";
            }

            if (multiplicationStatus)
            {
                multiplicationMessageText[gameManager.questionNumber - 1].text += "\nMULTIPLICATION PERFORMED";
                gameManager.multiplicationPerformed = true;
            }
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            multiplicationMessageText[gameManager.questionNumber].text = "";
            multiplicationStatus = true;
            List<SumCube> cubesInMultiply = new List<SumCube>();
            foreach (var cube in gameManager.cubesInMulRoom)
            {
                cubesInMultiply.Add(cube);
            }

            if (cubesInMultiply.Count == 0)
            {
                multiplicationStatus = false;
                multiplicationMessageText[gameManager.questionNumber].text += "\nThere must be at least one cube in the room";
            }

            if (multiplyValue == 0)
            {
                multiplicationStatus = false;
                multiplicationMessageText[gameManager.questionNumber].text += "\nCannot multiply by 0. Change the multiply value";
            }

            if (multiplicationStatus)
            {
                multiplicationMessageText[gameManager.questionNumber].text += "\nMULTIPLICATION PERFORMED";
                gameManager.multiplicationPerformed = true;
            }
        }
    }
    #endregion
    
    #region public void OnClickPerformDivision()

    public void OnClickPerformDivision()
    {
        if (gameManager.gameScene2)
        {
            divisionMessageText[gameManager.questionNumber - 1].text = "";
            divisionStatus = true;
            List<SumCube> cubesInDivision = new List<SumCube>();
            foreach (var cube in gameManager.cubesInDivRoom)
            {
                cubesInDivision.Add(cube);
            }

            if (cubesInDivision.Count == 0)
            {
                divisionStatus = false;
                divisionMessageText[gameManager.questionNumber - 1].text += "\nThere must be at least one cube in the room";
            }

            if (divideValue == 0)
            {
                divisionStatus = false;
                divisionMessageText[gameManager.questionNumber - 1].text += "\nCannot multiply by 0. Change the multiply value";
            }

            if (divisionStatus)
            {
                divisionMessageText[gameManager.questionNumber - 1].text += "\nDIVISION PERFORMED";
                gameManager.divisionPerformed = true;
            }
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            divisionMessageText[gameManager.questionNumber].text = "";
            divisionStatus = true;
            List<SumCube> cubesInDivision = new List<SumCube>();
            foreach (var cube in gameManager.cubesInDivRoom)
            {
                cubesInDivision.Add(cube);
            }

            if (cubesInDivision.Count == 0)
            {
                divisionStatus = false;
                divisionMessageText[gameManager.questionNumber].text += "\nThere must be at least one cube in the room";
            }

            if (divideValue == 0)
            {
                divisionStatus = false;
                divisionMessageText[gameManager.questionNumber].text += "\nCannot multiply by 0. Change the multiply value";
            }

            if (divisionStatus)
            {
                divisionMessageText[gameManager.questionNumber].text += "\nDIVISION PERFORMED";
                gameManager.divisionPerformed = true;
            }
        }
    }
    #endregion
    
    #region public void OnClickPerformEqualisation()

    public void OnClickPerformEqualisation()
    {
        if (gameManager.gameScene2)
        {
            equalMessageText[gameManager.questionNumber - 1].text = "";
            equalStatus = true;
            List<SumCube> cubesInEqual = new List<SumCube>();
            foreach (var cube in gameManager.cubesInEqualRoom)
            {
                cubesInEqual.Add(cube);
            }

            if (cubesInEqual.Count <= 1)
            {
                equalStatus = false;
                equalMessageText[gameManager.questionNumber - 1].text += "\nThere must be at two cubes in the room";
            }

            if (cubesInEqual.Count > 2)
            {
                equalStatus = false;
                equalMessageText[gameManager.questionNumber - 1].text += "\nThere can only be two cubes in the room";
            }

            if (cubesInEqual.Count == 2)
            {
                //Cube1 is 1x and Cube2 contains y
                if (cubesInEqual[0].TextOnCube == "1x" && cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1x and Cube2 contains x
                else if (cubesInEqual[0].TextOnCube == "1x" && cubesInEqual[1].TextOnCube.Contains("x"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1y and Cube2 contains x
                else if (cubesInEqual[0].TextOnCube == "1y" && cubesInEqual[1].TextOnCube.Contains("x"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1y and Cube2 contains y
                else if (cubesInEqual[0].TextOnCube == "1y" && cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is NOT 1x but it contains an x and Cube2 is a constant
                else if (cubesInEqual[0].TextOnCube != "1x" && cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nDivide the x constant to get a 1 in front of x and then return to this room";
                }
                //Cube1 is NOT 1y but it contains an y and Cube2 is a constant
                else if (cubesInEqual[0].TextOnCube != "1y" && cubesInEqual[0].TextOnCube.Contains("y") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nDivide the y constant to get a 1 in front of y and then return to this room";
                }
                //Cube1 is a constant and Cube2 is NOT 1x but it contains a x
                else if (cubesInEqual[1].TextOnCube != "1x" && cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nDivide the x constant to get a 1 in front of x and then return to this room";
                }
                //Cube1 is a constant and Cube2 is NOT 1y but it contains a y
                else if (cubesInEqual[1].TextOnCube != "1y" && cubesInEqual[1].TextOnCube.Contains("y") && !cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nDivide the y constant to get a 1 in front of y and then return to this room";
                }
                //Cube1 is a constant and Cube2 is a constant as well
                else if (!cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber - 1].text += "\nEquals can only by done by a letter cube and a constant";
                }
            }

            if (equalStatus)
            {
                equalMessageText[gameManager.questionNumber - 1].text += "\nEQUAL PERFORMED";
                foreach (var cube in gameManager.cubesEqualRoomGameObject)
                {
                    Destroy(cube);
                }
                gameManager.equalPerformed = true;
            }
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            equalMessageText[gameManager.questionNumber].text = "";
            equalStatus = true;
            List<SumCube> cubesInEqual = new List<SumCube>();
            foreach (var cube in gameManager.cubesInEqualRoom)
            {
                cubesInEqual.Add(cube);
            }

            if (cubesInEqual.Count <= 1)
            {
                equalStatus = false;
                equalMessageText[gameManager.questionNumber].text += "\nThere must be at two cubes in the room";
            }

            if (cubesInEqual.Count > 2)
            {
                equalStatus = false;
                equalMessageText[gameManager.questionNumber].text += "\nThere can only be two cubes in the room";
            }

            if (cubesInEqual.Count == 2)
            {
                //Cube1 is 1x and Cube2 contains y
                if (cubesInEqual[0].TextOnCube == "1x" && cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1x and Cube2 contains x
                else if (cubesInEqual[0].TextOnCube == "1x" && cubesInEqual[1].TextOnCube.Contains("x"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1y and Cube2 contains x
                else if (cubesInEqual[0].TextOnCube == "1y" && cubesInEqual[1].TextOnCube.Contains("x"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is 1y and Cube2 contains y
                else if (cubesInEqual[0].TextOnCube == "1y" && cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nEquals can only be done by a letter cube and a constant";
                }
                //Cube1 is NOT 1x but it contains an x and Cube2 is a constant
                else if (cubesInEqual[0].TextOnCube != "1x" && cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nDivide the x constant to get a 1 in front of x and then return to this room";
                }
                //Cube1 is NOT 1y but it contains an y and Cube2 is a constant
                else if (cubesInEqual[0].TextOnCube != "1y" && cubesInEqual[0].TextOnCube.Contains("y") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nDivide the y constant to get a 1 in front of y and then return to this room";
                }
                //Cube1 is a constant and Cube2 is NOT 1x but it contains a x
                else if (cubesInEqual[1].TextOnCube != "1x" && cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nDivide the x constant to get a 1 in front of x and then return to this room";
                }
                //Cube1 is a constant and Cube2 is NOT 1y but it contains a y
                else if (cubesInEqual[1].TextOnCube != "1y" && cubesInEqual[1].TextOnCube.Contains("y") && !cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nDivide the y constant to get a 1 in front of y and then return to this room";
                }
                //Cube1 is a constant and Cube2 is a constant as well
                else if (!cubesInEqual[0].TextOnCube.Contains("x") && !cubesInEqual[0].TextOnCube.Contains("y") && !cubesInEqual[1].TextOnCube.Contains("x") && !cubesInEqual[1].TextOnCube.Contains("y"))
                {
                    equalStatus = false;
                    equalMessageText[gameManager.questionNumber].text += "\nEquals can only by done by a letter cube and a constant";
                }
            }

            if (equalStatus)
            {
                equalMessageText[gameManager.questionNumber].text += "\nEQUAL PERFORMED";
                foreach (var cube in gameManager.cubesEqualRoomGameObject)
                {
                    Destroy(cube);
                }
                gameManager.equalPerformed = true;
            }
        }
    }
    #endregion
    
    #region public void OnClickIncreaseMultiplyByValue()

    public void OnClickIncreaseMultiplyByValue()
    {
        multiplyValue++;

        if (gameManager.gameScene2)
        {
            multiplyByText[gameManager.questionNumber - 1].text = multiplyValue.ToString();
        }
        else if (gameManager.tutorialOfGameScene2)
        {
            multiplyByText[gameManager.questionNumber].text = multiplyValue.ToString();
        }
    }
    #endregion
    
    #region public void OnClickDecreaseMultiplyByValue()

    public void OnClickDecreaseMultiplyByValue()
    {
        multiplyValue--;

        if (gameManager.gameScene2)
        {
            multiplyByText[gameManager.questionNumber - 1].text = multiplyValue.ToString();
        }
        else if (gameManager.tutorialOfGameScene2)
        {
            multiplyByText[gameManager.questionNumber].text = multiplyValue.ToString();
        }
    }
    #endregion
    
    #region public void OnClickIncreaseDivideByValue()

    public void OnClickIncreaseDivideByValue()
    {
        divideValue++;

        if (gameManager.gameScene2)
        {
            divideByText[gameManager.questionNumber - 1].text = divideValue.ToString();
        }
        else if(gameManager.tutorialOfGameScene2)
        {
            divideByText[gameManager.questionNumber].text = divideValue.ToString();
        }
    }
    #endregion
    
    #region public void OnClickDecreaseDivideByValue()

    public void OnClickDecreaseDivideByValue()
    {
        divideValue--;

        if (gameManager.gameScene2)
        {
            divideByText[gameManager.questionNumber - 1].text = divideValue.ToString();
        }
        else if (gameManager.tutorialOfGameScene2)
        {
            divideByText[gameManager.questionNumber].text = divideValue.ToString();
        }
    }
    #endregion
    
    #endregion

    #region public void RefreshBooleanColors()

    public void RefreshBooleanColors()
    {
        isCorrectBlue = false;
        isCorrectGreen = false;
        isCorrectRed = false;

        isWrongOneBlue = false;
        isWrongOneGreen = false;
        isWrongOneRed = false;
        
        isWrongTwoBlue = false;
        isWrongTwoGreen = false;
        isWrongTwoRed = false;
        
        SetAnswerStatus("");
    }
    #endregion
    
    #region public void SetAnswerStatus(string text)

    public void SetAnswerStatus(string text)
    {
        if(scene.name == "GameScene")
            answerStatus.text = text;
    }
    #endregion 
    
    //#region public void AdjustHealthBarHealth()

    //public void AdjustHealthBarHeight()
    //{
    //    currentHealthBarImageHeight = ((gameManager.currentHealth * 1.0f) / gameManager.maxHealth) * maxHealthBarImageHeight;
        
    //    var RedBarHealthTransform = healthBarRed.transform as RectTransform;
    //    var healthHeight = currentHealthBarImageHeight;
        
    //    RedBarHealthTransform.sizeDelta = new Vector2(RedBarHealthTransform.sizeDelta.x, healthHeight);
    //}
    //#endregion
    
    #region public void SetEquationColor(TextMeshPro text, int value)

    public void SetEquationColor(TextMeshPro text, int value)
    {
        switch (gameManager.colorChosenList[value])
        {
            case 0:
                text.GetComponent<TextMeshPro>().color = new Color(1, 0, 1);
                break;
            case 1:
                text.GetComponent<TextMeshPro>().color = new Color(0, 0.4f, 1);
                break;
            case 2:
                text.GetComponent<TextMeshPro>().color = new Color(0, 0.7f, 1);
                break;
            case 3:
                text.GetComponent<TextMeshPro>().color = new Color(0, 0.75f, 0);
                break;
            case 4:
                text.GetComponent<TextMeshPro>().color = new Color(1, 0.4f, 0);
                break;
            case 5:
                text.GetComponent<TextMeshPro>().color = new Color(1, 0, 0);
                break;
        }
        
    }
    #endregion
    
    #region public void ConstantlyUpdateSimultaneousEquation()

    public void ConstantlyUpdateSimultaneousEquation(int question)
    {
        simultaneousEquation1List[question].text = CreationOfSimultaneousEquations(gameManager.xFoundValue,gameManager.yFoundValue,gameManager.newA, gameManager.newB, gameManager.newC);
        simultaneousEquation2List[question].text = CreationOfSimultaneousEquations(gameManager.xFoundValue,gameManager.yFoundValue,gameManager.newD, gameManager.newE, gameManager.newF);
    }
    #endregion

    #region public void UpdateHandBoardText()
    public void UpdateHandBoardText()
    {
        simultaneousEquation1.text = "Proceed\nForward";
        simultaneousEquation2.text = "";
        correctAnswer.GetComponent<TextMeshPro>().text = "";
        wrongAnswer1.GetComponent<TextMeshPro>().text = "";
        wrongAnswer2.GetComponent<TextMeshPro>().text = "";
    }
    #endregion

    #region public void SetLetterValues()
    public void SetLetterValues()
    {
        switch (gameManager.questionNumber)
        {
            //Tutorial
            case 0:
                a = 1;
                b = 1;
                c = 3;
                d = 1;
                e = 2;
                f = 9;
                break;
            case 1:
                a = 1;
                b = 3;
                c = 4;
                d = 1;
                e = 1;
                f = 10;
                break;
            case 2:
                a = 2;
                b = 2;
                c = 8;
                d = 3;
                e = 2;
                f = 21;
                break;
            case 3:
                a = 3;
                b = 5;
                c = 10;
                d = 4;
                e = 3;
                f = 7;
                break;
        }
    }
    #endregion
}
