using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class ReadersSpawnerGuideCollider: MonoBehaviour
{
//###############################################################
//###############################################################
    //Singleton
    #region SINGLETON
        public static ReadersSpawnerGuideCollider _instance;

        public static ReadersSpawnerGuideCollider Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    #endregion


    public struct note
    {   
        // sbyte range [128 to byte 127]
        public sbyte prefab_id; // id of prefab(Hold Tap)  (1, 2, 3...)
        public sbyte position; // position.x of prefab  (-2, -1..... 2)
        public int isSameHeight; // if is same heigh will spawn 
        public sbyte sound_id; // if of sound to play
    }
    
    //Array of Structs 
    public note[] n = new note[1];// SET TO amount of notes i.e. (<prefab_id98>1</prefab_id98> = 98)

    /* THINGS TO CHANGE*/
    // public note[] n = new note[42] to (<prefab_id98>1</prefab_id98> = 98)
    // Cood [392] = lines of code  ie = 98*4 =392
    //m_coord = new  string[560]; // Check from Inspector!!!! // set to the amount of lines * 4  ie = 98*4 =392
    //public XmlNode[560] m_node
        // Limit [391] =  ie = 98*4 =392 - 1 = 391


    //LEVEL NUMBER
    public int LevelNumber = 1;

    //TextAsset Reference
    public TextAsset xmlRawFile;


    //Arrays of NODES & COORDINATES/ 
    public string[] m_coord = new  string[1]; // Check from Inspector!!!! // set to the amount of lines * 4  ie = 98*4 =392
    public XmlNode[] m_nodes = new XmlNode[1] ;


    //LIMIT OF ITERATIONS WHEN PARSING DATA ACCORDING TO LEVEL LOADED 
    // !!!!!!!!!! MUST BE SET TO THE (AMOUNT OF LINES TO READ - 1) ie 320 line then 319 
    public int Limit = 320;  // Change from Inspector!!!!

    //STRING THAT CONTAINS TXT DATA
    public string data;

    //COUNTER FOR THE NESTED FOR LOOP
    public int r_counter = 0;

    public int[] levelSize = new int[1]; // Defaults 140, 294

   // public float[] levelSPEED = new float[1]; // Defaults
    //public float[] levelSEPARATION = new float[1]; // Defaults 
    //public sbyte[] levelINITIAL_AMOUNT = new sbyte[1]; // Defaults 

//###############################################################
//###############################################################
        public GameObject[] prefabs = new GameObject[5];

    public float SPEED = 1; // x     | 1 default 
    //public float spawnRate ;// AutoCalc 1/x
    public float SEPARATION = 1;// | 1 default Scaler the speed on MOVEDOWN


    //public sbyte INITIAL_AMOUNT = 20; // Objects to Spawn


    


    // spawnScale (SPEED): Its teh SPEED of the spawned objects (Does not affect SEPARATION)
    // SPEED_SCALAR (SEPARATION): It is the SEPARATION between the Spawned objects (DOES NOT AFFECT THE SPEED) 

    public int s_counter = 0;
    public float offset = 0;
//public float EXTRA_OFFSET = 0;
//public float AMOUNT_TO_MOD ;
//public float AMOUNT_TO_MOVE ;

//public float HEIGHT;

    public sbyte prefab_id;
    public sbyte position;
    //sbyte isSameHeight; // Spaces apart from previous spawned
    public sbyte sound_id;

    public GameObject[] GuideRefs = new GameObject[2];
    public GameObject GuideColRef;
    
MoveDown Guide_MD1;
MoveDown Guide_MD2;
   
    MoveDown md;
  MoveDown2 md2;

    //--

    // public const string LAYER_NAME = "TopLayer";
    //public int sortingOrder = 0;
    private SpriteRenderer sprite;

//###############################################################
//###############################################################
   //REMOVED public GameObject[] GuideRefs = new GameObject[2];
    public sbyte g_counter ;
    public float Local_Separation;


    void Start()
    {

//		EXTRA_OFFSET= 0f;
		

        LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));
        
        LevelNumber = LL.Level;

        int LevelNumberLocal = LevelNumber; // 1 , 2
        int LevelSizeLocal = levelSize[LevelNumberLocal - 1]; // 140, 294

       // Spawner._instance.SPEED = levelSPEED[LevelNumberLocal -1];
      //  Spawner._instance.SEPARATION = levelSEPARATION[LevelNumberLocal -1 ];
      //  Spawner._instance.INITIAL_AMOUNT = levelINITIAL_AMOUNT[LevelNumberLocal - 1];


        //Set Array Size Dynamically
         n = new note[LevelSizeLocal]; // SET TO amount of notes i.e. (<prefab_id98>1</prefab_id98> = 98)
         m_coord = new string[LevelSizeLocal * 4]; // Check from Inspector!!!! // set to the amount of lines * 4  ie = 98*4 =392
         m_nodes = new XmlNode[LevelSizeLocal * 4];
         Limit = (LevelSizeLocal * 4) -1;


        //Reference to TXT file
        data = xmlRawFile.text;

        //Call Parser
        parseXmlFile(data);

        //Fill Stucture
        FillStruct();



//###############################################################

        
//         AMOUNT_TO_MOD = 20;
//		 AMOUNT_TO_MOVE = 5;

        switch (LL.Level)
        {
            case 1:
                SPEED = 6 ;
                SEPARATION = 1f;
               // INITIAL_AMOUNT = 2;
            break;
            case 2:
                SPEED = 8;
                SEPARATION = 1f;
                //INITIAL_AMOUNT = 2;
                break;
             
            case 3:
                SPEED = 6.5F ;
                SEPARATION = 1f;
                //INITIAL_AMOUNT = 2;
                break;
            
            case 4:
                SPEED = 10F ;
                SEPARATION = 1f;
                //INITIAL_AMOUNT = 1; 
                break;

            default:

            break;
        }


              //  SET Guides Position
        //Debug.Log("transform.position.y " + transform.position.y);
                GuideRefs[0].transform.position = new Vector3(4,  transform.position.y , 1);
                GuideRefs[1].transform.position = new Vector3(4, GuideRefs[0].transform.position.y + SEPARATION, 1);

                GuideColRef.transform.position = new Vector3(4, GuideRefs[0].transform.position.y - SEPARATION, 1);
                
                //Restart Offset
                offset = 0;



		Guide_MD1 = GuideRefs[0].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;
		Guide_MD2 = GuideRefs[1].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;

EnableScripts(true);


//###############################################################
        g_counter = 0;

        Local_Separation = SEPARATION;

        GuideRefs[0].transform.localScale = new Vector3(1, Local_Separation, 1);
        GuideRefs[1].transform.localScale = new Vector3(1, Local_Separation, 1);

        GuideRefs[1].transform.position = new Vector3(GuideRefs[1].transform.position.x, GuideRefs[0].transform.position.y + Local_Separation, 1);

        /*
        StartCoroutine(CallInitializer());
*/

    }

public void EnableScripts(bool stats){

	Guide_MD1.enabled = stats;
Guide_MD2.enabled = stats;
}



    public void FillStruct()
    {
        int r_counter = 0;

 

        //                  ARRAY SIZE note[] n = 20
        for (int i = 0; i < n.Length; i++)
        {
            n[i].prefab_id = sbyte.Parse( m_coord[r_counter]);
            n[i].position = sbyte.Parse(m_coord[r_counter + 1]);
            n[i].isSameHeight = int.Parse(m_coord[r_counter + 2]);
            n[i].sound_id = sbyte.Parse( m_coord[r_counter + 3]);
             
            r_counter += 4;
       
            //Debug.Log(n[i].prefab_id + "|" + n[i].position + "|" + n[i].isSameHeight + "|" + n[i].sound_id);

        }


    }

    void parseXmlFile(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//levels/level" + LevelNumber.ToString();
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        
        foreach (XmlNode node in myNodeList)
        {
            // Initialize rest
            // First Call Iteration 0-99
            for (int i = 0; i <= Limit; i++)
            {



                if (i == 0 && r_counter == 0)// Only happens the very first time
                {
                    //XML NODE[0] = First
                    m_nodes[0] = node.FirstChild;

                    //Array[0] = XML NODE[0]  /// used as 0, on next call value will be 100
                    m_coord[0] = m_nodes[r_counter].InnerXml;

                }
                else
                {
                    m_nodes[i] = m_nodes[r_counter - 1].NextSibling;

                    m_coord[i] = m_nodes[r_counter].InnerXml;
                }
                r_counter++;// increase counter
            }
        }
    }

//########################################
//########################################
/*
      public void SpawnObjectInitial()
    {



        for (int i = 0; i < INITIAL_AMOUNT; i++)
        {
         
            // Offset Accrued

            offset += (n[s_counter].isSameHeight * SEPARATION) ;
            
                
            //Assign values to variables from Readers
            prefab_id = n[s_counter].prefab_id;
            position = n[s_counter].position;
            sound_id = n[s_counter].sound_id;


            //Refer to SoundID
            md = prefabs[prefab_id].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;

            md.soundID = sound_id; 

            //Refer to SpriteRenderer
            sprite = prefabs[prefab_id].gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            if (sprite)
            {
                sprite.sortingOrder = -s_counter;
            }
            

            Instantiate(prefabs[prefab_id], new Vector3(position, (s_counter * SEPARATION) + transform.position.y + offset+ EXTRA_OFFSET, 0), Quaternion.identity);

            if (s_counter== (INITIAL_AMOUNT-1))
            {
              //  SET Guides Position
                GuideRefs[0].transform.position = new Vector3(1, (s_counter * SEPARATION) + transform.position.y + offset + SEPARATION + HEIGHT, 1);
                GuideRefs[1].transform.position = new Vector3(1, GuideRefs[0].transform.position.y + SEPARATION, 1);

                GuideColRef.transform.position = new Vector3(1, GuideRefs[0].transform.position.y - SEPARATION, 1);
                
                //Restart Offset
                offset = 0;
            }


            s_counter++;
        }


                
    }

*/
        public void SpawnObject(float positionY)
    {
        if (s_counter < n.Length)
        {
            
            // 
           
            // Offset Accrued
            offset += (n[s_counter].isSameHeight * SEPARATION) ;// / SPEED_SCALAR IS NOT HOW SEPARATED WILL BE FROM EACH OTHER;
             

            //Assign values to variables from Readers
            prefab_id = n[s_counter].prefab_id;
            position = n[s_counter].position;
            sound_id = n[s_counter].sound_id;


            //Refer to SoundID

			if(prefab_id == 1){

			 md = prefabs[prefab_id].gameObject.GetComponent(typeof(MoveDown)) as MoveDown;


            md.soundID = sound_id;
	
			}		
           	if(prefab_id == 2){

			 md2 = prefabs[prefab_id].gameObject.GetComponent(typeof(MoveDown2)) as MoveDown2;


            md2.soundID = sound_id;
	
			}		




            //Refer to SpriteRenderer
            sprite =  prefabs[prefab_id].gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            if (sprite)
            {
                sprite.sortingOrder =  - s_counter;
            }
            /**/


            Instantiate(prefabs[prefab_id], new Vector3(position, positionY + offset , 0), Quaternion.identity);




            s_counter++;
/*
            if(s_counter % AMOUNT_TO_MOD == 0)
            {
			//	Debug.Log("Move" + s_counter);
            GuideColRef.transform.position = new Vector3(4, GuideColRef.transform.position.y -AMOUNT_TO_MOVE, 1);


            }	
*/

        }
        else{
        	//stop scripts
        	EnableScripts(false);
        }
    }
//########################################
//########################################

/*IEnumerator CallInitializer()
    {
        yield return new WaitForSeconds(4); 
        SpawnObjectInitial();
    }
*/
    void OnTriggerEnter2D(Collider2D col)
    {
        //is not about time but about position(collison)
        //need to spawn in advance FIRST based on postion 

        SpawnObject(GuideRefs[g_counter].transform.position.y);

        GuideRefs[g_counter].transform.position += new Vector3(0, (2 * Local_Separation), 0); 

        //change count to alternate between guide movement
        if (g_counter == 0)
        {
         
            g_counter = 1;
        }
        else
        {
            g_counter = 0;
        }
    }

}