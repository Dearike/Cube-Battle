using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitProperty
{
    public PropertyType CharacteristicType;

    public int value;
    
}

[System.Serializable]
public class Characteristics : MonoBehaviour
{
    [SerializeField] protected List<UnitProperty> unitProperties;

    protected Dictionary<PropertyType, UnitProperty> unitPropertiesDict;

    protected void Awake()
    {
        this.InitPropertiesDict();
    }

    private void InitPropertiesDict()
    {
        unitPropertiesDict = new Dictionary<PropertyType, UnitProperty>();

        foreach (var property in unitProperties)
        {
            unitPropertiesDict.Add(property.CharacteristicType, property);
        }
    }

    public UnitProperty GetPropertyByType(PropertyType type)
    {
        if (unitPropertiesDict.ContainsKey(type))
        {
            return unitPropertiesDict[type];
        }

        Debug.LogError($"{gameObject.name} no property by type: {type.ToString()}");
        return null;
    }
}
