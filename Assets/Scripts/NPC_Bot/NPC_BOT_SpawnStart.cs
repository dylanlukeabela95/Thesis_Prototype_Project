using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BOT_SpawnStart : MonoBehaviour
{
    public AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(SaySpeech());
    }

    #region IEnumerator SaySpeech()
    IEnumerator SaySpeech()
    {
        yield return new WaitForSeconds(36.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isIntroSpeech = false;
        Destroy(this.gameObject);
    }
    #endregion
}
