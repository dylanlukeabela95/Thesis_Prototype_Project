using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitles : MonoBehaviour
{
    public bool cutscene, gameScene;

    // Start is called before the first frame update
    void Start()
    {
        if(cutscene)
            StartCoroutine(SubtitlesText());

        if (gameScene)
            StartCoroutine(SubtitlesTextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region public void SetSubtitleText(string text)
    public void SetSubtitleText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
    #endregion

    #region IEnumerators

    #region IEnumerator Subtitles()
    IEnumerator SubtitlesText()
    {
        SetSubtitleText("Well, Well, Well. What do we have here?");
        yield return new WaitForSeconds(1.5f);
        SetSubtitleText("Specimen 501. A telemight");
        yield return new WaitForSeconds(3f);
        SetSubtitleText("A telemight? Your race is almost extinct. Good thing I have you here. I am about to make your race even more extinct and your powers will be mine.");
        yield return new WaitForSeconds(9f);
        SetSubtitleText("What...What is going on?");
        yield return new WaitForSeconds(1.5f);
        SetSubtitleText("Subject appears to be in the air");
        yield return new WaitForSeconds(2f);
        SetSubtitleText("I can see that genius. Do something.");
        yield return new WaitForSeconds(2.9f);
        SetSubtitleText("No, No, NO");
        yield return new WaitForSeconds(1f);
        SetSubtitleText("Hang on buddy. This will be a bumpy ride");
        yield return new WaitForSeconds(2.5f);
        SetSubtitleText("Come here. Come here you fool");
        yield return new WaitForSeconds(2.25f);
        SetSubtitleText("");
    }
    #endregion

    #region IEnumerator SubtitleTextScene()
    IEnumerator SubtitlesTextScene()
    {
        SetSubtitleText("I cannot fully escort you out. The place is blocking my powers to get you out of here.");
        yield return new WaitForSeconds(5.2f);
        SetSubtitleText("Hi. My name is Artificial Learning Intelligence for Calculating Equations or the Alice bot for short.");
        yield return new WaitForSeconds(6f);
        SetSubtitleText("You my friend are a telemight, meaning that you possess the powers of telekinesis.");
        yield return new WaitForSeconds(5.2f);
        SetSubtitleText("Therefore, you can grab any blocks that you will encounter and use them to your advantage.");
        yield return new WaitForSeconds(5.2f);
        SetSubtitleText("Ahead lie 3 doors that must be opened in order for you to be able to escape. ");
        yield return new WaitForSeconds(4.5f);
        SetSubtitleText("All doors are sealed with a simultaneous equation that must be solved and once they are, the door will open.");
        yield return new WaitForSeconds(5.5f);
        SetSubtitleText("I will be in every room aiding you if you get stuck. ");
        yield return new WaitForSeconds(3f);
        SetSubtitleText("Goodluck my friend.");
        yield return new WaitForSeconds(2f);
        SetSubtitleText("");

        //continue from here
    }
    #endregion

    #endregion
}
