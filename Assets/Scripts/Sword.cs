using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject cutPrefab;
    public float cutLifeTime;

    private bool dragging;
    private Vector2 swipeStart;

    void Start()
    {
    }

    void Update()
    {
       
#if UNITY_EDITOR
       
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            cutSpawner();
        }
#else
      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragging = true;
                swipeStart = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended && dragging)
            {
                dragging = false;
                cutSpawner();
            }
        }
#endif
    }

    private void cutSpawner()
    {
        Vector2 swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            swipeEnd = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
#endif

        GameObject cutInstance = Instantiate(cutPrefab, swipeStart, Quaternion.identity);
        cutInstance.GetComponent<LineRenderer>().SetPosition(0, swipeStart);
        cutInstance.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);

        Vector2[] collidePoint = new Vector2[2];
        collidePoint[0] = Vector2.zero;
        collidePoint[1] = swipeEnd - swipeStart;

        cutInstance.GetComponent<EdgeCollider2D>().points = collidePoint;
        Destroy(cutInstance, cutLifeTime);
    }
}
