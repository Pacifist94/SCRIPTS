using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELIMITER : MonoBehaviour
{
   
public GameObject GuideColRef;

public float AMOUNT_TO_MOVE = 1;
 




  void OnTriggerEnter2D(Collider2D col)
    {
  	
            GuideColRef.transform.position = new Vector3(4, GuideColRef.transform.position.y -AMOUNT_TO_MOVE, 1);



    }



}
