using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectIconHandler : MonoBehaviour
{
    private Camera playerCamera;
    private float scaleFactor = 0.1f;
    private Canvas canvas;
    private float originalCanvasYPosition;
    private RawImage iconImage;
    private TextMeshProUGUI durationText;
    private GameObject iconObject;
    public StatusEffectIconHandler iconScript;
    
    void Awake()
    {
        playerCamera = Camera.main;
        canvas = gameObject.GetComponentInChildren<Canvas>();
        originalCanvasYPosition = canvas.transform.position.y;
        iconImage = gameObject.GetComponentInChildren<RawImage>();
        durationText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        SetIcon("frozen");
        UpdateDuration(9);
    }
    
    void Update()
    {
        if (playerCamera)
        { 
            gameObject.transform.rotation = playerCamera.transform.rotation;
            float camHeight;
            if (playerCamera.orthographic)
            {
                camHeight = playerCamera.orthographicSize * 2;
            }
            else
            {
                float distanceToCamera = Vector3.Distance(playerCamera.transform.position, transform.position);
                camHeight = 2.0f * distanceToCamera * Mathf.Tan(Mathf.Deg2Rad * (playerCamera.fieldOfView * 0.5f));
            }
            float scale = camHeight * scaleFactor;
            if ((scale >= 4)) return;
            transform.localScale = new Vector3(scale, scale, scale);
            canvas.transform.localPosition = new Vector3(0.0f, originalCanvasYPosition * (scale * scaleFactor), 0.0f);
        }
    }

    public void CreateIcon(string iconName)
    {
        iconObject = Instantiate(Resources.Load("StatusEffectIcons/StatusEffectIcon3D")) as GameObject;
        if (!(iconObject != null)) return;
        iconScript = iconObject.GetComponent<StatusEffectIconHandler>();
        iconScript.SetIcon(iconName);
    }

    public void SetIcon(string iconName)
    {
        iconImage.texture = StatusEffect.icons[iconName];
    }

    public void UpdateDuration(float seconds)
    {
        durationText.text = seconds + "s";
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void DestroyIcon()
    {
        Destroy(gameObject);
    }
}
