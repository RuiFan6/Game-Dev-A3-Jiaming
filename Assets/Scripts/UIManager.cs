using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1Scene", LoadSceneMode.Single);
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        DontDestroyOnLoad(this.gameObject);
    }
}
