using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newton;

public class BallController : MonoBehaviour
{
    private int nball;
    private float dt;
    private Vector3[] X;
    private Vector3[] V;
    private List<GameObject> balls;
    private List<MoveController> ballscripts;
    public GameObject Ballpref;
    //--------------------------------------------------------------------------
    void update_xv(int N, Vector3[] X, Vector3[] V, float dt)
    {
        // C# 函数2 (传值与传址)
        // https://www.cnblogs.com/mdnx/archive/2012/09/04/2671060.html
        for (int i=0; i<N; i++)
        { 
            Vector3 a = new Vector3(0.0f, -10.0f, 0.0f);
            //------------------------------------------------------------------
            V[i].x = V[i].x + a.x * dt / 2.0f;
            V[i].y = V[i].y + a.y * dt / 2.0f;
            V[i].z = V[i].z + a.z * dt / 2.0f;
            //------------------------------------------------------------------
            X[i].x = X[i].x + V[i].x * dt;
            X[i].y = X[i].y + V[i].y * dt;
            X[i].z = X[i].z + V[i].z * dt;
            //------------------------------------------------------------------
            V[i].x = V[i].x + a.x * dt / 2.0f;
            V[i].y = V[i].y + a.y * dt / 2.0f;
            V[i].z = V[i].z + a.z * dt / 2.0f;
            //------------------------------------------------------------------
            if (X[i].y <= -0.0f)
            {
                V[i].y = -V[i].y * 0.98f;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dt        = 0.05f;
        nball     = 4;
        balls     = new List<GameObject>();
        ballscripts = new List<MoveController>();
        
        X = new Vector3[nball];
        V = new Vector3[nball];
        for (int i = 0; i < nball; i++)
        {
            GameObject newBall = Instantiate(Ballpref);
            MoveController ballscript = newBall.GetComponent<MoveController>();
            newBall.transform.parent = transform;
            ballscript.x = new Vector3(10.0f + i * 5.0f, 20.0f, 20.0f);
            ballscript.v = new Vector3(0.0f,0.0f,0.0f);
            newBall.transform.position = ballscript.x;
            balls.Add(newBall.gameObject);
            ballscripts.Add(ballscript);
            Debug.Log("debug-0" + i + ballscripts[i].x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < nball; i++)
        {
            Debug.Log("debug-1"+ i + ballscripts[i].x);
            X[i] = ballscripts[i].x;
            V[i] = ballscripts[i].v;
        }
        update_xv(nball, X, V, dt);
        for (int i = 0; i < nball; i++)
        {
            ballscripts[i].x = X[i];
            ballscripts[i].v = V[i];
        }
        for (int i = 0; i < nball; i++)
        {
            balls[i].transform.position = ballscripts[i].x;
        }
    }
}