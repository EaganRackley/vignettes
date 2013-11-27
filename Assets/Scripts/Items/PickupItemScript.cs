using UnityEngine;
using System.Collections;
using mms.items;
using mms.common;
using mms.input;


public class PickupItemScript : MonoBehaviour, IQuestItem {

	// Public properties used only for initialization!
	public string InitItemName = "";
	public ITEM_TYPE InitItemType = ITEM_TYPE.UNKNOWN;
	public TextMesh InitTextMesh;
	public InputProvider InitInputProvider;

	// Private attributes
	private string m_ItemName;
	private ITEM_TYPE m_ItemType;
	private bool m_Obtained = false;
	private bool m_LastState = false;
	private InputProvider m_InputProvider;

	// Private associations
	private TextMesh m_TextMesh;

	// Use this for initialization
	void Start () 
	{
		m_ItemName = InitItemName;
		m_ItemType = InitItemType;
		m_TextMesh = InitTextMesh;
		m_TextMesh.text = m_ItemName;
		m_InputProvider = InitInputProvider;
	}

	public string ItemName {
		get {
			return m_ItemName;
		}
		set {
			m_ItemName = value;
		}
	}

	public ITEM_TYPE ItemType {
		get {
			return m_ItemType;
		}
		set {
			m_ItemType = value;
		}
	}


	void OnTriggerStay( Collider other )
	{
		if (other.tag == Common.PLAYER_TAG) 
		{
			if( (!m_Obtained) && (PlayerHasPressedActionKey()) )
			{
				m_Obtained = true;
				StartCoroutine( HideItem() );
			}
		}
	}

	bool PlayerHasPressedActionKey()
	{
		if (m_InputProvider.Data == null)
		{
			print("Input provider is null!");
			return false;
		}

		if (m_InputProvider.Data.ActionState == ACTION_STATE.ACTIVE) 
		{
			return true;
		}

		return false;

	}

	#region IQuestItem implementation

	public string GetName ()
	{
		return m_ItemName;
	}

	ITEM_TYPE IQuestItem.GetType ()
	{
		return m_ItemType;
	}

	public bool ItemObtained ()
	{
		return m_Obtained;
	}

	public bool ItemStateChanged()
	{
		if( m_LastState != m_Obtained )
		{
			m_LastState = m_Obtained;
			return true;
		}

		return false;
	}

	public IEnumerator HideItem()
	{
		// Scale our item until it seems to have vanished...
		Vector3 size = transform.localScale;
		bool finishedHiding = false;
		while( !finishedHiding )
		{
			 float scalePerSecond = 1.00f * Time.deltaTime;
			if(size.x > 0.0f ) size.x -= scalePerSecond;
			if(size.y > 0.0f ) size.y -= scalePerSecond;
			if(size.z > 0.0f ) size.z -= scalePerSecond;
			transform.localScale = size;
			if( (size.x <= 0.0f) && (size.y <= 0.0f) && (size.z <= 0.0f) )
			{
				finishedHiding = true;
			}
			yield return true;
		}
	}

	void ShowItem()
	{

	}
	
	#endregion

}
