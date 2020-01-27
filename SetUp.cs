using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    public GameObject Spawner ;
    public GameObject DELIMITER;
    
   // public GameObject MyCollider;

    // Use this for initialization
    void Start()
    {
        float height = float.Parse(Screen.height.ToString());
        float width = float.Parse(Screen.width.ToString());
        float V_Units = height / width * 5.0943396226415094339622641509434f;


        Camera.main.transform.position = new Vector3(0, V_Units / 2, -10);
        Camera.main.orthographicSize = V_Units / 2;
        Spawner.transform.position = new Vector3(0, V_Units * 5);
        DELIMITER.transform.position = new Vector3(-5, (V_Units * 5) + 30f);

      //  MyCollider.transform.position = new Vector3(3, V_Units + 0.5f);
    }
}