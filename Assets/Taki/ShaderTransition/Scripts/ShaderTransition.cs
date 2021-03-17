using UnityEngine;
using UnityEngine.SceneManagement;

namespace Taki.TakiTransition
{
    public class ShaderTransition : MonoBehaviour
    {

        Material material;//トランジションマテリアルをSpriteRendererから取得する。
        float str;//変化の度合い。0～1の値をとり、その大きさでシェーダーの効力を変える。


        //enumで状態を定義。
        //最初呼び出されたり、TransitionOpen()をしている間はOpen
        //TransitionCloce()をするとcloseになる。
        public enum Mode
        {
            open,
            close
        }
        Mode mode;

        //何Frameかけてトランジションを完結させるかを指定してもらう
        [SerializeField] int flame;

        //シーン遷移を非同期で行うため、これでシーン遷移していいときに命令をする感じ
        AsyncOperation asyncOperation;


        // Startでは、open状態にして、マテリアルを取得、そして、シェーダーの強さを1にしている。
        void Start()
        {
            mode = Mode.open;
            material = GetComponent<SpriteRenderer>().material;
            material.SetFloat("_SliderParam", 1);
        }

        // 状態に応じて振る舞いを変えている。
        //　openならだんだん弱める
        //　closeならだんだん強め、最後には遷移を許可する。

        void FixedUpdate()
        {
            if(str < 1)
            {
                str += 1 / (float)flame;
                switch (mode)
                {
                    case Mode.open:
                        material.SetFloat("_SliderParam", 1-str);
                        break;
                    case Mode.close:
                        material.SetFloat("_SliderParam", str);
                        break;
                    default:
                        break;
                }

            }
            else if(mode == Mode.close)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }


        /// <summary>
        /// openでは状態をリセットするのみである。
        /// </summary>
        public void TransitionOpen()
        {
            if(mode == Mode.close)
            {
                mode = Mode.open;
                str = 0;
                material.SetFloat("_SliderParam", 1);
            }

        }

        /// <summary>
        /// cloceでは、状態を変更し、読み込み先を非同期で読み込み始める。
        /// </summary>
        /// <param name="destination"></param>
        public void TransitionClose(string destination)
        {
            if(mode == Mode.open)
            {
                mode = Mode.close;

                str = 0;
                material.SetFloat("_SliderParam", 0);
                asyncOperation = SceneManager.LoadSceneAsync(destination);
                asyncOperation.allowSceneActivation = false;
            }

        }

    }
}

