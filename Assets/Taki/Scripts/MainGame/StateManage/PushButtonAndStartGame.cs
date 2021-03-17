using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PushButtonAndStartGame : MonoBehaviour
{

    [SerializeField] GameFlow gameflow;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);//自身を選択状態にする。
        GetComponent<Button>().
        GetComponent<Button>().onClick.AddListener(()=> 
        { 
            gameflow.StartGame(); //ボタンを押したらゲームスタート
            gameObject.SetActive(false);

        });
    }

}
