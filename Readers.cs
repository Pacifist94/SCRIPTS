using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class Readers: MonoBehaviour
{
    //Singleton
    #region SINGLETON
        public static Readers _instance;

        public static Readers Instance
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
    public string LevelNumber = "1";

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
    public int counter = 0;

    public int[] levelSize = new int[1]; // Defaults 140, 294

   // public float[] levelSPEED = new float[1]; // Defaults
    //public float[] levelSEPARATION = new float[1]; // Defaults 
    //public sbyte[] levelINITIAL_AMOUNT = new sbyte[1]; // Defaults 

    void Start()
    {

        LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));
        
        LevelNumber = LL.Level.ToString();


        


        int LevelNumberLocal = int.Parse(LevelNumber); // 1 , 2
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
    }

 


    public void FillStruct()
    {
        int counter = 0;

 

        //                  ARRAY SIZE note[] n = 20
        for (int i = 0; i < n.Length; i++)
        {
            n[i].prefab_id = sbyte.Parse( m_coord[counter]);
            n[i].position = sbyte.Parse(m_coord[counter + 1]);
            n[i].isSameHeight = int.Parse(m_coord[counter + 2]);
            n[i].sound_id = sbyte.Parse( m_coord[counter + 3]);
             
            counter += 4;
       
            //Debug.Log(n[i].prefab_id + "|" + n[i].position + "|" + n[i].isSameHeight + "|" + n[i].sound_id);

        }


    }

    void parseXmlFile(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//levels/level" + LevelNumber;
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        
        foreach (XmlNode node in myNodeList)
        {
            // Initialize rest
            // First Call Iteration 0-99
            for (int i = 0; i <= Limit; i++)
            {



                if (i == 0 && counter == 0)// Only happens the very first time
                {
                    //XML NODE[0] = First
                    m_nodes[0] = node.FirstChild;

                    //Array[0] = XML NODE[0]  /// used as 0, on next call value will be 100
                    m_coord[0] = m_nodes[counter].InnerXml;

                }
                else
                {
                    m_nodes[i] = m_nodes[counter - 1].NextSibling;

                    m_coord[i] = m_nodes[counter].InnerXml;
                }
                counter++;// increase counter
            }
        }
    }

    

}