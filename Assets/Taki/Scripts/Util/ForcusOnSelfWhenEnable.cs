using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForcusOnSelfWhenEnable : MonoBehaviour
{


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

    }

}
