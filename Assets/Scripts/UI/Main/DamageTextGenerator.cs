using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using TMPro;

public class DamageTextGenerator : MonoBehaviour
{
    [SerializeField] GameObjectFloatSOEvent enemyDamageEvent;
    [SerializeField] GameObject textPrefab;

    private void Awake() {
        enemyDamageEvent.AddListener(GenerateDamageText);
    }

    void GenerateDamageText(GameObject damagedObject, float damageValue)
    {
        if (!damagedObject.activeInHierarchy) return;
        string damageTextValue;
        if (damageValue < 1) damageTextValue = damageValue.ToString("G2");
        else if (damageValue < 10) damageTextValue = damageValue.ToString("G2");
        else damageTextValue = ((int)damageValue).ToString();
        GameObject newText = Instantiate(textPrefab, damagedObject.transform.position, Quaternion.identity, transform);

        TextMesh textMesh = newText.transform.GetChild(0).GetComponent<TextMesh>();
        textMesh.text = damageTextValue;
        textMesh.fontSize = Mathf.Clamp(32 + (int)damageValue / 2, 32, 80);

        newText.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4f, 4f), Random.Range(5f, 14f));
    }

    
}
