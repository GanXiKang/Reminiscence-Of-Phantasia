using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGardenControl_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip plant;

    [Header("Now")]
    public GameObject treeCherry_Now;
    public GameObject treeGrape_Now;
    
    [Header("Future")]
    public GameObject treeCherry_Future;
    public GameObject treeGrape_Future;

    [Header("Past")]
    public GameObject treeKid;
    public GameObject plantingEF;
    public GameObject interactableEF;
    public static bool isPlanting = false;
    public static bool isDigPlant = false;
    public static bool isCherryTree = false;
    public static bool isGrapeTree = false;

    void Update()
    {
        treeCherry_Future.SetActive(isCherryTree);
        treeGrape_Future.SetActive(isGrapeTree);
        treeCherry_Now.SetActive(isCherryTree);
        treeGrape_Now.SetActive(isGrapeTree);

        if (StoryInteractableControl_Prince.isSwallowHunger && StoryBagControl.isItemNumber[12] && StoryBagControl.isItemNumber[13])
            interactableEF.SetActive(true);

        if (isPlanting)
        {
            isPlanting = false;
            StartCoroutine(Planting());
        }
    }

    IEnumerator Planting()
    {
        interactableEF.SetActive(false);
        plantingEF.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        print("1");
        BGM.PlayOneShot(plant);
        treeKid.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        plantingEF.SetActive(false);
    }
}
