using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyCSharp
{
	public class LoadController : ScriptableObject
	{
		public  Dictionary<string, GameObject> AllObjects { get; set; }
		DirectoryInfo baseDir = new DirectoryInfo("Assets/Resources");
		GameObject obj;

		public LoadController ()
		{
			
		}

		public void Awake(){
			AllObjects = new Dictionary<string, GameObject> ();
			iterateDir (baseDir);
		}

		// Iterates over a directory and gets all the prefabs.
		private void iterateDir(DirectoryInfo dir) {
			FileInfo[] info = dir.GetFiles("*.prefab");
			DirectoryInfo[] dirs = dir.GetDirectories ();

			if (info.Length != 0){
				foreach (FileInfo file in info){
					int index = file.FullName.IndexOf ("Resources") + 10;
					int index2 = file.FullName.IndexOf (".prefab") - index;

					obj = Instantiate(Resources.Load(file.FullName.Substring(index, index2)), new Vector3(0,-25,0), Quaternion.identity) as GameObject;
					obj.name = obj.name.Substring (0, obj.name.Length - 7);
					AllObjects.Add (file.Name.Substring(0,file.Name.IndexOf(".prefab")), obj);
                    Debug.LogWarning(file.Name.Substring(0, file.Name.IndexOf(".prefab")));
				}
			}

			foreach (DirectoryInfo d in dirs) {
				iterateDir (d);
			}
		}
	}
}

