using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PanelCamera : MonoBehaviour
{

    RenderTexture m_renderTexture;
    private Camera m_camera;
    //test
    bool m_tkScreen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitRendering()
    {
        m_camera = GetComponent<Camera>();
        m_renderTexture = new RenderTexture(100, 200, 24, RenderTextureFormat.ARGB32);
        m_renderTexture.Create();

        RenderTexture r = Resources.Load<RenderTexture>("TestRenderTexture");
        //Material preview = new Material(Shader.Find("UI/Unlit/Detail"));

        m_renderTexture = new RenderTexture(1920,1080,24,RenderTextureFormat.ARGB32);
        m_renderTexture.Create();

        m_camera.targetTexture = m_renderTexture;

        Debug.Log("Render text = " + m_renderTexture + " created = " + m_renderTexture.IsCreated());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public RenderTexture GetRenderView()
    {
        return m_renderTexture;
    }

    /*private void OnPostRender()
    {
        Debug.Log("take a screenshot");

        if (m_tkScreen == true)
        {

            m_tkScreen = false;
            RenderTexture rt = m_renderTexture;

            Texture2D testText2 = new Texture2D(m_renderTexture.width, m_renderTexture.height, TextureFormat.ARGB32, false);// Resources.Load<Texture2D>("Test2");
            testText2.ReadPixels(new Rect(0, 0, m_renderTexture.width, m_renderTexture.height), 0, 0);
            //testText2.Apply();
            byte[] byteArray = testText2.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/test1.png", byteArray);


            Sprite testSprite = Sprite.Create(testText2, new Rect(0, 0, 256, 256), Vector2.zero);
            //m_previewButton.GetComponent<Image>().overrideSprite = testSprite;//preview;
            //bug.Log("Resource Render text = " + Resources.Load<RenderTexture>("TestRenderTexture"));
        }
    }*/
}


