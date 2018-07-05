using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestationController : MonoBehaviour {

    public Time Time = new Time();
    public float TimeUntilBirth = 9999999;
    private float TotalBirthTime;

    public float TimeUntilDeath;
    private float TotalDeathTime;

    public float scale = 1;
    public GameObject BabyPrefab;
    public bool hasBirthed;

    private Renderer renderer;

	// Use this for initialization
	void Awake () {
        TotalBirthTime = TimeUntilBirth;
        TotalDeathTime = TotalBirthTime;
        TimeUntilDeath = TimeUntilBirth / 3;

        renderer = GetComponent<Renderer>();

        hasBirthed = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (TimeUntilBirth > 0)
        {
            // if there is still time to be born, grow incrementally.
            TimeUntilBirth -= Time.unscaledDeltaTime;
            var x = (TotalBirthTime - TimeUntilBirth) / TotalBirthTime;
            scale = Mathf.Max(.1f, x);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (TimeUntilBirth <= 0 && hasBirthed == false)
        {
            // if it's time, be born.
            Born();
        }
        else if (TimeUntilBirth <= 0 && hasBirthed == true) {
            TimeUntilDeath -= Time.unscaledDeltaTime;
            renderer.material.color = Color.gray;

            if (TimeUntilDeath <= 0) {
                Die();
            }
        }
	}

    private void Born()
    {
        scale = 1;
        Debug.Log("Birth!");
        Instantiate(BabyPrefab);
        hasBirthed = true;
    }

    private void Die() {
        Debug.Log("Death!");
        scale = 0;
        Destroy(gameObject);
    }
}
