using UnityEngine;
using UnityEngine.UI;

public class PaintEnvironment : MonoBehaviour
{
    [SerializeField] private PaintTarget[] targets;
    [SerializeField] private TestBlock pickerPanel;
    [SerializeField] private FlexibleColorPicker flexibleColorPicker;
    [SerializeField] private Transform burstObject;
    [SerializeField] private Button pickerButton;
    [SerializeField] private Image currentHue;
    
    private ParticleSystem burst;
    private ParticleSystem.MainModule burstMain;

    private bool isShown;

    private void Awake()
    {
        burst = burstObject.GetComponent<ParticleSystem>();
        burstMain = burst.main;
        
        pickerPanel.BindBlock();
        pickerButton.onClick.AddListener(() =>
        {
            if (!isShown)
            {
                pickerPanel.Show();
            }
            else
            {
                pickerPanel.Hide();
                currentHue.color = flexibleColorPicker.color;
            }
            isShown = !isShown;
        });
    }
    
    private void Start()
    {
        foreach (var target in targets)
        {
            target.BindTarget(flexibleColorPicker, PlayBurst);
        }
    }

    private void OnEnable()
    {
        burst.Stop();
        currentHue.color = flexibleColorPicker.color;
        pickerPanel.HideForce();
        isShown = false;
    }

    private void PlayBurst(Vector3 position)
    {
        burst.Stop();
        burstMain.startColor = flexibleColorPicker.color;
        burstObject.position = new Vector3(position.x, position.y + 1f, position.z);
        burst.Play();
    }
}
