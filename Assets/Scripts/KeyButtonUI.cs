using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class KeyButtonUI : MonoBehaviour
{
    public Image E_Slider;
    public Text E_Text;
    public Button.ButtonClickedEvent buttonevent;
    public KeyCode Key;
    public float PushTime=1;
    bool basildi;


   public void Intialize(KeyCode key, Button.ButtonClickedEvent eventa, float pushTime,string texta)
    {
        Key = key;
         buttonevent= eventa;
        PushTime = pushTime;
        E_Text.text = texta;

    }
    bool ilkbasis;
    private void Update()
    { if (Input.GetKeyDown(Key))
            ilkbasis = true;
        if (Input.GetKeyUp(Key))
            ilkbasis = false;
        if (Input.GetKey(Key)&& buttonevent!=null&&ilkbasis)
        {
            if (!basildi)
            {
                basildi = true;
                StartCoroutine(ReloadSlider(PushTime));
            }
         


        }
        else
        {
            basildi = false;
            StopAllCoroutines();
            E_Slider.fillAmount = 0;

        }
        
    }
    public IEnumerator ReloadSlider(float time)
    {
        int a = 0;  // start
        int b = 1;  // end
        float x = time;  // time frame
        float n = 0;  // lerped value
        for (float f = 0; f <= x; f += Time.deltaTime)
        {
            n = Mathf.Lerp(a, b, f / x); // passing in the start + end values, and using our elapsed time 'f' as a portion of the total time 'x'

            E_Slider.fillAmount = n;

            // use 'n' .. ?
            yield return null;
        }
        ilkbasis = false;
        basildi = false;
        E_Slider.fillAmount = 0;
        buttonevent.Invoke();
    }
}
