using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveDown : MonoBehaviour
{
    public float speedScale;
    public sbyte soundID;

    public bool wasCol2dDisabled = false;
    public Collider2D col2d;

public GameObject LocalScoreGO;  
public GameObject LocalComboGO;

public Text LocalScoreTXT;
public Text LocalComboTXT;



public bool isCalculable;
public bool isTouchable;
//-------------------

 ScoreManager SM;
 PoolManager PM;
        
 
//--------

// 0 = Missed , 1 = Bad, 2 = Good, 3 = Perfect
    public sbyte status = 0; // JUST cHANGED FROM 0 -3   

    // Start is called before the first frame update
    void Start()
    {
        speedScale = ReadersSpawnerGuideCollider._instance.SPEED;
        
        col2d = GetComponent<Collider2D>();



/*
try{
 
LocalScoreGO = GameObject.Find("Canvas/txtScore"); 
LocalComboGO = GameObject.Find("Canvas/txtComboVal");

LocalScoreTXT = LocalScoreGO.GetComponent<Text>();
LocalComboTXT = LocalComboGO.GetComponent<Text>();


}
catch  {


} 

*/
SM = (ScoreManager)FindObjectOfType(typeof(ScoreManager));
 
PM = (PoolManager)FindObjectOfType(typeof(PoolManager));

//LocalScoreTXT = ScoreManager._instance.scoreTXT;
//LocalComboTXT = ScoreManager._instance.comboTXT;


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( speedScale * Vector3.down * Time.deltaTime);


//TOUCH
if (isCalculable){

                for (int i = 0; i < Input.touchCount; i++) //JUST READDED
               
//JUST REMOVED                if  (true)
                {
                    // Convert Screen Coordinated to WorldPoint
                   Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);//JUST READDED
                    Vector2 touchPos = new Vector2(wp.x, wp.y);//JUST READDED

                    if (!wasCol2dDisabled)// Prefab is TAP and the collider is not disabled yet
                    {



                        //Checks if Position is withing Collider and Phase is Began
                        /*isTouchable*/ 
                        if (  col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Began)
                        {


                            Destroyer._instance.PlayAudio(soundID);

                             switch (status)
                            {
                              case 1://bad
                            SM.SetActiveCombo(false);
                           SM.AddScore(status);
                            SM.ResetCombo();
                           //--- ScoreManager._instance.SetActiveCombo(false);
                           //--- ScoreManager._instance.AddScore(status);
                           
                           //--- LocalScoreTXT.text =  Mathf.RoundToInt(ScoreManager._instance.currentScore).ToString();
                            //--ScoreManager._instance.ResetCombo();  
                            //  
                            SM.SpanwAtVector(status, transform.position);

                   

                                  break;
                              case 2://Good

                            SM.AddScore(status);
                            SM.AddCombo(1);
                            

//--                            ScoreManager._instance.AddScore(status);
//--                            LocalScoreTXT.text =  Mathf.RoundToInt(ScoreManager._instance.currentScore).ToString();
//--                            ScoreManager._instance.AddCombo(1);
//--                            LocalComboTXT.text =  Mathf.RoundToInt(ScoreManager._instance.currentCombo).ToString();

  
                                if(SM.currentCombo > 1){
                                SM.AnimateCombo();

                                }
                        

//--                            if(ScoreManager._instance.currentCombo > 1){
//--                                ScoreManager._instance.AnimateCombo();
//--
//--                            }


                            
                                    SM.SpanwAtVector(status, transform.position);
                                  break;
                              case 3://Perfect
                            SM.AddScore(status);
                           //-- LocalScoreTXT.text =  Mathf.RoundToInt(ScoreManager._instance.currentScore).ToString();
                            SM.AddCombo(1);
                            //--LocalComboTXT.text =  Mathf.RoundToInt(ScoreManager._instance.currentCombo).ToString();



                            if(SM.currentCombo > 1){
                                SM.AnimateCombo();  
                            }       

                            SM.SpanwAtVector(status, transform.position);                            
                            break;
                            

                              default:
                                  
                                  break;
                            }

                            


                                col2d.enabled = false;
                                wasCol2dDisabled = true;
                            Destroy(gameObject); 


                            ///////////COMMENT START
                            // Bad Tap
                           /*
                            if (transform.position.y >= my_UpperLimitBad || transform.position.y <= my_DownerLimitBad) // Bad
                            {
                                //TEMPOPTIMIZED  PM.ResetCombo();
                                PM.sp.SpawnAudio(soundID);

                                //DO BAD TAP ANIMATION 
                                myAnimator.enabled = false;
                                myAnimator.enabled = true;
                                myAnimator.Play("TapBad");

                                StartCoroutine(DisableGameObject());
                            }
                            // Excellent Tap
                            else if (transform.position.y > my_DownerLimitGood && transform.position.y < my_UpperLimitGood) //Excellent
                            {

                                  SM.AddToCombo();

                                PM.sp.SpawnAudio(soundID);
                                //DO EXCELLENT TAP ANIMATION 
                                myAnimator.enabled = false;
                                myAnimator.enabled = true;
                                myAnimator.Play("TapExcellent");

                                StartCoroutine(DisableGameObject());
                            }
                            else // Good Tap
                            {

                                 SM.AddToCombo();

                                PM.sp.SpawnAudio(soundID);

                                //DO GOOD TAP ANIMATION 
                                myAnimator.enabled = false;
                                myAnimator.enabled = true;
                                myAnimator.Play("TapGood");

                                StartCoroutine(DisableGameObject());
                            }*/


                        
                         
                          
                            }
                    }
                }




}

    }





}
 



















