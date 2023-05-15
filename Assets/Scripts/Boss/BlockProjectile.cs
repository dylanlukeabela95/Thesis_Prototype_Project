using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProjectile : MonoBehaviour
{
    public bool disableForce = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableForce());
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableForce)
        {
            //transform.Translate(Vector3.back * 10 * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(-transform.forward * 8000);
        }
    }

    #region IEnumerator DisableForce()
    IEnumerator DisableForce()
    {
        yield return new WaitForSeconds(0.55f);
        disableForce = true;
    }
    #endregion
}
