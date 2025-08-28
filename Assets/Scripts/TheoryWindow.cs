using UnityEngine;
using UnityEngine.UI;

public class TheoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject[] sequenceObjects;
    [SerializeField] private Button quitButton;//to quit current sequence
    [SerializeField] private GameObject sequencePickScreen;
    [SerializeField] private Button[] pickButtons;
    [SerializeField] private GameObject navigationSource;
    
    private GameObject currentSequence;

    private void Awake()
    {
        for (int i = 0; i < pickButtons.Length; i++)
        {
            var i1 = i;
            
            pickButtons[i].onClick.AddListener(() =>
            {
                sequencePickScreen.SetActive(false);
                navigationSource.SetActive(true);
                sequenceObjects[i1].SetActive(true);
                currentSequence = sequenceObjects[i1];
            });
        }
        
        quitButton.onClick.AddListener(() =>
        {
            sequencePickScreen.SetActive(true);
            currentSequence?.SetActive(false);
            navigationSource.SetActive(false);
        });
    }
    
    private void OnEnable()
    {
        sequencePickScreen.SetActive(true);
        navigationSource.SetActive(false);
    }

    private void OnDisable()
    {
        currentSequence?.SetActive(false);
    }
}
