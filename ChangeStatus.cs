using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeStatus : MonoBehaviour
{

public sbyte changeTo;


// 0 = Missed , 1 = Bad, 2 = Good, 3 = Perfect

    void OnTriggerEnter2D(Collider2D col)
    {
	//audi.PlayOneShot(clips[col.gameObject.GetComponent<MoveDown>().soundID]);
	col.gameObject.GetComponent<MoveDown>().status = changeTo;


        
    }


}
