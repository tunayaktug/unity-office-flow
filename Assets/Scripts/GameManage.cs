using UnityEngine;

public enum GameStep
{
    None,
    ClickedPen,
    DrewOnBoard,
    FilledGlass,
    WateredPlant,
    ThrewGlass,
    ReadyToFinish
}

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    public GameObject finishCircle; 

    public GameStep CurrentStep { get; private set; } = GameStep.None;

    private GameObject currentHeldObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AdvanceStep(GameStep nextStep)
    {
        if ((int)nextStep == (int)CurrentStep + 1)
        {
            CurrentStep = nextStep;
            Debug.Log($"adým ilerledi: {CurrentStep}");
        }
        else
        {
            Debug.LogWarning($"yanlýþ sýrayla iþlem yapýlýyor. geçerli adým: {CurrentStep}, Ýstenen: {nextStep}");
        }
    }

    public void HoldObject(GameObject obj)
    {
        currentHeldObject = obj;
    }

    public void ReleaseObject()
    {
        currentHeldObject = null;
    }

    public bool IsHoldingSomething()
    {
        return currentHeldObject != null;
    }

    public GameObject GetHeldObject()
    {
        return currentHeldObject;
    }
    public void ShowFinishUI()
    {
        if (finishCircle != null)
            finishCircle.SetActive(true);
    }
}
