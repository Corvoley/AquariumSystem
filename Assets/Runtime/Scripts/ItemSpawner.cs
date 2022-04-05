using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawner : MonoBehaviour
{

    private Transform itemParent;
    private GameObject itemToSpawn;
    private Camera cam;
    private SpriteRenderer itemIconSprite;
    [SerializeField] private SpriteRenderer allowedAreaToSpawnItem;
    private Vector3 iconOffSet;


    public void Awake()
    {
        itemIconSprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        ItemIconView();
        if (Input.GetMouseButtonDown(0) && allowedAreaToSpawnItem.bounds.Contains(returnMousePositionOnWorldPosition()) && itemToSpawn != null && !IsPointerOverUIObject())
        {
            Instantiate(itemToSpawn, returnMousePositionOnWorldPosition(), Quaternion.identity, itemParent);

        }
    }

    private Vector3 returnMousePositionOnWorldPosition()
    {
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(cam.transform.position.z);
        //worldPoint.z = 11f;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    private void ItemIconView()
    {
        if (itemIconSprite != null)
        {
            itemIconSprite.transform.position = returnMousePositionOnWorldPosition() + iconOffSet;
            Color tmp = itemIconSprite.color;
            tmp.a = 0.5f;
            itemIconSprite.color = tmp;
        }
    }
    public void SetItemToSpawn(GameObject itemPrefab)
    {
        ShopMenu.CloseCurrent();
        itemToSpawn = itemPrefab;
        itemIconSprite.sprite = itemPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        iconOffSet = itemPrefab.GetComponentInChildren<SpriteRenderer>().transform.position;

    }
    public void SetParentOfSpawn(Transform parentTransform)
    {
        itemParent = parentTransform;

    }
}
