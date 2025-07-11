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
                    Debug.Log("bardak çöpe atýldý.");
                    GameManage.Instance.AdvanceStep(GameStep.ThrewGlass);
                    GameManage.Instance.ShowFinishUI(); 
                }
                else
                {
                    Debug.LogWarning("bardak elde deðil, çöpe atýlamaz!");
                }
            }
            else
            {
                Debug.LogWarning("çöpe atýlacak bardak bulunamadý.");
            }
        }
        else
        {
            Debug.Log("çöp henüz aktif deðil. Adým: " + GameManage.Instance.CurrentStep);
        }

    }
}