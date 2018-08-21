using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash_Screen : MonoBehaviour {

    [SerializeField]
    private float time = 2;
    public RawImage splashImage;
    public int loadLevel;

	// Use this for initialization
	IEnumerator Start()
    {
        //StartCoroutine(ToMainMenu());

        splashImage.canvasRenderer.SetAlpha(0.0f);

        FadIn();
        yield return new WaitForSeconds(2.5f);
        FadOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
    */
    void FadIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
