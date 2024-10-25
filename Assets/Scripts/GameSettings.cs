using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    public int BoardSizeX = 5;

    public int BoardSizeY = 5;

    public int MatchesMin = 3;

    public int LevelMoves = 16;

    public float LevelTime = 30f;

    public float TimeForHint = 5f;

    public eThemeType ThemeType;

    [SerializeField]
    private List<ThemeConfig> m_themeConfigList;

    private Dictionary<NormalItem.eNormalType, NormalItemConfig> m_itemConfigMap;

    private Dictionary<BonusItem.eBonusType, BonusItemConfig> m_bonusItemConfigMap;

    public void Init()
    {
        var themeConfig = m_themeConfigList.Find(c => c.Type == ThemeType);

        m_itemConfigMap = new Dictionary<NormalItem.eNormalType, NormalItemConfig>();
        foreach (var itemConfig in themeConfig.ItemConfigList)
        {
            m_itemConfigMap.Add(itemConfig.Type, itemConfig);
        }

        m_bonusItemConfigMap = new Dictionary<BonusItem.eBonusType, BonusItemConfig>();
        foreach (var itemConfig in themeConfig.BonusItemConfigList)
        {
            m_bonusItemConfigMap.Add(itemConfig.Type, itemConfig);
        }
    }

    public NormalItemConfig GetItemConfig(NormalItem.eNormalType type)
    {
        if (m_itemConfigMap.TryGetValue(type, out var itemConfig))
        {
            return itemConfig;
        }

        return null;
    }

    public BonusItemConfig GetBonusItemConfig(BonusItem.eBonusType type)
    {
        if (m_bonusItemConfigMap.TryGetValue(type, out var itemConfig))
        {
            return itemConfig;
        }

        return null;
    }
}

[System.Serializable]
public class ThemeConfig
{
    public eThemeType Type;
    public List<NormalItemConfig> ItemConfigList;
    public List<BonusItemConfig> BonusItemConfigList;
}

public abstract class BaseItemConfig
{
    public Sprite Sprite;
}

[System.Serializable]
public class NormalItemConfig : BaseItemConfig
{
    public NormalItem.eNormalType Type;
}

[System.Serializable]
public class BonusItemConfig : BaseItemConfig
{
    public BonusItem.eBonusType Type;
}

public enum eThemeType
{
    CHARACTER,
    FISH,
}
