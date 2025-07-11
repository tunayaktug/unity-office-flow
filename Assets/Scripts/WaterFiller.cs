using UnityEngine;

public class WaterFiller : InteractableObject
{
    public override void OnInteract()
    {
        if (GameManage.Instance.CurrentStep == GameStep.DrewOnBoard)
        {
            Glass glass = FindObjectOfType<Glass>();
            if (glass != null && glass.IsPickedUp())
            {
                glass.FillGlass();
            }
        }
    }
}
