using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnimManager : MonoBehaviour {

    [Header("ANIMATORS")]
    public Animator panelAnimator;

    [Header("ANIMATION STRINGS")]
    public string fadeInAnim;
    public string fadeOutAnim;

    [Header("SETTINGS")]
    public string shortcutKey;

    public KeyCode veryshortkey;
   
    public GameObject app;

public bool opened;
    
    void Update()
    {
         if (Input.GetKeyDown(shortcutKey))
        {    panelAnimator.gameObject.SetActive(true);
            panelAnimator.Play(fadeInAnim);

        }
   
      
    }
    void Start(){

    

    }

}
