using UnityEngine;


public class FinishCircle : MonoBehaviour
{
    public AudioClip finishSound;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        if (GameManage.Instance.CurrentStep == GameStep.ThrewGlass)
        {
            Debug.Log("seviye tamamlandý");
            GameManage.Instance.AdvanceStep(GameStep.ReadyToFinish);
            Destroy(gameObject);

        }
    }
}
