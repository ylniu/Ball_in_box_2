﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newton;

public class BallController : MonoBehaviour
{
    private float dt = 0.001f;
    private Vector3 Pos;
    private Vector3 Vel;
    private ArrayList Posarray;
    private ArrayList Velarray;
    private ArrayList balls;
    private int i1;
    private var posStore = new List<Vector3>();
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
        Posarray = new ArrayList();
        Velarray = new ArrayList();
        for (int i = 0; i < 2  ; i++)
        {
            GameObject newBall = Instantiate(Ballpref);
            newBall.transform.parent = transform;
            Pos= new Vector3(20 + i * 5, 20, 20);
            Vel = new Vector3(0,0,0);
            newBall.transform.position = Pos;
            Posarray.Add(newBall.transform.position);
            Velarray.Add(Vel);
            balls.Add(newBall.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            Pos = Posarray[i];
            Vel = Velarray[i];
            Vector6 XV = NewXV(Pos, Vel, dt);
            Pos = getPos(XV);
            Vel = getVel(XV);
            Posarray[i] = Pos;
            Velarray[i] = Vel;
        }
        i1 = 0;
        foreach (GameObject ball in balls)
            ball.transform.position = Posarray[i1];
            i1++;
        {
        }
    }
}