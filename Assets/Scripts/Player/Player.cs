using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    private InputDevice targetDevice;
    private InputDevice targetDevice2;

    public GameObject barrel;
    public GameObject bullet;
    [SerializeField]
    private bool isShooting = false;

    public UIManager uiManager;
    public GameManager gameManager;

    public XRRayInteractor interactor = null;
    public XRRayInteractor interactor2 = null;
    public GameObject rightHand;
    public GameObject leftHand;

    public GameObject gun;

    public bool hasCubeInHandLeft = false;
    public bool hasCubeInHandRight = false;

    public bool grabCube;

    public bool isOnPressurePlate = false;
    public GameObject pressurePlate;

    public bool isIntroSpeech = true;

    public bool canShoot = false;

    public float fireRate = 0.5f;
    public float lastShot = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (uiManager.scene.name != "StartScene")
        {
            interactor = rightHand.GetComponent<XRRayInteractor>();
        }

        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        
        List<InputDevice> devices2 = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices2);

        foreach (var item in devices)
        {
            //Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        if (devices2.Count > 0)
        {
            targetDevice2 = devices2[0];
        }

       
        //if (gameManager.tutorialOfGameScene1 || gameManager.gameScene)
        //{
        //    StartCoroutine(Shoot(0.5f));
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.startScene && gameManager.gameScene2)
        {
            if (isIntroSpeech)
            {
                gameManager.locomotionSystem.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 0;
            }
            else
            {
                gameManager.locomotionSystem.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 2.5f;
            }
        }

         targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

         //Check if any object is grabbed - To hide gun when object is grabbed and show when nothing is being grabbed
         if (uiManager.scene.name == "GameScene" || gameManager.tutorialOfGameScene1)
         {
             if (interactor.selectTarget != null)
             {
                 gun.SetActive(false);
             }
             else if (interactor.selectTarget == null)
             {
                 gun.SetActive(true);
             }
             
             if (triggerValue > 0.1f)
             {
                isShooting = true;
                if (!UIManager.isHover && !UIManager.isClicking && interactor.selectTarget == null)
                {
                    Fire();
                }
             }
             else
             {
                 isShooting = false;
             }
         }
         else if (uiManager.scene.name == "Game2Scene")
         {
             targetDevice.TryGetFeatureValue(CommonUsages.grip, out float triggerValue2);
             targetDevice2.TryGetFeatureValue(CommonUsages.grip, out float triggerValue3);
             
             RaycastHit hit;

             if (triggerValue3 > 0.1f)
             {
                 if (Physics.Raycast(interactor.transform.position, interactor.transform.forward, out hit, 100f))
                 {
                     Debug.DrawRay(interactor.transform.position, interactor.transform.right, Color.green);
                     if (hit.transform.tag == "SumCube")
                     {
                         hit.transform.GetComponent<SumCubeObject>().isGrabbed = true;
                     }
                 }
             }

             if (triggerValue2 > 0.1f)
             {
                 if (Physics.Raycast(interactor2.transform.position, interactor2.transform.forward, out hit, 100f))
                 {
                     if (hit.transform.tag == "SumCube")
                     {
                         hit.transform.GetComponent<SumCubeObject>().isGrabbed = true;
                     }
                 }
             }

             if (triggerValue2 < 0.1f && triggerValue3 < 0.1f)
             {
                 foreach (var cube in GameObject.FindGameObjectsWithTag("SumCube"))
                 {
                     cube.GetComponent<SumCubeObject>().isGrabbed = false;
                 }
             }

             if (interactor.selectTarget != null)
             {
                 hasCubeInHandLeft = true;
             }
             else if (interactor.selectTarget == null)
             {
                 hasCubeInHandLeft = false;
             }

             if (interactor2.selectTarget != null)
             {
                 hasCubeInHandRight = true;
             }
             else if (interactor2.selectTarget == null)
             {
                 hasCubeInHandRight = false;
             }
         }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "PressurePlate")
    //    {
    //        isOnPressurePlate = true;
    //        pressurePlate = other.gameObject;
    //    }
    //}

    public void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject projectile = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            lastShot = Time.time;
        }
    }

    IEnumerator Shoot(float time)
    {
        while (true)
        {
            if (!UIManager.isHover && !UIManager.isClicking && interactor.selectTarget == null)
            {
                if (isShooting)
                {
                    
                    yield return new WaitForSeconds(time);
                }
                else
                {
                    yield return new WaitForSeconds(0);
                }   
            }
            else if(UIManager.isHover || UIManager.isClicking || interactor.selectTarget != null)
            {
                yield return new WaitForSeconds(0);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collider_Sum")
        {
            Destroy(other.gameObject);
            gameManager.questionNumber++;

            if (gameManager.gameScene2)
            {
                if (gameManager.questionNumber == 2)
                {
                    gameManager.SetCubesForNewSum(gameManager.spawnLocations_2);
                }
                else if (gameManager.questionNumber == 3)
                {
                    gameManager.SetCubesForNewSum(gameManager.spawnLocations_3);
                }
            }
            else if (gameManager.gameScene)
            {
                uiManager.sumCalculated = false;

                uiManager.GameScreen();
            }
        }
    }
}
