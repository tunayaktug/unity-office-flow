using UnityEngine;

public class Board : InteractableObject
{
    [SerializeField] private Material blackMaterial;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public override void OnInteract()
    {
        if (GameManage.Instance.CurrentStep == GameStep.ClickedPen)
        {
            Pen pen = FindObjectOfType<Pen>();

            if (pen != null && pen.IsPickedUp())
            {
                rend.material = blackMaterial;
                GameManage.Instance.AdvanceStep(GameStep.DrewOnBoard);

                pen.DropPen(); 
                GameManage.Instance.ReleaseObject();
                Debug.Log("tahta siyaha boyandý, kalem býrakýldý.");
            }
        }
    }

}
