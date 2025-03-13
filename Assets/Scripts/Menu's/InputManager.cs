using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    int currentIndex = 0;
    List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject canvas;

    private void Start()
    {
        foreach (Transform item in canvas.GetComponentInChildren<Transform>())
        {
            item.gameObject.TryGetComponent<Button>(out var button);
            if (button != null)
            {
                buttons.Add(item.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex < 0) currentIndex = buttons.Count;

        if (currentIndex > buttons.Count - 1) currentIndex = 0;


        if (EventSystem.current.currentSelectedGameObject != buttons[currentIndex])
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex]);
        }
    }

    public void MenuUp()
    {
        currentIndex -= 1;
    }
    public void MenuDown()
    {
        currentIndex += 1;
    }

    public void Quit()
    {
        AppHelper.Quit();
    }
}
