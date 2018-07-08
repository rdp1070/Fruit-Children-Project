using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    public string item_id = "000000";
    public string item_name = "Item";
    public int quantity = 0;
    public int max_quantity = 99;
    public bool isHold = false;
    public Sprite inventory_appearance;

    public ItemController() {
        item_name = " ";
        item_id = "000000";
        quantity = 0;
        isHold = false;
    }

    /// <summary>
    /// This triggers on action button for whatever you are holding.
    /// </summary>
    void Action() {
        
    }
}
