using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaPoinUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void setupManaText(int mana)
    {
        textMesh.SetText(mana.ToString());
    }

}
