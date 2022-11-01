using Core.WindowService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
	public class AddBugView : BaseView
	{
		[SerializeField] 
		private Button _button;
		public Button Button => _button;
	}
}