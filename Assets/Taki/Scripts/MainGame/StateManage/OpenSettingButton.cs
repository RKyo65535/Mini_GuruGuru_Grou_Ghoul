using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenSettingButton : MonoBehaviour
{

    [SerializeField] GameFlow gameFlow;
    [SerializeField] GameObject activationCanvas;
    [SerializeField] GameObject firstForcusObject;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (gameFlow.CanSetting())
            {
                ChangeCanvas();

            }
        }
        );
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeCanvas();

        }
    }

    void ChangeCanvas()
    {
        if (gameFlow.CanSetting())
        {
            gameFlow.StartSetting();
            activationCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstForcusObject);
        }

    }
}
