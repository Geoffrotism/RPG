using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Door : Structure {
	const int HEALTH_FACTOR = 5;
	public bool Open {get; set;} // is the door open?

	public Door() {
		
		this.CurrentHealth = this.MaxHealth = 0;
		this.X = 0f;
		this.Y = 0f;
		this.Z = 0f;
		this.Material_Type = Struct_Type.wood;
		this.Loc_Name = "Generic Wall";
	}

	public void Init(Struct_Type type, float x, float y, float z) {	

		switch (type) {
		case Struct_Type.wood: 
			MaxHealth = this.CurrentHealth = HEALTH_FACTOR;
			break;
		case Struct_Type.iron:
			this.MaxHealth = this.CurrentHealth = HEALTH_FACTOR * 2;
			break;
		case Struct_Type.steel:
			this.MaxHealth = this.CurrentHealth = HEALTH_FACTOR * 2.5;
			break;
		}

		this.X = x;
		this.Y = y;
		this.Z = z;
		this.transform.position = new Vector3 (this.X, this.Y, this.Z);
		this.Material_Type = type;
		this.pathValue = 0f;
	}

	// Update is called once per frame
	void Update () {
	}
}
