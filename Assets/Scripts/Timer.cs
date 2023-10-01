using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float timer = 0;
    private Coroutine coroutine;
    private void Start()
    {
        FindObjectOfType<PlayerController>().actionMoveStart += StartCount;
        FindObjectOfType<PlayerController>().actionMoveEnd += StopCount;

    }
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    Debug.Log(timer);
    //}

    IEnumerator Timer1()
    {
        //while (true)
        //{
        timer += Time.deltaTime;
        Debug.Log(timer);
        yield return null;
        //}
    }
    void StartCount()
    {
        StartCoroutine(Timer1());
    }
    void StopCount()
    {
        StopCoroutine(Timer1());

    }
    public float ShowTime()
    {
        return timer;
    }
}
