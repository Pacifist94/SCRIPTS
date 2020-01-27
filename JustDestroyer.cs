using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDestroyer : MonoBehaviour
{

public int counter = 0;
 
  void OnTriggerEnter2D(Collider2D col)
    {
	//audi.PlayOneShot(clips[col.gameObject.GetComponent<MoveDown>().soundID]);

		ScoreManager._instance.SetActiveCombo(false);
    	ScoreManager._instance.ResetCombo();


		ScoreManager._instance.SpanwAtVector(0, col.gameObject.transform.position);


        Destroy(col.gameObject);

        counter++;
    }

}
