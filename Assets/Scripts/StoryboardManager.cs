using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 Todo : make each scene as an empty object. In this object create each sequence as an empty object. in this sequence create all camera assigned to these sequence. assign the selected camera to the panel 
*/


public class StoryboardManager : MonoBehaviour
{
    //gui link (will be a singleton)
    private GUIManager m_guiManager;
    private GameObject m_worldEnv;//the world envornement
    private List<Scene> m_scenes;
    private Scene m_currentScene;
    private Panel m_currentPanel;

    // Start is called before the first frame update
    void Start()
    {
        m_scenes = new List<Scene>();
        m_guiManager = GameObject.Find("Manager").GetComponent<GUIManager>();
        m_worldEnv = GameObject.Find("WorldEnvironment");
        Init();
    }

    void Init()
    {
        AddScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScene()
    {
        m_currentScene = CreateScene(); //new Scene(m_scenes.Count+1);
        m_scenes.Add(m_currentScene);
    }

    public void AddSequence()
    {
        m_currentScene.AddSequence();
    }

    //can be trigged from the gui or the user in world scene
    public void AddPanel()
    {
        AddSequence();
        GameObject panelGUI = m_guiManager.CreateGUIPanel();
        Panel panel = CreatePanel(panelGUI);
        m_currentScene.CurrentSequence.AddPanel(panel);
    }

    private Panel CreatePanel(GameObject _panelGUI)
    {
        GameObject go = new GameObject("panel");
        Panel panel = go.AddComponent<Panel>();
        panel.Init(m_currentScene.CurrentSequence, _panelGUI, m_currentScene.CurrentSequence.PanelsCount+1);
        panel.transform.SetParent(m_currentScene.CurrentSequence.transform);
        return panel;
    }

    //
    private Scene CreateScene()
    {
        int sceneNb = m_scenes.Count + 1;
        GameObject go = new GameObject("Scene_" + sceneNb.ToString());
        go.transform.SetParent(m_worldEnv.transform);
        Scene scene = go.AddComponent<Scene>();
        scene.Init(sceneNb);
        return scene;
    }

    public void CreateCamera()
    {
        m_currentScene.CurrentSequence.CurrentPanel.AddCamera();
    }

    public void SetCurrentPanel(Panel _panel)
    {
        m_currentPanel = _panel;
    }
    public string GetCurrentPanelID()
    {
        return m_currentPanel.GetIDPanel();
    }


}
