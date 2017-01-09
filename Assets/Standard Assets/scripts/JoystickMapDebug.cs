using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class JoystickMapDebug : MonoBehaviour {

    public bool isAxis;
    public string buttonName;
    Text txt;

    // Use this for initialization
    void Start () {
        txt = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(isAxis)
        {
            txt.text = buttonName + " : " + CrossPlatformInputManager.GetAxis(buttonName).ToString();
        }
        else
        {
            txt.text = buttonName + " : " + CrossPlatformInputManager.GetButtonDown(buttonName).ToString();
        }
        
    }

}
