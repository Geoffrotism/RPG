using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Structure : MonoBehaviour
	{
		// Where is this
		public string Loc_Name {get; set;} // Localized name.
		public float X {get; set;}
		public float Y {get; set;}
		public float Z {get; set;}
		public double MaxHealth {get; set;}
		public double CurrentHealth {get; set;}
		public Struct_Type Material_Type {get; set;}
		public double pathValue { get; set; } // 1 = fully passable, 0 = not passable.

	}
}

