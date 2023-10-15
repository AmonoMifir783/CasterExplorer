using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CreateSpellItem : MonoBehaviour
{
    public void CreateSpell()
    {

        //Debug.Log("Сообщение CreateSpell");
        SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();
        string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/Fireball.asset";
        #if UNITY_EDITOR
            AssetDatabase.CreateAsset(spellItem, targetPath);
            AssetDatabase.SaveAssets();
        #endif

    }
}
