using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUIControl_House : MonoBehaviour
{
    [Header("DayUI")]
    public GameObject dayUI;
    public Image dayImage;
    public Sprite[] day;
    public Transform inPoint, outPoint;
    float _speed = 2f;
    bool isAppear = true;

    void Start()
    {
        StartCoroutine(DayUIMoveLoop());
        dayImage.sprite = day[GameControl_House._day];
    }

    IEnumerator DayUIMoveLoop()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 target = isAppear ? inPoint.position : outPoint.position;
            while (Vector3.Distance(dayUI.transform.position, target) > 0.01f)
            {
                dayUI.transform.position = Vector3.MoveTowards(dayUI.transform.position, target, _speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(5f);

            isAppear = !isAppear;
        }

        dayUI.SetActive(false);
    }
}
