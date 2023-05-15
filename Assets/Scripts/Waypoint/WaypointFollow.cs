using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWaypoint;

    public bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
        StartCoroutine(StartMoving());
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            WaypointGoTo();
        }
    }

    #region public void WaypointGoTo()
    public void WaypointGoTo()
    {
        if(currentWaypoint < waypoints.Length) 
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, 5 * Time.deltaTime);
            if(transform.position == waypoints[currentWaypoint].transform.position ) 
            {
                currentWaypoint++;
            }
        }
        else
        {
            //Do nothing
        }
    }
    #endregion

    #region IEnumerator StartMoving()
    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(13.5f);
        startMoving = true;
    }
    #endregion
}
