using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Skeleton2DView
{
    public class Skeleton2DView : MonoBehaviour
    {
        [FormerlySerializedAs("_ColorSourceManager")] 
        public GameObject colorSourceManager;
        private Skeleton2DManager colorManager;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private bool iscolorSourceManagerNull;

        private void Start ()
        {
            iscolorSourceManagerNull = colorSourceManager == null;
            gameObject.GetComponent<Renderer>().material.SetTextureScale(MainTex, new Vector2(-1, 1));
        }

        private void Update()
        {
            if (iscolorSourceManagerNull)
            {
                return;
            }
        
            colorManager = colorSourceManager.GetComponent<Skeleton2DManager>();
            if (colorManager == null)
            {
                return;
            }
        
            gameObject.GetComponent<Renderer>().material.mainTexture = colorManager.GetColorTexture();
        }
    }
}
