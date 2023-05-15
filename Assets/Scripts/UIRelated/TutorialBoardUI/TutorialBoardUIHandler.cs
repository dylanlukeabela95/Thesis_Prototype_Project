using UnityEngine;
using UnityEngine.UI;


public class TutorialBoardUIHandler : MonoBehaviour
{
    public GameObject[] dots;
    public Color32 selectedColor;
    public Color32 defaultColor;
    public GameObject[] pages;
    public int currentPage = 0;
    public bool nextPage;
    public bool previousPage;
    public bool pageChange;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;

        defaultColor = new Color32(255, 255, 255, 255);
        selectedColor = new Color32(255, 0, 0, 255);

        dots[0].GetComponent<Image>().color = selectedColor;
        pages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(pageChange)
        {
            SwitchPages();
            pageChange = false;
        }
    }

    #region public void ResetDisplayPages()
    public void ResetDisplayPages()
    {
        for(int i = 0;i<pages.Length;i++)
        {
            pages[i].SetActive(false);
            dots[i].GetComponent<Image>().color = defaultColor;
        }
    }
    #endregion

    #region public void SwitchPages()
    public void SwitchPages()
    {
        ResetDisplayPages();

        if (nextPage)
        {
            //If on last page - go to first page
            if (currentPage == pages.Length - 1)
            {
                currentPage = 0;
            }
            else
            {
                currentPage++;
            }
            nextPage = false;
        }
        else if (previousPage)
        {
            //If on first page - go to last page
            if (currentPage == 0)
            {
                currentPage = pages.Length - 1;
            }
            else
            {
                currentPage--;
            }
            previousPage = false;
        }

        dots[currentPage].GetComponent<Image>().color = selectedColor;
        pages[currentPage].SetActive(true);
    }
    #endregion

    #region public void NextButton()
    public void NextButton()
    {
        nextPage = true;
        pageChange = true;
    }
    #endregion

    #region public void PreviousButton()
    public void PreviousButton()
    {
        previousPage = true;
        pageChange = true;
    }
    #endregion
}
