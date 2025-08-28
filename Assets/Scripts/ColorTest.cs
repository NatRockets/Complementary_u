using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
    [SerializeField] private GameObject startObject;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject finishObject;
    [SerializeField] private Button finishButton;
    [SerializeField] private Text resText;
    [SerializeField] private StringProvider sourceProvider;
    [SerializeField] private ShadeCalculator shadeCalculator;

    [Header("Fields")] [SerializeField] private GameObject source;
    [SerializeField] private Text questionAsset;
    [SerializeField] private Image sampleImage;
    [SerializeField] private AnswerButton[] answerButtons;

    private int rightAnswers;
    private int questionCounter;

    private List<ColorQuestion> currentTestAssets = new();

    private void Awake()
    {
        foreach (AnswerButton answerButton in answerButtons)
        {
            answerButton.InitAnswer();
        }

        startButton.onClick.AddListener(StartTest);
        finishButton.onClick.AddListener(FinishTest);
    }

    private void OnEnable()
    {
        startObject.SetActive(true);
        finishObject.SetActive(false);
        source.SetActive(false);
    }

    private void StartTest()
    {
        startObject.SetActive(false);

        rightAnswers = 0;
        FormTestAssets();
        questionCounter = 0;
        source.SetActive(true);
        InitQuestion();
    }

    private void FormTestAssets()
    {
        currentTestAssets.Clear();
        currentTestAssets = sourceProvider.GetQuestionSequence(15);
        Shuffle(currentTestAssets);
    }

private void OnAnswerPicked(bool right)
    {
        sampleImage.enabled = true;
        
        foreach (var but in answerButtons)
        {
            but.ResetAnswer();
        }
        
        if (right)
        {
            rightAnswers++;
        }
        
        questionCounter++;
        if (questionCounter >= currentTestAssets.Count)
        {
            OnTestFinished();
        }
        else
        {
            InitQuestion();
        }
    }

    private void InitQuestion()
    {
        sampleImage.enabled = false;
        questionAsset.text = "";
        questionAsset.DOText(currentTestAssets[questionCounter].question, 1f)
            .OnComplete(() =>
            {
                Shuffle(answerButtons);
                shadeCalculator.GenerateColorOptions(currentTestAssets[questionCounter].rule, out Color sample, out Color var1, out Color var2 );
                if (sample != default)
                {
                    sampleImage.color = sample;
                    sampleImage.enabled = true;
                }
                
                answerButtons[0].SetAnswer(var1, () => OnAnswerPicked(true));
                answerButtons[1].SetAnswer(var2, () => OnAnswerPicked(false));
            });
    }

    private void OnTestFinished()
    {
        foreach (var but in answerButtons)
        {
            but.ResetAnswer();
        }
        
        source.SetActive(false);
        
        resText.text = $"{rightAnswers}/{currentTestAssets.Count}";
        finishObject.SetActive(true);
    }

    private void FinishTest()
    {
        finishObject.SetActive(false);
        startObject.SetActive(true);
    }
    
    private void Shuffle<T> (T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = Random.Range(0, n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
    
    private void Shuffle<T> (List<T> array)
    {
        int n = array.Count;
        while (n > 1) 
        {
            int k = Random.Range(0, n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}
