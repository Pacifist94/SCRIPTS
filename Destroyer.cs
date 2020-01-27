using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public AudioClip[] clips = new AudioClip[10];  //changeInspector;
    public AudioSource audi;

 

        //Singleton
    #region SINGLETON
        public static Destroyer _instance;

        public static Destroyer Instance
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


    void OnTriggerEnter2D(Collider2D col)
    {
	//audi.PlayOneShot(clips[col.gameObject.GetComponent<MoveDown>().soundID]);
	PlayAudio(col.gameObject.GetComponent<MoveDown2>().soundID);


        DestroyMe(col.gameObject);
    }

	public void PlayAudio(sbyte soundID){
        audi.PlayOneShot(clips[soundID]);
	}

	public void DestroyMe(GameObject GO){
     Destroy(GO);
	}


}














