using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{

    private Scene m_scene; //the scene parent of this sequence
    private List<Panel> m_panels;
    private Panel m_currentPanel;
    private int m_numSeq; //sequence number
    
    public Panel CurrentPanel { get { return m_currentPanel; } private set { } }
    public int PanelsCount { get { return m_panels.Count; } }

    public void Init(Scene _scene, int _numSeq)
    {
        m_scene = _scene;
        m_panels = new List<Panel>();
        m_numSeq = _numSeq;
    }

    public void AddPanel(Panel _panel)
    {
        m_currentPanel = _panel;
        m_panels.Add(m_currentPanel);
    }

    public string GetNum()
    {
        return m_numSeq.ToString();
    }

    public string GetSceneNum()
    {
        return m_scene.GetNum();
    }
}
