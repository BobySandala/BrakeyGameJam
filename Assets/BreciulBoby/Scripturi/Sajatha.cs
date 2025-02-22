using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sajatha : MonoBehaviour
{
    public float f_Speed;
    public float f_LifeTime = 10;

    [SerializeField]
    private float f_Angle;
    [SerializeField]
    private Vector3 v3_Direction;

    private float f_dreapta;
    private float f_inainte;
    private float f_SpawnTime;
    private void Start()
    {
        v3_Direction = Vector3.zero;
        f_SpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > f_SpawnTime + f_LifeTime) { Destroy(gameObject); }
        print("unkle: " + transform.rotation.eulerAngles.ToString());
        f_Angle = transform.rotation.eulerAngles.y;

        f_dreapta = Mathf.Sin(f_Angle * Mathf.Deg2Rad);
        f_inainte = Mathf.Cos(f_Angle * Mathf.Deg2Rad);

        v3_Direction.x = f_dreapta;
        v3_Direction.z = f_inainte;

        transform.position += v3_Direction * f_Speed * Time.deltaTime;
    }
}
