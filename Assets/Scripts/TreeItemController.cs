using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeItemController : ItemController
{

    public void Start()
    {
        name = "Sapling";
        isHold = false;
        quantity = 1;
        max_quantity = 99;
    }

    public void Action()
    {
        if (isHold && quantity > 0) {
            isHold = false;
            quantity--;
            // find where the character is looking, and place a prefab there. 
        }
    }


}
