using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance;
    [SerializeField] private GameObject itemPrefab; // 아이템 생성 Prefab
    private Dictionary<int, ItemSO> itemDictionary;

    [SerializeField] private ItemSO ItemLIFESO;
    [SerializeField] private ItemSO ItemSIZESO;
    [SerializeField] private ItemSO ItemSPEEDSO;
    [SerializeField] private ItemSO ItemCOPYSO;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;            
            SetItemDictionary();
        }
    }

    public void SetItemDictionary()
    {
        itemDictionary = new Dictionary<int, ItemSO>
        {
            {1, ItemLIFESO},
            {2, ItemSIZESO},
            {3, ItemSPEEDSO},
            {4, ItemCOPYSO}
        };
    }

    public ItemSO GetData(int id)
    {
        if (id == 0 || !itemDictionary.ContainsKey(id))
        {
            return null;
        }
        return itemDictionary[id];
    }

    public void SpawnItem(Vector3 position, int blockHp)
    {
        int randomSpawnIndex = UnityEngine.Random.Range(0, 100); // 0부터 99 사이의 랜덤한 값을 선택
        if (blockHp * 20 >= randomSpawnIndex)
        {            
            int randomItemIndex = UnityEngine.Random.Range(1, 5); // 1부터 4 사이의 랜덤한 값을 선택
            ItemSO itemData = GetData(randomItemIndex);            
            if (itemData != null && itemPrefab != null)
            {
                GameObject itemObject = Instantiate(itemPrefab, position, Quaternion.identity);
                ItemController itemController = itemObject.GetComponent<ItemController>();
                if (itemController != null)
                {
                    itemController.SoItem = itemData;
                }
                SpriteRenderer itemSpriteRenderer = itemObject.GetComponent<SpriteRenderer>();
                if (itemSpriteRenderer != null)
                {
                    itemSpriteRenderer.sprite = itemData.imageSprite;
                }
            }
        }
    }
}
    
    