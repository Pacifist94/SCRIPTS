using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{ 


	//Singleton
    #region SINGLETON

    public static ScoreManager _instance;

    public static ScoreManager Instance
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

//    public Text scoreTXT;
  //  public Text comboTXT;

    public Animator comboBanAnim;
    public Animator comboValAnim;

    public GameObject comboBanGO;
    public GameObject TMComboLabelGO;
    public GameObject TMComboGO;


public TextMeshProUGUI TMTXT; 
public TextMeshProUGUI TMCombo; 


	//Score Variables 
	public float currentScore;
	public int currentCombo;


	//---------SCORE
	public  float MAIN_SCORE;
	public float[] TAPABLE_PREFABS = new float[1];

	public float CALC_PERFECT_SCORE;
	public float CALC_GOOD_SCORE;
	public float CALC_BAD_SCORE;



 PoolManager PM;


    public GameObject[] StatusAnimationGO = new GameObject[2];



public void SpanwAtVector(int id, Vector3 position_new){

            
PM.ReuseObject(StatusAnimationGO[id], position_new, Quaternion.identity);

            //Instantiate(StatusAnimationGO[id], position_new, Quaternion.identity);


} 



void Start(){

    PM = (PoolManager)FindObjectOfType(typeof(PoolManager));


 
PM.CreatePool(StatusAnimationGO[0], 5);
PM.CreatePool(StatusAnimationGO[1], 5);
PM.CreatePool(StatusAnimationGO[2], 5); 
PM.CreatePool(StatusAnimationGO[3], 5);




LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));


CALC_PERFECT_SCORE = 1000000f / TAPABLE_PREFABS[LL.Level - 1 ] ;
CALC_GOOD_SCORE = CALC_PERFECT_SCORE * 0.80f;
CALC_BAD_SCORE = CALC_PERFECT_SCORE * 0.30f;
/*
Debug.Log("LL.Level " + LL.Level); 
Debug.Log("CALC_PERFECT_SCORE " + CALC_PERFECT_SCORE);
Debug.Log("CALC_GOOD_SCORE " + CALC_GOOD_SCORE);
Debug.Log("CALC_BAD_SCORE " + CALC_BAD_SCORE);
Debug.Log("TAPABLE_PREFABS[] " + TAPABLE_PREFABS[LL.Level - 1 ]);
Debug.Log("VAL " + Mathf.RoundToInt( (float.Parse(TAPABLE_PREFABS[LL.Level - 1 ].ToString()) * CALC_PERFECT_SCORE)));
*/

/*
scoreTXT.text = Mathf.RoundToInt(0).ToString();
comboTXT.text = currentCombo.ToString();  
*/

//TMTXT = TMGO.GetComponent<TextMeshProUGUI>();
//TMCombo = TMGO.GetComponent<TextMeshProUGUI>();


}


    
public void AddScore(sbyte status){

 switch (status)
        {
           
            case 1:
           		currentScore += CALC_BAD_SCORE;
           	break;
             
            case 2:
                currentScore += CALC_GOOD_SCORE;
            break;
            
            case 3:
            	currentScore += CALC_PERFECT_SCORE;
            break;

            default:

            break;
        }



 UpdateScore();

}

public void AddCombo(int amount){


currentCombo++;

 UpdateCombo();



}

public void ResetCombo(){
currentCombo = 0;
UpdateCombo();
}


public void UpdateScore(){
  

	//scoreTXT.text = Mathf.RoundToInt(currentScore).ToString();

TMTXT.text = Mathf.RoundToInt(currentScore).ToString();	
}

public void UpdateCombo(){

	//comboTXT.text = currentCombo.ToString();

TMCombo.text = currentCombo.ToString();  
  
}

public void AnimateCombo(){
   SetActiveCombo(true);
   comboBanAnim.Play("comboBan", -1, 0);
 //JRN   comboValAnim.Play("comboVal", -1, 0);
}

public void SetActiveCombo(bool IsActive){
    comboBanGO.SetActive(IsActive);
    TMComboGO.SetActive(IsActive);
    TMComboLabelGO.SetActive(IsActive);
   
}


}























