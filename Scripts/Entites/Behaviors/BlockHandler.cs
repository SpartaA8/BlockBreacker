using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BlockHandler : MonoBehaviour
{
    [SerializeField]protected BlockSO blockSO;
    private SpriteRenderer spriteRenderer;
    protected int currentHp;

    protected virtual void Awake()
    {
        MainSceneManager.Instance.OnFinishStageEvent += DestroyBlock;
    }

    public void BlockSpriteChange()
    {
        Sprite[] slicedSprites = Resources.LoadAll<Sprite>("Breakout-001-C");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = slicedSprites[blockSO.blockSpriteColor];
    }

    public void SetBlockSO(BlockSO newBlockSO)
    {
        blockSO = newBlockSO;
        currentHp = blockSO.hp;
        BlockSpriteChange();
    }    

    public virtual void TakeDamage(int damage)
    {

        if (!blockSO.isInvincible)
        {
            currentHp -= damage;
            if (currentHp == 0)
            {
                Destroy(gameObject);                
                ItemDataManager.Instance.SpawnItem(transform.position, blockSO.hp);
                MainSceneManager.Instance.DestroyBlock(blockSO.score);
            }
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        MainSceneManager.Instance.OnFinishStageEvent -= DestroyBlock;
    }
}
