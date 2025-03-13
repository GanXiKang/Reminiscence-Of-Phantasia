using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGardenControl_Prince : MonoBehaviour
{
    [Header("Now")]
    public GameObject treeCherry_Now;
    public GameObject treeGrape_Now;
    
    [Header("Future")]
    public GameObject treeCherry_Future;
    public GameObject treeGrape_Future;

    [Header("Past")]
    public GameObject treeKid;
    public GameObject effects;
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
    }
}
