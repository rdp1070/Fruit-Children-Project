using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGeneticsController : MonoBehaviour {

    public GameObject statPanel;

    public List<Stat> stats;

    public Stat FStat;
    public Stat RStat;
    public Stat UStat;
    public Stat IStat;
    public Stat TStat;

	// Use this for initialization
	void Start () {

        stats = new List<Stat>(){ FStat, RStat, UStat, IStat, TStat };

        if (statPanel == null) {
            GameObject.FindGameObjectsWithTag("Stat Panel");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Stat {


    public Stat(string Name) {
        name = Name;
        growthRate = 1;
        value = 0;
        baseValue = 0;
        potentialValue = 100;
    }

    /// <summary>
    /// The name of the stat that appears in the label
    /// </summary>
    public string name;
    /// <summary>
    /// Everytime you do an activity it adds a multiplier 
    /// times your growthRate to the value of the stat. 
    /// This number indicates how fast it grows.
    /// </summary>
    public float growthRate;
    /// <summary>
    /// Current value of the stat.
    /// </summary>
    public float value;
    /// <summary>
    /// The stat level the baby is born with.
    /// </summary>
    public float baseValue;
    /// <summary>
    /// The stat maximum for this stat.
    /// </summary>
    public float potentialValue;

    /// <summary>
    /// Just a hand subdivider for the value;
    /// </summary>
    public int level;
}
