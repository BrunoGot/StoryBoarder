using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Panel : MonoBehaviour
{
    GUIManager m_GUIManager;
    StoryboardManager m_sotryBoardManager;
    Sequence m_sequence;
    PanelCamera m_mainCamera;
    GameObject m_previewButton;
    private int m_panelNB;

    public void Init(Sequence _seq, GameObject _panelGUI, int _panelNb) //get a GUI separate of this script and link the handlers during initialization (kind of MVC structure)
    {
        GameObject manager = GameObject.Find("Manager");
        m_GUIManager = manager.GetComponent<GUIManager>();
        m_sotryBoardManager = manager.GetComponent<StoryboardManager>();
        m_sequence = _seq;
       
        m_previewButton = _panelGUI.transform.Find("Preview").gameObject;
        m_previewButton.GetComponent<Button>().onClick.AddListener(OpenScene);
        m_panelNB = _panelNb;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update camera ");
        Debug.Log("CurrentPanel = " + m_sequence.CurrentPanel);
        if (m_mainCamera != null && m_sotryBoardManager.GetCurrentPanelID()==GetIDPanel())
        {
            Debug.Log("update camera ");
            m_mainCamera.transform.position = m_GUIManager.GetWorldCamera().transform.position;
            m_mainCamera.transform.rotation = m_GUIManager.GetWorldCamera().transform.rotation;
        }
    }

    //handlers
    public void OpenScene()
    {
        Debug.Log("Open Scene");
        m_GUIManager.SwitchToWorldGUI();
        m_GUIManager.SetLabel(m_sequence.GetNum(), m_sequence.GetSceneNum());
        m_sotryBoardManager.SetCurrentPanel(this);
    }

    public string GetIDPanel()
    {
        return m_sequence.GetSceneNum() + m_sequence.GetNum() + m_panelNB.ToString();
    }
    public void AddCamera()
    {
        GameObject go = new GameObject("Camera");
        m_mainCamera = go.AddComponent<PanelCamera>();
        m_mainCamera.transform.SetParent(m_sequence.transform);
        m_mainCamera.InitRendering();

        //new WaitForSeconds(1f);
        //m_mainCamera.Render();
        Debug.Log("m_mainCamera.GetRenderView() = " + m_mainCamera.GetRenderView());
        m_GUIManager.SetCameraView(m_mainCamera.GetRenderView());
        m_previewButton.GetComponent<RawImage>().texture = m_mainCamera.GetRenderView();
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
