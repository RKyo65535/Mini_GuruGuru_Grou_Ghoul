using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseSettingCanvas : MonoBehaviour
{
    [SerializeField] GameFlow gameFlow;
    [SerializeField] GameObject inactivationCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (gameFlow.IsSetting())
            {
                gameFlow.EndSetting();
                inactivationCanvas.SetActive(false);

            }

        }
        );
    }
}
