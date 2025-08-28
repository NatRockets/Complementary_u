using UnityEngine;
using UnityEngine.UI;

public class TheorySequence : MonoBehaviour //enabling needed sequence
{
    [SerializeField] private TestBlock[] blocks;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    
    private int iterator;

    private void Awake()
    {
        foreach (var t in blocks)
        {
            t.BindBlock();
        }
    }
    
    private void OnEnable()
    {
        prevButton.onClick.AddListener(() => Shift(-1));
        nextButton.onClick.AddListener(() => Shift(1));
        
        iterator = 0;

        for (int i = 1; i < blocks.Length; i++)
        {
            blocks[i].HideForce();
        }
        
        blocks[0].Show();
        CheckNavigation();
    }

    private void OnDisable()
    {
        prevButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
    }

    private void Shift(int value)
    {
        blocks[iterator].Hide();
        iterator += value;
        blocks[iterator].Show();
        CheckNavigation();
    }
    
    private void CheckNavigation()
    {
        prevButton.interactable = iterator > 0;
        nextButton.interactable = iterator < blocks.Length - 1;
    }
}
