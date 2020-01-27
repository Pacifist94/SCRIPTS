using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENABLE_CALC : MonoBehaviour
{  



 void OnTriggerEnter2D(Collider2D col)
    {
	//audi.PlayOneShot(clips[col.gameObject.GetComponent<MoveDown>().soundID]);
	col.gameObject.GetComponent<MoveDown>().isCalculable = true;


        
    }
}
