using UnityEngine;

namespace MavLib.Variables
{
	public class ObservableVariableSO<T> : PersistentScriptableObject
	{
		public event System.Action OnDidChange;
		
		
		[SerializeField, ReadOnly] private T value;
		public T Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != null && this.value.Equals(value)) return;
				
				this.value = value;
				OnDidChange?.Invoke();
			}
		}
		
		
		
		public static implicit operator T(ObservableVariableSO<T> obj)
		{
			return obj.Value;
		}
	}
}