using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWaypoint;

    public GameObject answerCube;

    public GameManager_Cutscene gameManager;

    public GameObject player;

    public GameObject barrel;

    public bool startMoving;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
        gameManager = GameObject.FindObjectOfType<GameManager_Cutscene>();
        StartCoroutine(ShootCubes());
        StartCoroutine(StartMoving());
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            PlayerFollow();
        }
    }

    #region public void PlayerFollow()
    public void PlayerFollow()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 1.45f, transform.position.z), player.transform.position, 4.5f * Time.deltaTime);

        var direction = (player.transform.position - transform.position).normalized;
        var rotGoal = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.01f);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    #endregion

    #region IEnumerators

    #region IEnumerator ShootCubes()
    IEnumerator ShootCubes()
    {
        while(true)
        {
            if (startMoving)
            {
                GenerateBlock();

                var random = Random.Range(1, 4);

                yield return new WaitForSeconds(random);
            }
            else
            {
                yield return null;
            }
        }
    }
    #endregion

    #region IEnumerator StartMoving()
    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(14.5f);
        startMoving = true;
    }
    #endregion

    #endregion

    #region public void GenerateBlock()
    public void GenerateBlock()
    {
        
        var cube = Instantiate(answerCube, barrel.transform.position, barrel.transform.rotation);

        
        var randomValue = Random.Range(2, 100);
        var randomCoefficient = Random.Range(0, 3);
        var randomMaterial = Random.Range(0,gameManager.materialArray.Length);
        string coefficient = "";

        switch (randomCoefficient)
        {
            case 0:
                coefficient = "x";
                break;
            case 1:
                coefficient = "y";
                break;
            case 2:
                coefficient = "";
                break;
            default:
                coefficient = "";
                break;
        }

        for(int i = 0;i<cube.transform.childCount;i++)
        {
            cube.GetComponentsInChildren<TextMeshPro>()[i].text = randomValue + coefficient;
        }

        cube.GetComponent<MeshRenderer>().material = gameManager.materialArray[randomMaterial];
        
    }
    #endregion
}
