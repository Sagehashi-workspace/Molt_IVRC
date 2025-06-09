using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AddEffect : MonoBehaviour
{
    [SerializeField] private float Effect_Threshold = 0.9f;
    private bool check_peel = false;
    private float Acc_Velocity = 1.0f;

    ParticleSystem m_ParticleSystem;
    private ParticleSystem.Particle[] m_Particles;
    float m_TotalTime = 0;
    [SerializeField]private float m_Speed = 1;
    Vector3 tmp;

    // Start is called before the first frame update
    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_ParticleSystem.Stop();  
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("SetVelocity" + Acc_Velocity);
        if (check_peel)
        {
            m_ParticleSystem.Play();
        }

        int maxParticles = m_ParticleSystem.main.maxParticles;
        if (m_Particles == null || m_Particles.Length < maxParticles)
        {
            m_Particles = new ParticleSystem.Particle[maxParticles];
        }

        int particleNum = m_ParticleSystem.GetParticles(m_Particles);

        //All perticle
        for (int i = 0; i < particleNum; i++)
        {
            tmp = m_Particles[i].position;
            tmp.x += Mathf.Cos(m_TotalTime * i) * m_Speed;
            m_Particles[i].position = tmp;
        }
        m_ParticleSystem.SetParticles(m_Particles, particleNum);

        m_TotalTime += Time.deltaTime;

        Debug.Log(Acc_Velocity);

        if (Acc_Velocity > Effect_Threshold) {
            check_peel = true;
        }
        else
        {
            check_peel = false;
        }
    }

    public float SetAcc_Velocity
    {
        get { return Acc_Velocity; }
        set { Acc_Velocity = value; }
    }
}
