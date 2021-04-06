using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dummiesman;
using System;
using System.Runtime.InteropServices;

//to do make it as a singleton
/*Manage the World GUI and dashboard GUI*/
public class GUIManager : MonoBehaviour
{
    private GameObject m_storyboardGUI;
    private StoryboardManager m_storyboardManager;
    private CameraController m_CameraController;
    private GameObject m_mainCamera;

    //GUI elements
    private GameObject m_worldGUI;
    private GameObject m_PanelGrids;
    private GameObject m_cameraViewGUI;
    private Text m_sceneLabel;
    private Texture m_renderTexture;

    //Flags
    private bool m_camGUI;

    //resources
    private GameObject m_guiPanel;
    private GameObject m_buttonPanel;
    /*
    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();
    System.Windows.Forms.OpenFileDialog m_openFileDialog;*/
    // Start is called before the first frame update
    void Start()
    {
        //init variables
        m_guiPanel = Resources.Load<GameObject>("GUI/PanelTemplate");

        m_storyboardGUI = GameObject.Find("StoryboardGUI");
        m_storyboardManager = GameObject.Find("Manager").GetComponent<StoryboardManager>();
        m_worldGUI = GameObject.Find("WorldGUI");
        m_PanelGrids = GameObject.Find("PanelGrid");
        m_buttonPanel = m_PanelGrids.transform.Find("AddPanelButton").gameObject;
        m_CameraController = GameObject.Find("Controllers").GetComponent<CameraController>();
        m_mainCamera = GameObject.Find("Main Camera");
        m_sceneLabel = GameObject.Find("SceneLabel").GetComponent<Text>();
        //camera GUI
        m_cameraViewGUI = GameObject.Find("CameraViewGUI");
        m_renderTexture = m_cameraViewGUI.transform.Find("RawImage").GetComponent<RawImage>().material.mainTexture;
        //AddNewPanel();
  //      m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
        //init states
        Init();
    }

    void Init() //initialize the state of the gui
    {
        SwitchToSBGUI();
        m_CameraController.SetCamera(m_mainCamera);
    }
    //worldGUI
    public void SetLabel(string _seqLabel, string _sceneLabel)
    {
        m_sceneLabel.text = "Scene : "+ _sceneLabel+" | seq : "+ _seqLabel;
    }

    public void CreateCamera()
    {
        m_storyboardManager.CreateCamera();
        DisplayCameraGUI(true);
    }


    public void DisplayCameraGUI(bool _val)
    {
        m_camGUI = _val;
        m_cameraViewGUI.SetActive(m_camGUI);
    }

    public Camera GetWorldCamera()
    {
        return m_mainCamera.GetComponent<Camera>();
    }
/*    public CanvasRenderer GetCameraCanvas()
    {
        return m_renderTexture;
    }
    */
    //Display the camera view as texture in the GUI
    public void SetCameraView(RenderTexture _rTexture)
    {
        m_renderTexture = _rTexture;
        Debug.Log("m_renderTexture  = " + m_renderTexture);
        m_cameraViewGUI.transform.Find("RawImage").GetComponent<RawImage>().texture = m_renderTexture;
    }

    //storyboardGUI
    //gui events
    public GameObject CreateGUIPanel()
    {
        m_buttonPanel.transform.SetParent(null);
        GameObject guiPanel = InstantiateGUIPanel();
        //put this panel at last object of the grid and replace the "plus button" at the end of the grid
        guiPanel.transform.SetParent(m_PanelGrids.transform);
        m_buttonPanel.transform.SetParent(m_PanelGrids.transform);
        return guiPanel;
    }

    //trigged when the user click on the "add panel button"
    public void AddPanelHandler()
    {
        m_storyboardManager.AddPanel();
    }

    public void ImportFBX()
    {

        new OBJLoader().Load("D:\\Users\\Documents\\Props\\Tests\\Cube.obj");/*
        m_openFileDialog.InitialDirectory = Application.dataPath;
        m_openFileDialog.Filter = "obj files (*.obj)|*.obj";
        m_openFileDialog.FilterIndex = 2;
        //  openFileDialog.RestoreDirectory = false;
        m_openFileDialog.FileOk+=OpenFile;
        m_openFileDialog.ShowDialog();*/

        //        GetComponent<FBXImporter>().ParseFBX("D:\\Users\\Documents\\Props\\Tests\\CubeMaya2012.fbx");
    }

    public void OpenFile(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Debug.Log("Open a file");
        /*if (m_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {*/
            //Get the path of specified file*/
          //  string filePath = m_openFileDialog.FileName;
         //   Debug.Log("Selected file = " + filePath);
            new OBJLoader().Load("D:\\Users\\Documents\\Props\\Tests\\Cube.obj");
       // }
    }
    //SB methods
    GameObject InstantiateGUIPanel() {
        GameObject guiPanel = Instantiate(m_guiPanel);
        return guiPanel;
    }

    // globals mehtods
    public void SwitchToWorldGUI()
    {
        m_storyboardGUI.SetActive(false);
        m_CameraController.gameObject.SetActive(true);
        m_worldGUI.SetActive(true);
    }

    public void SwitchToSBGUI() //Hide world GUI and active the storyboard gui
    {
        m_worldGUI.SetActive(false);
        m_storyboardGUI.SetActive(true);
        m_CameraController.gameObject.SetActive(false);
        DisplayCameraGUI(false);

    }


    public void SwitchGUIMode() //switch  to display world or storyboard GUI
    {
        if (m_storyboardGUI.activeSelf == true)
        {
            SwitchToWorldGUI();
        }
        else if (m_worldGUI.activeSelf == true)
        {
            SwitchToSBGUI();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
