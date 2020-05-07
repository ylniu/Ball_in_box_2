using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BallController : MonoBehaviour
{
    private int nball;
    private int ndyn;
    private float dt;
    private float vel;
    private float k;
    private float m;
    private float radius;
    private float diameter;
    private float force_radius;
    private Vector3[] X;
    private Vector3[] V;
    private Vector3[] F;
    private Vector3[,] F_pair;
    private Vector3[] a;
    private Vector3   e;
    private float[,] Fr;
    private float[,] D;
    private List<GameObject> balls;
    private List<MoveController> ballscripts;
    public GameObject Ballpref;
    //--------------------------------------------------------------------------
    void Initiate_params()
    {
        //----------------------------------------------------------------------
        // Initiation
        //
        nball = 2;
        ndyn = 20;
        dt = 0.005f;
        radius = 5.0f;
        diameter = radius * 2.0f;
        k = 0.05f;
        m = 1.0f;
        force_radius = 2.0f * radius;
        //
        balls = new List<GameObject>();
        ballscripts = new List<MoveController>();

        e = new Vector3();
        X = new Vector3[nball];
        V = new Vector3[nball];
        a = new Vector3[nball];
        F = new Vector3[nball];
        F_pair = new Vector3[nball, nball];
        D = new float[nball, nball];
        Fr = new float[nball, nball];
    }
    void Get_xv()
    {
        for (int i = 0; i < nball; i++)
        {
            X[i] = ballscripts[i].x;
            V[i] = ballscripts[i].v;
        }
    }
    void Set_xv()
    {
        for (int i = 0; i < nball; i++)
        {
            ballscripts[i].x = X[i];
            ballscripts[i].v = V[i];
        }
    }
    void Update_position()
    {
        for (int i = 0; i < nball; i++)
        {
            balls[i].transform.position = ballscripts[i].x;
        }
    }
    void Initiate_xv()
    {
        vel = 1.0f;
        for (int i = 0; i < nball; i++)
        {
            float xvel = -(i - 0.5f) * vel;
            ballscripts[i].x = new Vector3(10.0f + i * 20.0f, 20.0f, 20.0f);
            ballscripts[i].v = new Vector3(xvel, 0.0f, 0.0f);
        }
    }
    void In_box()
    {
        for (int i = 0; i < nball; i++)
        {
            if (X[i].x <= 0.0f + radius || X[i].x >= 40.0f - radius)
            {
                V[i].x = -V[i].x;
            }
            //------------------------------------------------------------------
            if (X[i].y <= 0.0f + radius || X[i].y >= 40.0f - radius)
            {
                V[i].y = -V[i].y;
            }
            //------------------------------------------------------------------
            if (X[i].z <= 0.0f + radius || X[i].z >= 40.0f - radius)
            {
                V[i].z = -V[i].z;
            }
        }
    }
    void Distance()
    {
        for (int i = 0; i < nball; i++)
        {
            for (int j = i+1; j < nball; j++)
            {
                if (i == j)
                {
                    D[i, j] = 0.0f;
                }
                else
                {
                    D[i, j] = (X[i] - X[j]).magnitude;
                    D[j, i] = D[i, j];
                }
            }
        }
    }
    void Cal_force_pair()
    {
        for (int i = 0; i < nball - 1; i++)
        {
            for (int j = i + 1; j < nball; j++)
            {
                if (D[i, j] < force_radius)
                {
                    float dr = force_radius - D[i, j];
                    Fr[i, j] = k * dr;
                }
                else
                {
                    Fr[i, j] = 0.0f;
                }
                e = (X[i] - X[j]) / D[i, j];
                // The force act on i
                F_pair[i, j] = e * Fr[i, j];
                // The force act on j
                F_pair[j, i] = -F_pair[i, j];
            }
        }
    }
    void Cal_force()
    {
        Distance();
        Cal_force_pair();
        //----------------------------------------------------------------------
        for (int i = 0; i < nball; i++)
        {
            F[i] = new Vector3(0.0f, 0.0f, 0.0f);
            for (int j = 0; j < nball; j++)
            {
                if (j !=i && D[i, j] < force_radius)
                {
                    F[i] = F[i] + F_pair[i, j];
                }
            }
            a[i] = F[i] / m;
        }
        //----------------------------------------------------------------------
    }
    void Update_xv(int N, Vector3[] X, Vector3[] V, float dt, int ndyn, int itype)
    {
        //----------------------------------------------------------------------
        // C# 函数2 (传值与传址)
        // https://www.cnblogs.com/mdnx/archive/2012/09/04/2671060.html
        //
        for (int idyn=1; idyn<ndyn; idyn++)
        {
            Cal_force();
            string Fx = F[0].x.ToString();
            string distance = D[0, 1].ToString();
            // Debug.Log("Fx = " + Fx + " force_radius = " + force_radius + " Distance = "+ distance + "X0 = " + X[0].x + "X1 = " + X[1].x + " Forcex = "+F[0].x);
            //----------------------------------------------------------------------
            for (int i = 0; i < N; i++)
            {
                V[i].x = V[i].x + a[i].x * dt / 2.0f;
                V[i].y = V[i].y + a[i].y * dt / 2.0f;
                V[i].z = V[i].z + a[i].z * dt / 2.0f;
            }
            //----------------------------------------------------------------------
            for (int i = 0; i < N; i++)
            {
                X[i].x = X[i].x + V[i].x * dt;
                X[i].y = X[i].y + V[i].y * dt;
                X[i].z = X[i].z + V[i].z * dt;
            }
            //----------------------------------------------------------------------
            Cal_force();
            //----------------------------------------------------------------------
            for (int i = 0; i < N; i++)
            {
                V[i].x = V[i].x + a[i].x * dt / 2.0f;
                V[i].y = V[i].y + a[i].y * dt / 2.0f;
                V[i].z = V[i].z + a[i].z * dt / 2.0f;
                //------------------------------------------------------------------
            }
            In_box();
        }
     }
    // Start is called before the first frame update
    void Start()
    {
        Initiate_params();
        for (int i = 0; i < nball; i++)
        {
            GameObject newBall = Instantiate(Ballpref);
            newBall.transform.localScale = new Vector3(diameter, diameter, diameter);
            MoveController ballscript = newBall.GetComponent<MoveController>();
            balls.Add(newBall.gameObject);
            ballscripts.Add(ballscript);
        }
        Initiate_xv();
    }

    // Update is called once per frame
    void Update()
    {
        Get_xv();
        Update_xv(nball, X, V, dt, ndyn, 1);
        Set_xv();
        Update_position();
    }
}