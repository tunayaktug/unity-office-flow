using UnityEngine;

public class Pen : InteractableObject
{
    private bool isPickedUp = false;
    private Camera mainCam;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isReturning = false;

    public AudioClip pickupSound;
    private AudioSource audioSource;
    private void Start()
    {
        mainCam = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        if (GameManage.Instance.CurrentStep == GameStep.None && !GameManage.Instance.IsHoldingSomething())
        {
            isPickedUp = true;
            if (pickupSound != null)
                audioSource.PlayOneShot(pickupSound);
            GameManage.Instance.HoldObject(gameObject);
            GameManage.Instance.AdvanceStep(GameStep.ClickedPen);
            GetComponent<Collider>().enabled = false;
            Cursor.visible = false;
        }
    }


    private void Update()
    {
        if (isPickedUp)
        {
            
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 3f; 
            Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePosition);

            transform.position = worldPos;
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

    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    public void DropPen()
    {
        isPickedUp = false;
        isReturning = true; 
        Cursor.visible = true;
    }
}
