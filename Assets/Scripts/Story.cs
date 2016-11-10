using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Story : MonoBehaviour {

    public Canvas storyCanvas;
    public List<RawImage> storyImages;
    public float timeFullPic, timeToWaitSwitch;
    private float timeSwitchPic, alphaFadeIn;
    public int currentImage;
    
	// Use this for initialization
	void Start () {

        currentImage = 0;
        alphaFadeIn = 0;
        foreach (RawImage myImg in storyImages)
        {
            myImg.color = new Color(255, 255, 255, 0f);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(currentImage < storyImages.Count)
        {
            timeSwitchPic = Time.time;
            alphaFadeIn += Time.deltaTime * timeFullPic;
            storyImages[currentImage].color = new Color(255, 255, 255, alphaFadeIn);

            if (timeSwitchPic > timeToWaitSwitch)
            {
                timeToWaitSwitch = timeToWaitSwitch + timeSwitchPic;
                if (storyImages[currentImage].color.a >= 1)
                {
                    currentImage++;
                    alphaFadeIn = 0;
                }
            }
        }

        /*
        foreach (RawImage myImg in storyImages)
        {
            if (myImg.color.a >= 1)
                storyImages.Remove(myImg);
        }
        */
    }
}
