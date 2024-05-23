using UnityEngine;

[CreateAssetMenu(fileName = "BlockSO", menuName = "ScriptObj/Block", order = 2)]
public class BlockSO : ScriptableObject
{
    [Header("Block Info")]
    public int hp;
    public int blockId;
    public int score;
    public int blockSpriteColor;
    public bool isInvincible;
}
