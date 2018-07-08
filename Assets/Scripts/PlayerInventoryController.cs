using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{

    public GameObject inventory_trough;
    public GameObject inventory_item_panel;
    private List<GameObject> inventoryHotbarPanels;

    [Tooltip("The list of inventory items.")]
    public List<ItemController> inventory = new List<ItemController>();

    [SerializeField]
    [Tooltip("The item currently in hand.")]
    private ItemController currentItem;

    [Tooltip("How much the player can carry at once.")]
    public int carry_capacity = 5;

    public Canvas canvas;

    // Use this for initialization
    void Awake()
    {
        inventory.Capacity = carry_capacity;
        SetUpUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isFocused == true)
        {
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                Debug.Log("Moused Scroll Delta: " + Input.mouseScrollDelta);
                // this will select the next item in the list or roll over
                SelectNextItem(Input.mouseScrollDelta.y);
            }
        }
    }

    private void SelectNextItem(float delta)
    {
        if (inventory.Count > 0)
        {
            var index = inventory.IndexOf(currentItem);
            if (delta > 0)
            {
                index = (index + 1) % inventory.Count;
                SelectItem(inventory[index], currentItem);
            }
            else
            {
                index = (index - 1 < 0)? inventory.Count-1 : index - 1;
                SelectItem(inventory[index], currentItem);
            }
            UpdateUI();
        }
    }

    private void SetUpUI()
    {
        inventoryHotbarPanels = new List<GameObject>();
        for (var x = 0; x < carry_capacity; x++)
        {
            var panel = Instantiate(inventory_item_panel);
            panel.transform.parent = inventory_trough.transform;
            inventoryHotbarPanels.Add(panel);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (var x = 0; x < inventoryHotbarPanels.Count; x++)
        {
            HotBarItemController hotBarItem = inventoryHotbarPanels[x].GetComponent(typeof(HotBarItemController)) as HotBarItemController;

            if (!(x >= inventory.Count))
            {

                ItemController item = inventory[x];

                if (hotBarItem.outline != null)
                {
                    if (item.isHold == true)
                    {
                        hotBarItem.outline.effectColor = Color.red;
                        hotBarItem.outline.effectDistance = new Vector2(2, 2);
                    }
                    else
                    {
                        hotBarItem.outline.effectColor = Color.black;
                        hotBarItem.outline.effectDistance = new Vector2(1, 1);
                    }
                }

                if (hotBarItem.quantity_text != null)
                {
                    // set the quantity text to the quantity of the item in question.
                    hotBarItem.quantity_text.text = item.quantity.ToString();
                    // if it is at max quantity change the color of the text.
                    if (item.quantity == item.max_quantity)
                    {
                        hotBarItem.quantity_text.CrossFadeColor(Color.red, .5f, false, false);
                    }
                    else
                    {
                        hotBarItem.quantity_text.color = Color.black;
                    }
                }

                // if the item has an inventory appearance set the UI image to that.
                if (item.inventory_appearance != null)
                {
                    if (hotBarItem.item_sprite != null)
                        hotBarItem.item_sprite.sprite = item.inventory_appearance;
                }
            }
            else
            {
                hotBarItem.quantity_text.text = "";
                hotBarItem.item_sprite.sprite = null;
                hotBarItem.outline.effectColor = Color.black;
                hotBarItem.outline.effectDistance = new Vector2(1, 1);
            }


        }
        //var inventory_image_UI = inventory_trough.GetComponentsInChildren(typeof());
    }

    private void SelectItem(ItemController item)
    {
        item.isHold = true;
        currentItem = item;
    }

    private void SelectItem(ItemController new_item, ItemController previous_item)
    {
        new_item.isHold = true;
        previous_item.isHold = false;
        currentItem = new_item;
    }

    /// <summary>
    /// Look in the inventory and decide to pick it up or not.
    /// </summary>
    /// <param name="pickup_item"> The item which you aim to pick up.</param>
    public void PickupItem(ItemController pickup_item)
    {
        pickup_item.name = pickup_item.item_name;

        if (inventory.Count == 0)
            SelectItem(pickup_item);
        // compare to each item in the inventory.
        foreach (ItemController item in inventory)
        {
            // if the item is the same ID as the item you are picking up
            if (item.item_id == pickup_item.item_id && item.quantity < item.max_quantity)
            {
                item.quantity++;
                // remove item from world.
                Destroy(pickup_item.gameObject);
                UpdateUI();
                return;
            }
        } // end existing item check

        // if you make it to this point you can check for a new item now.
        if (inventory.Count < carry_capacity) // if you can carry more new items.
        {
            inventory.Add(pickup_item);
            Destroy(pickup_item.gameObject);
            UpdateUI();
            return;
        }
    }

}

[CustomEditor(typeof(PlayerInventoryController))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        PlayerInventoryController myTarget = (PlayerInventoryController)target;

        if (myTarget.inventory_item_panel == null)
        {
            EditorGUILayout.HelpBox("You need an Inventory Item Image", MessageType.Error);
        }

        if (myTarget.inventory_trough == null)
        {
            EditorGUILayout.HelpBox("You Need an Inventory Trough", MessageType.Error);
        }

        if (myTarget.canvas == null)
        {
            EditorGUILayout.HelpBox("You Need a Canvas", MessageType.Error);
        }

        if (myTarget.inventory != null)
        {
            Rect r = EditorGUILayout.BeginVertical();
            EditorGUI.ProgressBar(r, (myTarget.inventory.Count / myTarget.carry_capacity), "Used Inventory");
            GUILayout.Space(18);
            EditorGUILayout.EndVertical();
        }
    }
}
