using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradePicker : MonoBehaviour
{
    [SerializeField] LevelUpgradeContainer levelUpgrades;
    public List<int> GetThreeRandomUpgradeIndexes()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int counter = 0;
            int randIndex = Random.Range(0, levelUpgrades.levelUpgrades.Count);
            while (counter < 100 && indexes.Contains(randIndex)) 
            {
                randIndex = Random.Range(0, levelUpgrades.levelUpgrades.Count);
            }
            indexes.Add(randIndex);
        }

        return indexes;
    }

    public LevelUpgrade[] GetThreeRandomLevelUpgrades()
    {
        List<int> indexes = GetThreeRandomUpgradeIndexes();
        LevelUpgrade[] output = new LevelUpgrade[3];

        for (int i = 0; i < 3; i++)
        {
            output[i] = levelUpgrades.levelUpgrades[indexes[i]];
        }

        return output;
    }
}
