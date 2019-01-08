using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour {

    public List<Stat> stats;
    public FruitGeneticsController statInfo;
    public string babyName;
    public CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
        PanelInit();
	}
	
	// Update is called once per frame
	void Update () {
        // if you are looking at the baby, fade in the stats screen;
        
	}

    public void ShowSelf() {
        // do an animation where you fade in
        var imgs = canvasGroup.GetComponentsInChildren<Image>();
           foreach (Image img in imgs)
            img.CrossFadeAlpha(1, 2f, false);
    }

    public void HideSelf() {
        // do an animation where you fade out
        print("Hide Self");
        var imgs = canvasGroup.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
            img.CrossFadeAlpha(0, 2f, false);
        
    }

    public void PanelInit() {
        // initialize all the values for the panel
        HideSelf();

        stats = new List<Stat>();
        babyName = statInfo.name ?? "Test";

        foreach (Stat stat in stats) {
            // make a new UI element representing the stat
            // and fill the info in with the stat info
        }
    }
}
