using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_BOT : MonoBehaviour
{
    public GameObject[] faces;

    //this panel will be displayed according the the distance between the bot and the player
    public GameObject helpPanel;
    private bool showPanel;

    public float faceChangeTimer;

    private GameManager gameManager;
    public GameObject player;

    public TextMeshPro tipsText;

    public GameObject tipsQuestion0;
    public GameObject tipsQuestion1;
    public GameObject tipsQuestion2;
    public GameObject tipsQuestion3;

    //To handle page system for tips
    public GameObject[] tipsTextQuestion0;
    public GameObject[] tipsTextQuestion1;
    public GameObject[] tipsTextQuestion2;
    public GameObject[] tipsTextQuestion3;

    private bool nextPage;
    private bool prevPage;

    public int currentPage = 1;

    public int questionNumber;

    public TextMeshProUGUI pageNumber;

    public bool startNpc = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        StartCoroutine(ChangeFace());
    }

    // Update is called once per frame
    void Update()
    {
        if (!startNpc)
        {
            ShowHelpPanel();
            SelectTipsMenu(questionNumber);

            switch (questionNumber)
            {
                case 0:
                    ChangeTipsPage(tipsTextQuestion0);
                    break;
                case 1:
                    ChangeTipsPage(tipsTextQuestion1);
                    break;
                case 2:
                    ChangeTipsPage(tipsTextQuestion2);
                    break;
                case 3:
                    ChangeTipsPage(tipsTextQuestion3);
                    break;
            }
        }
        
    }

    #region private void ShowHelpPanel()
    private void ShowHelpPanel()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        //change back to 10
        if(distance < 10)
        {
            //open menu
            helpPanel.SetActive(true);
            showPanel = true;
        }
        else
        {
            //close menu
            helpPanel.SetActive(false);
            showPanel = false;
        }
    }
    #endregion

    #region private void SelectTipsMenu(int questionNumber)
    private void SelectTipsMenu(int questionNumber)
    {
        //implement a page system for the tips menu

        //place hint tips here depending on the question number
        switch(questionNumber)
        {
            case 0:
                //put text here depending on the panel text
                //After testing, change the tipsText to the tipsTextQuestion0 and set the page system

                tipsQuestion0.SetActive(true);
                tipsTextQuestion0[0].GetComponent<TextMeshPro>().text = "<color=yellow>Step 1</color> - Grab the x block from equation 2 and the x block from equation 1\n" +
                                            "<color=yellow>Step 2</color> - Go to the subtraction room and place x block from equation 2 on the left pod and the x block from equation 1 on the right pod\n" +
                                            "<color=yellow>Step 3</color> - Click on Subtract button\n" +
                                            "<color=yellow>Step 4</color> - Take out the newly generated block\n" +
                                            "<color=yellow>Step 5</color> - Grab the y block from equation 2 and the y block from equation 1";

                tipsTextQuestion0[1].GetComponent<TextMeshPro>().text = "<color=yellow>Step 6</color> - Go to the subtraction room and place the y block from equation 2 on the left pod and the y block from equation 1 on the right pod\n" +
                                            "<color=yellow>Step 7</color> - Click on the Subtract button\n" +
                                            "<color=yellow>Step 8</color> - Take out the newly generated block\n" +
                                            "<color=yellow>Step 9</color> - Grab the constant value block from equation 2 and the constant value block from equation 1";

                tipsTextQuestion0[2].GetComponent<TextMeshPro>().text = "<color=yellow>Step 10</color> - Go to the subtraction room and place the constant value block from equation 2 on the left pod and the constant value block from equation 1 on the right pod\n" +
                                            "<color=yellow>Step 11</color> - Click on the Subtract button\n" +
                                            "<color=yellow>Step 12</color> - Grab the newly generated block and the y block that was generated in Step 8\n" +
                                            "<color=yellow>Step 13</color> - Go to the equal room";

                tipsTextQuestion0[3].GetComponent<TextMeshPro>().text = "<color=yellow>Step 14</color> - Place both blocks in the equal room\n" +
                                            "<color=yellow>Step 15</color> - Click on the Equal button\n" +
                                            "<color=yellow>Step 16</color> - Grab the golden block and walk towards the door and place the golden block on the y pad where it says Place Y Answer Here\n" +
                                            "<color=yellow>Step 17</color> - Go back to the sum panel and click on Reset Sum";

                tipsTextQuestion0[4].GetComponent<TextMeshPro>().text = "<color=yellow>Step 18</color> - Grab the middle block of equation 1 and the right block of equation 1\n" +
                                            "<color=yellow>Step 19</color> - Go to the subtraction room and place the block that was on the right in the equation on the left pod and the middle block on the right pod\n" +
                                            "<color=yellow>Step 20</color> - Click on the Subtract button";

                tipsTextQuestion0[5].GetComponent<TextMeshPro>().text = "<color=yellow>Step 21</color> - Grab the x block from equation 1 and the newly generated block from the subtract room and take them both to the equal room\n" +
                                            "<color=yellow>Step 22</color> - Place both blocks in the equal room\n" +
                                            "<color=yellow>Step 23</color> - Click on the Equal Button\n" +
                                            "<color=yellow>Step 24</color> - Take the golden cube to the door and place the golden block on the x pad where it says Place X Answer Here";

                tipsTextQuestion0[6].GetComponent<TextMeshPro>().text = "<color=yellow>Final Step</color> - Door should have opened and sum has been solved. Proceed!!!";
                break;
            case 1:
                tipsQuestion1.SetActive(true);
                tipsTextQuestion1[0].GetComponent<TextMeshPro>().text = "<color=yellow>Step 1</color> - Grab the x block from equation 1 and the x block from equation 2\n" +
                                                                          "<color=yellow>Step 2</color> - Go to the subtract room & place the blocks on either pod\n" +
                                                                          "<color=yellow>Step 3</color> - Click on the Subtract button\n" +
                                                                          "<color=yellow>Step 4</color> - Take out the newly generated block (We will not use this)\n" +
                                                                          "<color=yellow>Step 5</color> - Grab the 3y block from equation 1 and the y block from equation 2";


                tipsTextQuestion1[1].GetComponent<TextMeshPro>().text = "<color=yellow>Step 6</color> - Go to the subtract room and place the 3y block on the left pod and the y block on the right pod\n" +
                                                                          "<color=yellow>Step 7</color> - Click on the subtract button\n" +
                                                                          "<color=yellow>Step 8</color> - Grab the newly generated block and move it out of the room (We will use it later)\n" +
                                                                          "<color=yellow>Step 9</color> - Grab the 4 block from equation 1 and the 10 block from equation 2";

                tipsTextQuestion1[2].GetComponent<TextMeshPro>().text = "<color=yellow>Step 10</color> - Go to the subtract room and place the 4 block on the left pod and the 10 block on the right pod\n" +
                                                                          "<color=yellow>Step 11</color> - Click on the subtract button\n" +
                                                                          "<color=yellow>Step 12</color> - Grab the newly gnerated block and the block that was generated earlier containing y\n" +
                                                                          "<color=yellow>Step 13</color> - Take the two blocks to the division room and place the blocks inside that room";


                tipsTextQuestion1[3].GetComponent<TextMeshPro>().text = "<color=yellow>Step 14</color> - From the division menu, increase the dividing value by 2 and then click on the Perform Division button\n" +
                                                                          "<color=yellow>Step 15</color> - Grab the newly generated blocks and go to the Equal Room\n" +
                                                                          "<color=yellow>Step 16</color> - Place the blocks inside the room and click on the Perform Equal button";


                tipsTextQuestion1[4].GetComponent<TextMeshPro>().text = "<color=yellow>Step 17</color> - Grab the newly generated block and place it on the y pod near the door\n" +
                                                                          "<color=yellow>Step 18</color> - Go back to the sum blocks\n" +
                                                                          "<color=yellow>Step 19</color> - Grab the 10 block from equation 2 and the -3 block from equation 2\n" +
                                                                          "<color=yellow>Step 20</color> - Go to the subtraction room\n" +
                                                                          "<color=yellow>Step 21</color> - Place the 10 block on the left pod and the -3 pod on the right pod";
                                                                                                                                   
                tipsTextQuestion1[5].GetComponent<TextMeshPro>().text = "<color=yellow>Step 22</color> - Click on the subtract button\n" +
                                                                          "<color=yellow>Step 23</color> - Grab the newly generated block and the x block from equation 2\n" +
                                                                          "<color=yellow>Step 24</color> - Go to the Equals room and place the blocks inside that room\n" +
                                                                          "<color=yellow>Step 25</color> - Press the Equals button\n" +
                                                                          "<color=yellow>Step 26</color> - Grab the newly generated block and place it on the x pod near the door\n" +
                                                                          "<color=yellow>Final Step</color> - Door should be open. Proceed!!!";
                break;
            case 2:
                tipsQuestion2.SetActive(true);
                tipsTextQuestion2[0].GetComponent<TextMeshPro>().text = "<color=yellow>Step 1</color> - Grab the 2y block from equation 1 and the 2y block from equation 2\n" +
                                                                        "<color=yellow>Step 2</color> - Take them to the subtract room and place the blocks on either pod\n" +
                                                                        "<color=yellow>Step 3</color> - Click on the subtract button\n" +
                                                                        "<color=yellow>Step 4</color> - Grab the newly generated block and drop it somewhere outside the rooms (We will not use this)";


                tipsTextQuestion2[1].GetComponent<TextMeshPro>().text = "<color=yellow>Step 5</color> - Grab the 3x block from equation 2 and the 2x block from equation 1\n" +
                                                                        "<color=yellow>Step 6</color> - Take them to the subtract room and place the 3x block on the left pod and the 2x block on the right pod\n" +
                                                                        "<color=yellow>Step 7</color> - Click on the Subtract button\n" +
                                                                        "<color=yellow>Step 8</color> - Grab the newly generated block and place it somewhere (To be used later)";

                tipsTextQuestion2[2].GetComponent<TextMeshPro>().text = "<color=yellow>Step 9</color> - Grab the 21 block from equation 2 and the 8 block from equation 1\n" +
                                                                        "<color=yellow>Step 10</color> - Go to the subtract room and place the 21 block on the left pod and the 8 block on the right pod\n" +
                                                                        "<color=yellow>Step 11</color> - Click on the subtract button\n" +
                                                                        "<color=yellow>Step 12</color> - Grab the newly generated block and the generated block from before, the one that contains an x";

                tipsTextQuestion2[3].GetComponent<TextMeshPro>().text = "<color=yellow>Step 13</color> - Go to the Equals room and place the two blocks inside the room\n" +
                                                                        "<color=yellow>Step 14</color> - Click on the Equals Button\n" +
                                                                        "<color=yellow>Step 15</color> - Grab the newly generated block and place it on the x pod near the door\n" +
                                                                        "<color=yellow>Step 16</color> - Go back to the sum blocks\n" +
                                                                        "<color=yellow>Step 17</color> - Grab the 26 block from equation 1 and the 8 block from equation 1";

                tipsTextQuestion2[4].GetComponent<TextMeshPro>().text = "<color=yellow>Step 18</color> - Go to the subtract room and place the 8 block on the left pod and the 26 block on the right pod\n" +
                                                                        "<color=yellow>Step 19</color> - Click on the Subtract button\n" +
                                                                        "<color=yellow>Step 20</color> - Grab the newly generated block and grab the 2y block from equation 1\n" +
                                                                        "<color=yellow>Step 21</color> - Go to the division room and place the blocks inside that room";

                tipsTextQuestion2[5].GetComponent<TextMeshPro>().text = "<color=yellow>Step 22</color> - From the division value, increase the value to 2 and click on the Division button\n" +
                                                                        "<color=yellow>Step 23</color> - Grab the two blocks and you should have a y block and a -9 block\n" +
                                                                        "<color=yellow>Step 24</color> - Go to the Equals Room and place the blocks insde that room\n" +
                                                                        "<color=yellow>Step 25</color> - Click on the Equals button\n" +
                                                                        "<color=yellow>Step 26</color> - Grab the newly generated block and place it on the y pod near the door";

                tipsTextQuestion2[6].GetComponent<TextMeshPro>().text = "<color=yellow>Final Step</color> - Door should be open. Proceed!!!";
                break;
            case 3:
                tipsQuestion3.SetActive(true);
                tipsTextQuestion3[0].GetComponent<TextMeshPro>().text = "<color=yellow>Step 1</color> - Grab all the blocks from equation 1\n" +
                                                                        "<color=yellow>Step 2</color> - Place the blocks inside the multiplication room\n" +
                                                                        "<color=yellow>Step 3</color> - Increase the multiply value to 4 and Click on the Multiply Button\n" +
                                                                        "<color=yellow>Step 4</color> - Take all the blocks out of the multiplication room\n" +
                                                                        "<color=yellow>Step 5</color> - Grab all the blocks from equation 2";

                tipsTextQuestion3[1].GetComponent<TextMeshPro>().text = "<color=yellow>Step 6</color> - Place the blocks inside the multiplication room\n" +
                                                                        "<color=yellow>Step 7</color> - Increase the multiply value to 3 and Click on the Multiply Button\n" +
                                                                        "<color=yellow>Step 8</color> - Grab the 12x block from equation 1 and 12x block from equation 2\n" +
                                                                        "<color=yellow>Step 9</color> - Go to the subtraction room and place the cubes on either pod\n" +
                                                                        "<color=yellow>Step 10</color> - Click on the Subtraction Button";

                tipsTextQuestion3[2].GetComponent<TextMeshPro>().text = "<color=yellow>Step 11</color> - Grab the newly generated block and place it somewhere (We will not be using it)\n" +
                                                                        "<color=yellow>Step 12</color> - Grab the 20y block from equation 1 and the 9y block from equation 2\n" +
                                                                        "<color=yellow>Step 13</color> - Go to the subtraction room\n" +
                                                                        "<color=yellow>Step 14</color> - Place the 20y block on the left pod and the 9y block on the right pod";

                tipsTextQuestion3[3].GetComponent<TextMeshPro>().text = "<color=yellow>Step 15</color> - Click on the Subtraction Button\n" +
                                                                        "<color=yellow>Step 16</color> - Grab the newly generated block and place it somewhere (we will use it later)\n" +
                                                                        "<color=yellow>Step 17</color> - Grab the 40 block from equation 1 and the 21 block from equation 2\n" +
                                                                        "<color=yellow>Step 18</color> - Go to the subtraction room";

                tipsTextQuestion3[4].GetComponent<TextMeshPro>().text = "<color=yellow>Step 19</color> - Place the 40 block on the left pod and the 21 block on the right pod\n" +
                                                                        "<color=yellow>Step 20</color> - Click on the Subtraction Button\n" +
                                                                        "<color=yellow>Step 21</color> - Grab the newly generated block and grab the y block that was generated earlier\n" +
                                                                        "<color=yellow>Step 22</color> - Go to the division room and place both blocks in that room";

                tipsTextQuestion3[5].GetComponent<TextMeshPro>().text = "<color=yellow>Step 23</color> - Increase the division number to 11 and click on the Division Button\n" +
                                                                        "<color=yellow>Step 24</color> - Grab both blocks from inside the room and take them to the Equal Room\n" +
                                                                        "<color=yellow>Step 25</color> - Click on the Equal Button\n" +
                                                                        "<color=yellow>Step 26</color> - Grab the newly generated block and place it on the y pod near the door\n" +
                                                                        "<color=yellow>Step 27</color> - Go back to the sum";

                tipsTextQuestion3[6].GetComponent<TextMeshPro>().text = "<color=yellow>Step 28</color> - Grab all 3 blocks from equation 1 and take them to the multiplication room\n" +
                                                                        "<color=yellow>Step 29</color> - Increase the multiply value to 11 and click on the Multiply Button\n" +
                                                                        "<color=yellow>Step 30</color> - Take out all the blocks and grab the 95 block and the 110 block\n" +
                                                                        "<color=yellow>Step 31</color> - Go to the subtraction room\n" +
                                                                        "<color=yellow>Step 32</color> - Place the 110 block on the left pod and the 95 block on the right pod";

                tipsTextQuestion3[7].GetComponent<TextMeshPro>().text = "<color=yellow>Step 33</color> - Click on the Subtraction button\n" +
                                                                        "<color=yellow>Step 34</color> - Grab the newly generated block and the 33x block and go to the division room\n" +
                                                                        "<color=yellow>Step 35</color> - Place the blocks inside the division room\n" +
                                                                        "<color=yellow>Step 36</color> - Increase the division value to 33 and Click on the Division Button";

                tipsTextQuestion3[8].GetComponent<TextMeshPro>().text = "<color=yellow>Step 37</color> - Grab the two blocks from the division room and take them to the Equal Room\n" +
                                                                        "<color=yellow>Step 38</color> - Place the two blocks inside the Equal Room and click on the Equal Button\n" +
                                                                        "<color=yellow>Step 39</color> - Grab the newly generated block and place the block on the x pod near the door\n" +
                                                                        "<color=yellow>Final Step</color> - Door should be open. Proceed!!!";
                break;
        }
    }
    #endregion

    #region public void NextTipsPage()
    public void NextTipsPage()
    {
        nextPage = true;
        ResetAllTipsPages(gameManager.questionNumber);
    }
    #endregion

    #region public void PrevTipsPage()
    public void PrevTipsPage()
    {
        prevPage = true;
        ResetAllTipsPages(gameManager.questionNumber);
    }
    #endregion

    #region private void ResetAllTipsPages(int questionNumber)
    private void ResetAllTipsPages(int questionNumber)
    {
        switch(questionNumber)
        {
            case 0:
                ResetTipsPages(tipsTextQuestion0);
                break;
            case 1:
                ResetTipsPages(tipsTextQuestion1);
                break;
            case 2:
                ResetTipsPages(tipsTextQuestion2);
                break;
            case 3:
                ResetTipsPages(tipsTextQuestion3);
                break;
        }
    }
    #endregion

    #region private void ResetTipsPages(GameObject[] questionTipsList)
    private void ResetTipsPages(GameObject[] questionTipsList)
    {
        foreach(var tipPage in questionTipsList)
        {
            tipPage.SetActive(false);
        }
    }
    #endregion

    #region private void ShowCurrentPage(int currentPage, GameObject[] questionTipsList, int questionNumber)
    private void ShowCurrentPage(int currentPage, GameObject[] questionTipsList)
    {
        questionTipsList[currentPage - 1].SetActive(true);
    }
    #endregion

    #region private void ChangeTipsPage(GameObject[] questionTipsList)
    private void ChangeTipsPage(GameObject[] questionTipsList)
    {
        ResetAllTipsPages(questionNumber);
        if (nextPage)
        {
            if (currentPage == questionTipsList.Length)
            {
                currentPage = 1;
            }
            else
            {
                currentPage++;
            }
            nextPage = false;
        }
        else if (prevPage)
        {
            if (currentPage == 1)
            {
                currentPage = questionTipsList.Length;
            }
            else
            {
                currentPage--;
            }
            prevPage = false;
        }
        ShowCurrentPage(currentPage, questionTipsList);

        pageNumber.text = currentPage + "/" + questionTipsList.Length;
    }
    #endregion

    #region private void HideFaces()
    private void HideFaces()
    {
        foreach(var face in faces) 
        {
            face.SetActive(false);
        }
    }
    #endregion

    #region private void SetFace(int faceNumber)
    private void SetFace(int faceNumber)
    {
        HideFaces();
        faces[faceNumber].SetActive(true);
    }
    #endregion

    #region IEnumerator ChangeFace()
    IEnumerator ChangeFace() 
    {
        while (true)
        {
            var random = Random.Range(0, faces.Length);
            var timer = Random.Range(2f, 10f);
            SetFace(random);
            yield return new WaitForSeconds(timer);
        }
    }
    #endregion
}