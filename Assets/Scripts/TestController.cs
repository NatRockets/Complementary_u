using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct TestAssets
{
    public TestBlock targetPanel;
    public Button toButton;
    public Button fromButton;
    public GameObject[] linkedObjects;
    public bool switchMenu;
}

public class TestController : MonoBehaviour
{
    [SerializeField] private TestAssets[] groups;
    [SerializeField] private GameObject startupPanel;

    private void Start()
    {
        foreach (var group in groups)
        {
            SetupGroup(group);
        }
    }

    private void SetupGroup(TestAssets group)
    {
        group.targetPanel.BindBlock();
        
        group.toButton.onClick.AddListener(() =>
        {
            if (group.switchMenu)
            {
                startupPanel.SetActive(false);
            }
            foreach (var obj in group.linkedObjects)
            {
                obj.SetActive(true);
            }
            group.targetPanel.Show(true, true);
        });
        group.fromButton.onClick.AddListener(() =>
        {
            foreach (var obj in group.linkedObjects)
            {
                obj.SetActive(false);
            }
            group.targetPanel.Hide(true, true, () =>
            {
                if (group.switchMenu)
                {
                    startupPanel.SetActive(true);
                }
            });
        });
    }
}
