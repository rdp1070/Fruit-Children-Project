using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour {

    public Light sun;
    public Light moon;
    

	// Use this for initialization
	void Start () {
        sun.type = LightType.Directional;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Sun rotates around the planet, at some function of time. 
        // Time should be implemented, and you should also have a moon.
        // Time of day should help determine other functions of characters too, like sleeping, or lights turning on.
        if (sun != null) {
            sun.transform.Rotate(Vector3.right, .1f);
        }
        if (moon != null) {
            moon.transform.Rotate(Vector3.right, .1f);
        }
    }
}
