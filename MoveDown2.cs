using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveDown2 : MonoBehaviour
{
    public float speedScale;
    public sbyte soundID;

    public Collider2D col2d;


//-------------------


 
//--------

// 0 = Missed , 1 = Bad, 2 = Good, 3 = Perfect

    // Start is called before the first frame update
    void Start()
    {
        speedScale = ReadersSpawnerGuideCollider._instance.SPEED;
        
        col2d = GetComponent<Collider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( speedScale * Vector3.down * Time.deltaTime);

	}
}
 



















