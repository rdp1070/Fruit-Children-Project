using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string player_name;

    [SerializeField]
    [Tooltip("The list of inventory items.")]
    private List<ItemController> inventory = new List<ItemController>();

    [SerializeField]
    [Tooltip("The item currently in hand.")]
    private ItemController currentItem;

    [Tooltip("How much the player can carry at once.")]
    public int carry_capacity = 5;

    [Tooltip("The actual camera.")]
    public Camera player_camera;

    [Tooltip("How far the player can reach when interacting with something.")]
    public float reach;

    // Use this for initialization
    void Start()
    {
        inventory.Capacity = carry_capacity;
        if (player_camera == null)
        {
            player_camera = (Camera)GetComponentInChildren(typeof(Camera), false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            Debug.Log("Moused Scroll Delta: " + Input.mouseScrollDelta);
        }

        if (Input.GetMouseButtonUp(1))
        {

            Debug.Log("You hit mouse2");
            RaycastHit hit = new RaycastHit();
            //Vector2 y = player_camera.transform.forward * reach;
            Vector3 screen_middle = new Vector3(player_camera.pixelWidth / 2, player_camera.pixelHeight / 2);
            Ray screen_ray = player_camera.ScreenPointToRay(screen_middle);
            Ray viewport_ray = player_camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            Debug.DrawRay(screen_ray.origin, screen_ray.direction, Color.red, .5f);
            Debug.DrawRay(viewport_ray.origin, viewport_ray.direction, Color.yellow, .5f);

            if (Physics.Raycast(screen_ray, out hit, reach) == true)
            {
                Debug.Log("You hit something");
                if (hit.transform.gameObject.tag == "Item")
                { // if the thing you hit is an item
                    Debug.Log("The thing you hit is an item!");
                    var pickup_item = (ItemController)hit.transform.GetComponent(typeof(ItemController)) ?? new ItemController();
                    PickupItem(pickup_item);
                }
            }
        }
    }

    /// <summary>
    /// Look in the inventory and decide to pick it up or not.
    /// </summary>
    /// <param name="pickup_item"> The item which you aim to pick up.</param>
    private void PickupItem(ItemController pickup_item)
    {
        pickup_item.name = pickup_item.item_name;
        // compare to each item in the inventory.
        foreach (ItemController item in inventory)
        {
            // if the item is the same ID as the item you are picking up
            if (item.item_id == pickup_item.item_id && item.quantity < item.max_quantity)
            {
                item.quantity++;
                // remove item from world.
                Destroy(pickup_item.gameObject);
                return;
            }
        } // end existing item check
        // if you make it to this point you can check for a new item now.
        if (inventory.Count < carry_capacity) // if you can carry more new items.
        {
            inventory.Add(pickup_item);
            Destroy(pickup_item.gameObject);
            return;
        }
    }
}
