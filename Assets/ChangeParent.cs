using UnityEngine;

public class ChangeParent : MonoBehaviour
{
    [SerializeField] private Transform newParent; // 新しい親オブジェクト
    [SerializeField] private SkinPeel_ctr skinPeel_Ctr;
    [SerializeField] private PlaneController PlaneController;
    [SerializeField] private Moveborder_ctr moveborder_ctr;
    [SerializeField] private GameObject cloth_skin;

    private bool changeParent = false; // このboolがtrueになったら親を変更

    void Update()
    {
        if (changeParent && newParent != null)
        {
            cloth_skin.SetActive(true);
            //Debug.Log("StopMovement");

            GetComponent<MeshRenderer>().enabled = false;
            skinPeel_Ctr.enabled = false;
            //PlaneController.enabled = false;  
            moveborder_ctr.enabled = false;

            PlaneController.EndPoint = true;

            Vector3 localPosition = transform.localPosition;
            Vector3 localScale = transform.localScale;

            // 親を新しいオブジェクトに変更
            transform.SetParent(newParent);

            // ローカルの位置とスケールを再適用
            //transform.localPosition = localPosition;
            localPosition = Vector3.zero;
            transform.localScale = localScale;
        }
    }

    public bool IsChanged
    {
        get { return changeParent; }
        set { changeParent = value; }
    }
}
