using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EnumerableView view = GetComponent<EnumerableView>();

		view.Bind(new int[] { 1, 2, 3 });
		view.Draw();

		foreach (string key in Davfalcon.Engine.SystemData.Current.Effects.Names)
		{
			Debug.Log(key);
		}

		foreach (string key in Davfalcon.Engine.SystemData.Current.Buffs.Keys)
		{
			Debug.Log(key);
		}

		foreach (string key in Davfalcon.Engine.SystemData.Current.Equipment.Keys)
		{
			Debug.Log(key);
		}

		foreach (string key in Davfalcon.Engine.SystemData.Current.Spells.Keys)
		{
			Debug.Log(key);
		}

		foreach (string key in Davfalcon.Engine.SystemData.Current.Items.Keys)
		{
			Debug.Log(key);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
