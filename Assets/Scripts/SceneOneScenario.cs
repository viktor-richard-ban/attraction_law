using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class SceneOneScenario : MonoBehaviour
{
    public GameObject character;
    public GameObject fairy;
    public GameObject firstScript;
    public GameObject secondScript;
    public GameObject thirdScript;
    public GameObject fourthScript;
    public GameObject fifthScript;
    public GameObject sixthScript;
    public GameObject seventhScript;
    public GameObject eightScript;
    public GameObject ninthScript;
    public GameObject barrel;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    public GameObject door;

    public GameObject camera;
    private long lastDialogShowedAt;
    private float offset = 2.0f;
    private bool sleep = true;
    private bool isFairyLeft = false;

    void Start()
    {
        lastDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        firstScript.SetActive(true);
    }

    void Update()
    {
        Vector3 offsetPos = character.transform.position;
        offsetPos.y += offset;

        long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        long delta = now - lastDialogShowedAt;
        if (sleep)
        {
            if (delta < 3000)
            {
                firstScript.transform.position = offsetPos;
            }

            if (delta > 3000 && delta < 3100)
            {
                firstScript.SetActive(false);
                secondScript.SetActive(true);
            }

            secondScript.transform.position = offsetPos;

            if (delta > 6000 && delta < 6100)
            {
                secondScript.SetActive(false);
                thirdScript.SetActive(true);
            }

            if (delta > 7000 && delta < 7100)
            {
                fourthScript.SetActive(true);
            }
        }
        else if (!isFairyLeft)
        {
            fifthScript.SetActive(false);
            secondScript.SetActive(false);
            thirdScript.SetActive(false);
            fourthScript.SetActive(false);

            if (delta < 3000)
            {
                fifthScript.SetActive(true);
            }

            if (delta > 6000 && delta < 6100)
            {
                fifthScript.SetActive(false);
                sixthScript.SetActive(true);
                character.GetComponent<PlayerController>().isSticky = true;
            }

            if (delta > 7000 && delta < 7100)
            {
                sixthScript.SetActive(false);
                seventhScript.SetActive(true);
            }

            if (delta > 9000 && delta < 9100)
            {
                seventhScript.SetActive(false);
                camera = GameObject.FindWithTag("MainCamera");
                Vector3 cameraPos = camera.transform.position;
                cameraPos.x = fairy.transform.position.x;
                cameraPos.y = fairy.transform.position.y;
                camera.transform.position = cameraPos;
                camera.transform.SetParent(fairy.transform);
            }

            if (delta > 9500 && delta < 9600)
            {
                fairy.GetComponent<FairyController>().SetTarget(target1);
            }

            if (Vector3.Distance(target1.transform.position, fairy.transform.position) < 1f)
            {
                fairy.GetComponent<FairyController>().SetTarget(target2);
                door.SetActive(false);
            }


            if (Vector3.Distance(target2.transform.position, fairy.transform.position) < 1f)
            {
                fairy.GetComponent<FairyController>().SetTarget(target3);
                door.SetActive(true);
            }


            if (Vector3.Distance(target3.transform.position, fairy.transform.position) < 1f)
            {
                Debug.Log("Fairy Collided");
                Vector3 cameraPos = camera.transform.position;
                cameraPos.x = character.transform.position.x;
                cameraPos.y = character.transform.position.y;
                fairy.SetActive(false);
                isFairyLeft = true;
                camera.transform.position = cameraPos;
                camera.transform.SetParent(character.transform);

                lastDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }
        }

        if (isFairyLeft)
        {
            if (delta < 3000)
                eightScript.SetActive(true);


            if (delta > 4000 && delta < 4100)
                ninthScript.SetActive(true);


            if (delta > 7000 && delta < 7100)
            {
                SceneManager.LoadScene(2);
                Scene nextScene = SceneManager.GetSceneByName("House");
                if (nextScene.isLoaded)
                {
                    Scene active = SceneManager.GetActiveScene();
                    SceneManager.UnloadSceneAsync(active.buildIndex);
                    SceneManager.SetActiveScene(nextScene);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with " + other.gameObject.name);
        if (sleep && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player went to sleep!");
            firstScript.SetActive(false);
            secondScript.SetActive(false);
            fourthScript.SetActive(false);
            lastDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            fairy.SetActive(true);
            Vector3 xpos = character.transform.position;
            xpos.y = barrel.transform.position.y;
            barrel.transform.position = xpos;
            sleep = false;
        }
    }
}