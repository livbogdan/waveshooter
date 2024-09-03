using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = false;
    }



    #if UNITY_EDITOR || UNITY_STANDALONE
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    #elif UNITY_ANDROID || UNITY_IOS
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = Input.GetTouch(0).position;
        }
    }
    #endif
}

