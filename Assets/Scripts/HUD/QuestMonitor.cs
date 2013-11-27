using UnityEngine;
using System.Collections;
using System.Text;
using mms.items;

public class QuestMonitor : MonoBehaviour {

	// Public, for initialization only
	public string InitQuestName;
	public PickupItemScript[] InitQuestItems;
	public TextMesh InitTextMesh;

	// Private Attributes
	private bool m_ReadyToRender = true;
	private string m_QuestName;

	// Private Associations
	private IQuestItem[] m_QuestItems;
	private TextMesh m_TextMesh;

	// Use this for initialization
	void Start () {
		m_QuestName = InitQuestName;
		m_QuestItems = InitQuestItems;
		m_TextMesh = InitTextMesh;
		BuildQuestStatus();
	}

	bool QuestStatusHasChanged()
	{
		foreach( IQuestItem item in m_QuestItems )
		{
			if( item.ItemStateChanged() == true )
			{
				return true;
			}
		}
		return false;
	}

	void BuildQuestStatus()
	{
		StringBuilder outputString = new StringBuilder( m_QuestName );
		outputString.Append ("\n\r");
		foreach( IQuestItem item in m_QuestItems )
		{
			if( !item.ItemObtained() )
			{
				outputString.Append ("\u2610");
			}
			else
			{
				outputString.Append ("\u2611");
			}
			outputString.Append( item.GetName() );
			outputString.Append ("\n\r");
		}
		m_TextMesh.text = outputString.ToString ();

	}
	
	// Update is called once per frame
	void Update () {

		if (m_ReadyToRender == true) 
		{
			BuildQuestStatus();
		}
		m_ReadyToRender = QuestStatusHasChanged ();

	}
}
