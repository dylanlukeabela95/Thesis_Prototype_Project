using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Body")
        {
            LoadScene(sceneToLoad);
        }
    }

    #region private void LoadScene(string scene)
    private void LoadScene(string scene)
    {
        if(!string.IsNullOrEmpty(scene)) 
        {
            SceneManager.LoadScene(scene);
        }
    }
    #endregion
}
