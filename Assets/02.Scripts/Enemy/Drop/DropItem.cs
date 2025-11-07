using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItem:MonoBehaviour
{
    [Header("아이템 프리펩")]
    [SerializeField] private GameObject[] _itemPrefabs;

    [Header("아이템 드랍율 & 가중치 합")]
    [SerializeField] private float _dropRate = 0.5f;
    [SerializeField] private float _weightSum = 100f;

    [Header("아이템별 드랍 가중치")]
    [SerializeField] private DropWeightByItem[] _dropWeightByItem;
    


    private EItemType _targetItem = EItemType.IT_Health;

   
    public void Drop()
    {
        float randomNum = Random.Range(0f, 1f);

        if (randomNum <= _dropRate)
            return;

        DetermineItem();
        Instantiate(_itemPrefabs[(int)_targetItem], transform.position, Quaternion.identity);
    }


    private void DetermineItem()
    {
        
        float randomValue = Random.Range(0f, _weightSum);
        float weightSum = 0f;
        _targetItem = EItemType.IT_Health;

        foreach (var item in _dropWeightByItem)
        {
            weightSum += item.DropWeight;

            if (randomValue <= weightSum)
            {
                _targetItem = item.ItemType;
                break;
            }
           
        }
    }
}


[Serializable]
struct DropWeightByItem
{
    public EItemType ItemType;
    public int DropWeight;
}