using UnityEngine;

public class Flower : InteractableObject
{
    public GameObject waterEffectPrefab; 
    public AudioClip wateringSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public override void OnInteract()
    {
        if (GameManage.Instance.CurrentStep == GameStep.FilledGlass)
        {
            Glass glass = FindObjectOfType<Glass>();
            if (glass != null && glass.IsPickedUp() && glass.IsFilled())
            {
                Debug.Log("çiçek sulama baþladý");

             
                if (waterEffectPrefab != null)
                {
                    Vector3 effectPosition = transform.position + Vector3.up * 0.5f;
                    GameObject effectInstance = Instantiate(waterEffectPrefab, effectPosition, Quaternion.identity);
                    Destroy(effectInstance, 2f); 
                }
                else
                {
                    Debug.LogWarning("waterEffectPrefab atanmamýþ!");
                }

                
                if (wateringSound != null)
                {
                    audioSource.PlayOneShot(wateringSound);
                }

                glass.SetFilled(false);

           
                GameManage.Instance.AdvanceStep(GameStep.WateredPlant);
            }
        }
    }
}
