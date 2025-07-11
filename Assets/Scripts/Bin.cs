using UnityEngine;

public class Bin : InteractableObject
{
    public override void OnInteract()
    {
        if (GameManage.Instance.CurrentStep == GameStep.WateredPlant)
        {
            Glass glass = FindObjectOfType<Glass>();
            if (glass != null)
            {
                if (glass.IsPickedUp())
                {
                    glass.DropGlass();
                    Destroy(glass.gameObject);
                    Debug.Log("bardak ��pe at�ld�.");
                    GameManage.Instance.AdvanceStep(GameStep.ThrewGlass);
                    GameManage.Instance.ShowFinishUI(); 
                }
                else
                {
                    Debug.LogWarning("bardak elde de�il, ��pe at�lamaz!");
                }
            }
            else
            {
                Debug.LogWarning("��pe at�lacak bardak bulunamad�.");
            }
        }
        else
        {
            Debug.Log("��p hen�z aktif de�il. Ad�m: " + GameManage.Instance.CurrentStep);
        }

    }
}