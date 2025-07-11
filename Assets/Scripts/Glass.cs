using UnityEngine;

public class Glass : InteractableObject
{
    private bool isPickedUp = false;
    private bool isFilled = false;
    private bool isReturning = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Camera mainCam;

    public Material filledMaterial; // Su dolunca kullanýlacak materyal
    public Material originalMaterial;
    private Renderer rend;

    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        mainCam = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void OnInteract()
    {
       
        if ((GameManage.Instance.CurrentStep == GameStep.DrewOnBoard
              || GameManage.Instance.CurrentStep == GameStep.FilledGlass
              || GameManage.Instance.CurrentStep == GameStep.WateredPlant)
            && !GameManage.Instance.IsHoldingSomething())
        {
            isPickedUp = true;
            if (pickupSound != null)
                audioSource.PlayOneShot(pickupSound);
            GameManage.Instance.HoldObject(gameObject);
            Cursor.visible = false;
            GetComponent<Collider>().enabled = false;
            Debug.Log($"bardak alýndý. (Step: {GameManage.Instance.CurrentStep})");
        }
    }

    void Update()
    {
        if (isPickedUp)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 3f;
            transform.position = mainCam.ScreenToWorldPoint(mousePos);
        }

        if (isReturning)
        {
            float speed = 5f;
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                isReturning = false;
                GetComponent<Collider>().enabled = true;
            }
        }
    }

    public bool IsPickedUp() => isPickedUp;
    public bool IsFilled() => isFilled;

    public void FillGlass()
    {
        isFilled = true;
        isPickedUp = false;
        GameManage.Instance.ReleaseObject();

        Cursor.visible = true;
        rend.material = filledMaterial;

        isReturning = true;
        GameManage.Instance.AdvanceStep(GameStep.FilledGlass);
        Debug.Log("Bardak su ile doldu.");
    }

    public void DropGlass()
    {
        
        isPickedUp = false;
        GameManage.Instance.ReleaseObject();
        Cursor.visible = true;
    
    }

    public void SetFilled(bool filled)
    {
        isFilled = filled;
        if (filled)
            rend.material = filledMaterial;
        else
            rend.material = originalMaterial; 
    }
}
