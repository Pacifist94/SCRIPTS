using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{

    public int Level = 1 ;



    private static LevelLoader _instance;




 public static LevelLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelLoader>();
                DontDestroyOnLoad(_instance); 
            }

            return _instance;
        }

    }

//void Start(){Debug.Log("Start LevelLoader");}

    void Awake()
    {

        DontDestroyOnLoad(transform.gameObject);
    }


	public void LoadLevel(int sceneIndex) {

        StartCoroutine(LoadAsynchronously(sceneIndex));
    }


    IEnumerator LoadAsynchronously(int scIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scIndex);

         
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(progress); 
            yield return null;
        }
    }


}
