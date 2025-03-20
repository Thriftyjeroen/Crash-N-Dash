using NUnit.Framework;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    int currentIndex = 0;
    List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject canvas;

    private void Start()
    {
        if (canvas == null) return;
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
        if (buttons.Count == 0) return;

        if (currentIndex < 0) currentIndex = buttons.Count - 1;

        if (currentIndex > buttons.Count - 1) currentIndex = 0;


        if (EventSystem.current.currentSelectedGameObject != buttons[currentIndex])
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex]);
        }
    }

    public void MenuUp(InputAction.CallbackContext context)
    {        
        if (context.performed) currentIndex -= 1;
    }
    public void MenuDown(InputAction.CallbackContext context)
    {
        if (context.performed) currentIndex += 1;
    }


    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
        }
    }

    public void ChangeScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
