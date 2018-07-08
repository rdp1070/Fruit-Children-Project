using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string player_name;

    [SerializeField]
    private PlayerInventoryController inventory = new PlayerInventoryController();

    [Tooltip("The actual camera.")]
    public Camera player_camera;

    [Tooltip("How far the player can reach when interacting with something.")]
    public float reach;

    // Use this for initialization
    void Start()
    {
        if (player_camera == null)
        {
            player_camera = (Camera)GetComponentInChildren(typeof(Camera), false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("You hit Mouse 0");
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
                    inventory.PickupItem(pickup_item);
                }
            }
        }
    }
}
