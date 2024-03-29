
using UnityEngine;

namespace _Scripts.Scenes.SPPBScene
{
    public class Skeleton2DViewInSppbScene : MonoBehaviour
    {
        public GameObject colorSourceManager;
        private Skeleton2DManagerInSppbScene colorManager;
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
        
            colorManager = colorSourceManager.GetComponent<Skeleton2DManagerInSppbScene>();
            if (colorManager == null)
            {
                return;
            }
        
            gameObject.GetComponent<Renderer>().material.mainTexture = colorManager.GetColorTexture();
        }
    }
}