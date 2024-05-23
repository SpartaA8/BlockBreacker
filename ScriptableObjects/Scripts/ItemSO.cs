using UnityEngine;

public enum EItemType
{
    LIFE,
    SIZE,
    SPEED,
    COPY
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptObj/Item", order = 4)]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public int index;
    public EItemType itemType;
    public Sprite imageSprite;
}