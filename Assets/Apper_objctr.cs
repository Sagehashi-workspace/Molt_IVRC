using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apper_objctr : MonoBehaviour
{
    [SerializeField] private ChangeParent ChangeParent;
    [SerializeField] private Cloth clothComponent;
    [SerializeField] private GameObject skin;

    private bool apper_obj = false;
    private ClothSkinningCoefficient[] originalSkinningCoefficients;

    // Update is called once per frame
    void Update()
    {
        apper_obj = ChangeParent.IsChanged;

        if (apper_obj)
        {
            transform.localPosition = skin.transform.localPosition;
            transform.localRotation = skin.transform.localRotation;
            //transform.localScale = skin.transform.localScale;

            // RecreateCloth();
            skin.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    void RecreateCloth()
    {
        // Clothコンポーネントを削除
        if (clothComponent != null)
        {
            Destroy(clothComponent);
        }

        // 新しいClothコンポーネントを追加
        clothComponent = gameObject.AddComponent<Cloth>();
        clothComponent.coefficients = originalSkinningCoefficients;

        // 必要に応じて他のCloth設定をコピー
        // clothComponent.stretchingStiffness = 1.0f; など
    }
}
