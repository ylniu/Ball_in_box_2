using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newton;

public class BallController : MonoBehaviour
{
    private float dt = 0.001f;
    private Vector3 Pos;
    private Vector3 Vel;
    private Vector3[] Posarray;
    private Vector3[] Velarray;
    private ArrayList balls;
    private int i1;
    private int nball=4;
    //private var posStore = new List<Vector3>();
    //--------------------------------------------------------------------------
    private Vector6 Concatenate(Vector3 Pos, Vector3 Vel)
    {
        Vector6 XV;
        XV.x = Pos.x;
        XV.y = Pos.y;
        XV.z = Pos.z;
        XV.vx = Vel.x;
        XV.vy = Vel.y;
        XV.vz = Vel.z;
        return XV;
    }
    //--------------------------------------------------------------------------
    Vector6 NewXV(Vector3 X, Vector3 V, float dt)
    {
        Vector3 a = new Vector3(0.0f, -10.0f, 0.0f);
        //----------------------------------------------------------------------
        V.x = V.x + a.x * dt / 2.0f;
        V.y = V.y + a.y * dt / 2.0f;
        V.z = V.z + a.z * dt / 2.0f;
        //----------------------------------------------------------------------
        X.x = X.x + V.x * dt;
        X.y = X.y + V.y * dt;
        X.z = X.z + V.z * dt;
        //----------------------------------------------------------------------
        V.x = V.x + a.x * dt / 2.0f;
        V.y = V.y + a.y * dt / 2.0f;
        V.z = V.z + a.z * dt / 2.0f;
        //----------------------------------------------------------------------
        if (X.y <= -0.0f)
        {
            V.y = -V.y * 0.95f;
        }
        return Concatenate(X, V);
    }
    Vector3 getPos(Vector6 XV)
    {
        Vector3 X = new Vector3(XV.x, XV.y, XV.z);
        return X;
    }
    Vector3 getVel(Vector6 XV)
    {
        Vector3 V = new Vector3(XV.vx, XV.vy, XV.vz);
        return V;
    }
    public GameObject Ballpref;
        
    // Start is called before the first frame update
    void Start()
    {
        dt = 0.05f;
        balls    = new ArrayList();
        Posarray = new Vector3[nball];
        Velarray = new Vector3[nball];
        int j = 0;
        for (int i = 0; i < nball; i++)
        {
            GameObject newBall = Instantiate(Ballpref);
            newBall.transform.parent = transform;
            j = 10 +i * 5;
            Pos= new Vector3(j, 20, 20);
            Vel = new Vector3(0,0,0);
            newBall.transform.position = Pos;
            Posarray[i]=newBall.transform.position;
            Velarray[i]=Vel;
            balls.Add(newBall.gameObject);
            Debug.Log("pos--0--" + i + Posarray[i]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < nball; i++)
        {
            Pos = Posarray[i];
            Vel = Velarray[i];
            Debug.Log("pos--1--" + i + Posarray[i]);
            Vector6 XV = NewXV(Pos, Vel, dt);
            Pos = getPos(XV);
            Vel = getVel(XV);
            Posarray[i] = Pos;
            Velarray[i] = Vel;
            Debug.Log("pos--2--" + i + Posarray[i]);
        }
        i1 = 0;
        foreach (GameObject ball in balls)
            balls[i1].transform.position = Posarray[i1];
        i1++;
        {
        }
    }
}