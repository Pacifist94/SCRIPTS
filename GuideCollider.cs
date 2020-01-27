using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideCollider : MonoBehaviour
{
    public GameObject[] GuideRefs = new GameObject[2];
    public sbyte counter ;
    public float Local_Separation;

    void Start()
    {
        counter = 0;

        Local_Separation = Spawner._instance.SEPARATION;

        GuideRefs[0].transform.localScale = new Vector3(1, Local_Separation, 1);
        GuideRefs[1].transform.localScale = new Vector3(1, Local_Separation, 1);

        GuideRefs[1].transform.position = new Vector3(GuideRefs[1].transform.position.x, GuideRefs[0].transform.position.y + Local_Separation, 1);

        StartCoroutine(CallInitializer());
    }

    IEnumerator CallInitializer()
    {
        yield return new WaitForSeconds(4); 
        Spawner._instance.SpawnObjectInitial();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //is not about time but about position(collison)
        //need to spawn in advance FIRST based on postion 

        Spawner._instance.SpawnObject(GuideRefs[counter].transform.position.y);

        GuideRefs[counter].transform.position += new Vector3(0, (2 * Local_Separation), 0); 

        //change count to alternate between guide movement
        if (counter == 0)
        {
         
            counter = 1;
        }
        else
        {
            counter = 0;
        }
    }
}
