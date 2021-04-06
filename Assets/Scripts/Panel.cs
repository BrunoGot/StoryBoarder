using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Panel : MonoBehaviour
{
    GUIManager m_GUIManager;
    StoryboardManager m_sotryBoardManager;
    Sequence m_sequence;
    List<PanelCamera> m_cameras;//get list of all cameras recorded. May be useful to record differents point of view and set redo possibility //PanelCamera m_mainCamera;
    GameObject m_previewButton;
    private int m_panelNB;

    //properties
    public PanelCamera Camera { get; private set; }

    public void Init(Sequence _seq, GameObject _panelGUI, int _panelNb) //get a GUI separate of this script and link the handlers during initialization (kind of MVC structure)
    {
        GameObject manager = GameObject.Find("Manager");
        m_GUIManager = manager.GetComponent<GUIManager>();
        m_sotryBoardManager = manager.GetComponent<StoryboardManager>();
        m_sequence = _seq;
       
        m_previewButton = _panelGUI.transform.Find("Preview").gameObject;
        m_previewButton.GetComponent<Button>().onClick.AddListener(OpenScene);
        m_panelNB = _panelNb;
        m_cameras = new List<PanelCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update camera ");
        Debug.Log("CurrentPanel = " + m_sequence.CurrentPanel);
        if (Camera != null && m_sotryBoardManager.GetCurrentPanelID()==GetIDPanel())
        {
            Debug.Log("update camera ");
            Camera.transform.position = m_GUIManager.GetWorldCamera().transform.position;
            Camera.transform.rotation = m_GUIManager.GetWorldCamera().transform.rotation;
        }
    }

    //handlers
    public void OpenScene()
    {
        Debug.Log("Open Scene");
        m_GUIManager.SwitchToWorldGUI();
        m_GUIManager.SetLabel(m_sequence.GetNum(), m_sequence.GetSceneNum());
        m_sotryBoardManager.SetCurrentPanel(this);
        if (Camera != null)
        {
            m_GUIManager.GetWorldCamera().transform.position = Camera.transform.position;
            m_GUIManager.GetWorldCamera().transform.rotation = Camera.transform.rotation;
        }
    }

    public string GetIDPanel()
    {
        return m_sequence.GetSceneNum() + m_sequence.GetNum() + m_panelNB.ToString();
    }

    public void AddCamera()
    {
        if (Camera == null)
        {
            GameObject go = new GameObject("Camera");
            Camera = go.AddComponent<PanelCamera>();
            Camera.transform.SetParent(m_sequence.transform);
            m_cameras.Add(Camera);
        }
        Camera.InitRendering();

        //new WaitForSeconds(1f);
        //m_mainCamera.Render();
        Debug.Log("Camera.GetRenderView() = " + Camera.GetRenderView());
        m_GUIManager.SetCameraView(Camera.GetRenderView());
        m_previewButton.GetComponent<RawImage>().texture = Camera.GetRenderView();
        //  m_tkScreen = true;
    }

  /*  private void OnPostRender()
    {
        Debug.Log("take a screenshot");

        if (m_tkScreen == true) { 

            m_tkScreen = false;
            RenderTexture rt = m_renderTexture;

            Texture2D testText2 = new Texture2D(m_renderTexture.width, m_renderTexture.height, TextureFormat.ARGB32,false);// Resources.Load<Texture2D>("Test2");
            testText2.ReadPixels(new Rect(0, 0, m_renderTexture.width, m_renderTexture.height), 0, 0);
            //testText2.Apply();
            byte[] byteArray = testText2.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/test1.png", byteArray);

            
            Sprite testSprite = Sprite.Create(testText2, new Rect(0, 0, 256, 256), Vector2.zero);
            m_previewButton.GetComponent<Image>().overrideSprite = testSprite;//preview;
            //bug.Log("Resource Render text = " + Resources.Load<RenderTexture>("TestRenderTexture"));
        }
        

    }*/
}
