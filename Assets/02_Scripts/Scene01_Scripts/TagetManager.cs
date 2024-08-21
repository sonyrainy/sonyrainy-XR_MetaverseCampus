using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TagetManager : MonoBehaviour
{
    private int hitCount = 0;
    private float scaleFactor = 0.7f;
    public GameObject MapObject;
    public GameObject Compass;
    public GameObject LineObject;
    public GameObject UI;

    public GameObject Map;
    public GameObject compass;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("BULLET"))
            {
                 hitCount++;
                if(hitCount <= 4)
                {   
                    ReduceScale();
                }
                else
                {
                    Destroy(this.gameObject);
                    ActiveObject();
                }
                
                
            }
    }

    private void ActiveObject()
    {
        Compass.SetActive(true);
        MapObject.SetActive(true);
        LineObject.SetActive(false);
        UI.SetActive(true);
        Map.SetActive(true);
        compass.SetActive(true);
    }

    private void ReduceScale()
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = currentScale * scaleFactor; // 크기 축소
    }
}
