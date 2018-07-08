using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPileController : MonoBehaviour {

    public List<ItemController> items = new List<ItemController>();
    public int maxItems;
    public int numItems;

	// Use this for initialization
	void Start () {
        items.Capacity = maxItems;
	}
	
	// Update is called once per frame
	void Update () {
        numItems = items.Count;
	}

    void RenderItems() {
        // for each item in the List, shrink the item and put it in one unit of stockpile
        
    }
}
