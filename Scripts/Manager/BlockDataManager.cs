using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataManager : MonoBehaviour
{
    private static BlockDataManager Instance;
    private Dictionary<int, BlockSO> blockDictionary;
    [SerializeField] private BlockSO blockRedSO;
    [SerializeField] private BlockSO blockOrangeSO;
    [SerializeField] private BlockSO blockYellowSO;
    [SerializeField] private BlockSO blockGreenSO;
    [SerializeField] private BlockSO blockBlueSO;
    [SerializeField] private BlockSO hardBlockSO;
    [SerializeField] private BlockSO invincibleBlockSO;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        SetBlockDictionary();
    }

    public void SetBlockDictionary() // blockDictionary�� ��� Ÿ�Ժ��� �ش��ϴ� BlockSO ���� �Ҵ�
    {
        blockDictionary = new Dictionary<int, BlockSO>();
        blockDictionary.Add(1, blockRedSO);
        blockDictionary.Add(2, blockOrangeSO);
        blockDictionary.Add(3, blockYellowSO);
        blockDictionary.Add(4, blockGreenSO);
        blockDictionary.Add(5, blockBlueSO);
        blockDictionary.Add(6, hardBlockSO);
        blockDictionary.Add(7, invincibleBlockSO);
    }

    public static BlockDataManager GetInstance()
    {
        return Instance;
    }

    public BlockSO GetData(int id) //�Էµ� ���id�� �ش��ϴ� BlockSO��ü�� ��ȯ
    {
        if(id == 0) return null;
        return blockDictionary[id];
    }
}
