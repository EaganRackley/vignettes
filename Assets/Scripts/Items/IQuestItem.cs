using UnityEngine;
using System.Collections;

namespace mms.items
{
	public interface IQuestItem
	{
		string GetName();
		ITEM_TYPE GetType();
		bool ItemObtained();
		bool ItemStateChanged();
		IEnumerator HideItem();
	}
}
