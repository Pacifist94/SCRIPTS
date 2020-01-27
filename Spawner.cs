using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{

    public GameObject[] prefabs = new GameObject[5];

    public float SPEED = 1; // x     | 1 default 
    //public float spawnRate ;// AutoCalc 1/x
    public float SEPARATION = 1;// | 1 default Scaler the speed on MOVEDOWN


    public sbyte INITIAL_AMOUNT = 20; // Objects to Spawn


    


    // spawnScale (SPEED): Its teh SPEED of the spawned objects (Does not affect SEPARATION)
    // SPEED_SCALAR (SEPARATION): It is the SEPARATION between the Spawned objects (DOES NOT AFFECT THE SPEED) 

    public int counter = 0;
    public float offset = 0;

    public sbyte prefab_id;
    public sbyte position;
    //sbyte isSameHeight; // Spaces apart from previous spawned
    public sbyte sound_id;

    public GameObject[] GuideRefs = new GameObject[2];
    public GameObject GuideColRef;
    
    //Singleton
    #region SINGLETON

    public static Spawner _instance;

    public static Spawner Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Debug.Log("Grapes ");
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    MoveDown md;


    //--

    // public const string LAYER_NAME = "TopLayer";
    //public int sortingOrder = 0;
    private SpriteRenderer sprite;


    private void Start()
    {

     LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));
        
         


        switch (LL.Level)
        {
            case 1:
                SPEED = 6 ;
                SEPARATION = 1f;
                INITIAL_AMOUNT = 1;
            break;
            case 2:
                SPEED = 8;
                SEPARATION = 1f;
                INITIAL_AMOUNT = 1;
                break;
             
            case 3:
                SPEED = 6.5F ;
                SEPARATION = 1f;
                INITIAL_AMOUNT = 1;
                break;
            
            case 4:
                SPEED = 10F ;
                SEPARATION = 1f;
                INITIAL_AMOUNT = 1; 
                break;

            default:

            break;
        }
        
    }

    public void SpawnObjectInitial()
    {



        for (int i = 0; i < INITIAL_AMOUNT; i++)
        {
         
            // Offset Accrued

            offset += Readers._instance.n[counter].isSameHeight * SEPARATION;
            
                
            //Assign values to variables from Readers
            prefab_id = Readers._instance.n[counter].prefab_id;
            position = Readers._instance.n[counter].position;
            sound_id = Readers._instance.n[counter].sound_id;


            //Refer to SoundID
            md = prefabs[prefab_id].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;

            md.soundID = sound_id; 

            //Refer to SpriteRenderer
            sprite = prefabs[prefab_id].gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            if (sprite)
            {
                sprite.sortingOrder = -counter;
            }
            

            Instantiate(prefabs[prefab_id], new Vector3(position, (counter * SEPARATION) + transform.position.y + offset, 0), Quaternion.identity);

            if (counter== (INITIAL_AMOUNT-1))
            {
              //  SET Guides Position
                GuideRefs[0].transform.position = new Vector3(1, (counter * SEPARATION) + transform.position.y + offset + SEPARATION, 1);
                GuideRefs[1].transform.position = new Vector3(1, GuideRefs[0].transform.position.y + SEPARATION, 1);

                GuideColRef.transform.position = new Vector3(1, GuideRefs[0].transform.position.y - SEPARATION, 1);
                
                //Restart Offset
                offset = 0;
            }


            counter++;
        }


                
    }


        public void SpawnObject(float positionY)
    {
        if (counter < Readers._instance.n.Length)
        {
            
            // 
           
            // Offset Accrued
            offset += Readers._instance.n[counter].isSameHeight * SEPARATION;// / SPEED_SCALAR IS NOT HOW SEPARATED WILL BE FROM EACH OTHER;
             

            //Assign values to variables from Readers
            prefab_id = Readers._instance.n[counter].prefab_id;
            position = Readers._instance.n[counter].position;
            sound_id = Readers._instance.n[counter].sound_id;


            //Refer to SoundID
            md = prefabs[prefab_id].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;


            md.soundID = sound_id;

            //Refer to SpriteRenderer
            sprite =  prefabs[prefab_id].gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            if (sprite)
            {
                sprite.sortingOrder =  - counter;
            }
            /**/

            Instantiate(prefabs[prefab_id], new Vector3(position, positionY + offset, 0), Quaternion.identity);




            counter++;



        }
        
    }


}
























