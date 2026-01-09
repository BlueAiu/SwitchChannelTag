using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//自分のプレイヤーの色(マテリアル)を変える

public class ChangeMyMaterial : MonoBehaviour
{
    [System.Serializable]
    public class SkinGroup
    {
        [Tooltip("色を変更したいレンダラー群（体、顔、服など）")]
        [SerializeField] private Renderer[] _renderers;

        //登録された全てのレンダラーのマテリアルを一括変更
        public void ApplyMaterial(Material material)
        {
            if (material == null) return;

            foreach (var r in _renderers)
            {
                if (r != null)
                {
                    //メモリリーク防止とバッチングのため sharedMaterial を推奨
                    r.sharedMaterial = material;
                }
            }
        }
    }

    [Tooltip("鬼のスキン")]
    [SerializeField] private SkinGroup[] _taggerSkins;

    [Tooltip("逃げのスキン")]
    [SerializeField] private SkinGroup[] _runnerSkins;


    public void SetColorMaterials(Material[] taggerMaterials, Material[] runnerMaterials)
    {
        ApplyMaterialsToGroup(_taggerSkins, taggerMaterials);
        ApplyMaterialsToGroup(_runnerSkins, runnerMaterials);
    }

    //スキングループの配列に対して、マテリアル配列を順番に適用する
    private void ApplyMaterialsToGroup(SkinGroup[] skins, Material[] materials)
    {
        if (skins == null || materials == null) return;

        int count = Mathf.Min(skins.Length, materials.Length);

        for (int i = 0; i < count; i++)
        {
            if (skins[i] != null)
            {
                skins[i].ApplyMaterial(materials[i]);
            }
        }
    }
}
