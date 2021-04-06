using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*PB : Sequence and scenes elements are inverted*/

public class Scene : MonoBehaviour
{
    private List<Sequence> m_sequences;
    private Sequence m_currentSequence;

    public Sequence CurrentSequence { get { return m_currentSequence; } private set {}  }

    private int m_numScene;
    // Start is called before the first frame update
    public void Init(int _numScene)
    {
        m_sequences = new List<Sequence>();
        m_numScene = _numScene;
    }

    public void AddSequence()
    {
        m_currentSequence = CreateSeq();
        m_sequences.Add(m_currentSequence);
    }

    public string GetNum()
    {
        return m_numScene.ToString();
    }

    public Sequence CreateSeq() //create a sequence
    {
        int seqNb = m_sequences.Count + 1;
        GameObject go = new GameObject("Seq_" + seqNb.ToString());
        go.transform.SetParent(this.transform);
        Sequence seq = go.AddComponent<Sequence>();
        seq.Init(this, seqNb);
        return seq;
    }
}
