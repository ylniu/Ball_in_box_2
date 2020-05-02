using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newton;

public class MoveController : MonoBehaviour
{
    public Vector3 x;
    public Vector3 v;
    //--------------------------------------------------------------------------
    // Start is called before the first frame update
    //
    void Start()
    {
        x = new Vector3();
        v = new Vector3();
        Debug.Log("debug-A" + x);
    }
    //--------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        Debug.Log("debug-B" + x);
    }
}
