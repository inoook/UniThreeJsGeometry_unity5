using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UnityMeshUtils {

	public static int checkVertices (UnityEngine.Mesh mesh)
	{
		//var verticesMap = {}; // Hashmap for looking up vertice by position coordinates (and making sure they are unique)
		Dictionary<string, int> verticesMap = new Dictionary<string, int> ();
		List<Vector3> unique = new List<Vector3> ();
		List<int> changes = new List<int> ();
		
		float precisionPoints = 4; // number of decimal points, eg. 4 for epsilon of 0.0001
		float precision = Mathf.Pow (10, precisionPoints);

		Vector3[] vertices = mesh.vertices;
		Debug.Log("org: "+vertices.Length);
		for (int i = 0, il = vertices.Length; i < il; i ++) {
			
			Vector3 v = vertices [i];
			string key = Mathf.Round (v.x * precision).ToString () + '_' + Mathf.Round (v.y * precision).ToString () + '_' + Mathf.Round (v.z * precision).ToString ();
			
			int changeId;
			//if ( verticesMap[ key ] == null ) {
			if (!verticesMap.ContainsKey (key)) {
				verticesMap [key] = i;
				unique.Add (vertices [i]);
				//changes[ i ] = unique.Count - 1;
				changeId = unique.Count - 1;
				
			} else {
				//console.log('Duplicate vertex found. ', i, ' could be using ', verticesMap[key]);
				//changes[ i ] = changes[ verticesMap[ key ] ];
				changeId = changes [verticesMap [key]];
			}
			changes.Add (changeId);
		}
		
//		
//		// if faces are completely degenerate after merging vertices, we
//		// have to remove them from the geometry.
//		List<int> faceIndicesToRemove = new List<int> ();
//		
//		for (int i = 0, il = this.faces.Count; i < il; i ++) {
//			
//			Face3 face = this.faces [i];
//			
//			face.a = changes [face.a];
//			face.b = changes [face.b];
//			face.c = changes [face.c];
//			
//			List<int> indices = new List<int> (new int[]{ face.a, face.b, face.c });
//			
//			//int dupIndex = -1;
//			
//			// if any duplicate vertices are found in a Face3
//			// we have to remove the face as nothing can be saved
//			for (int n = 0; n < 3; n ++) {
//				if (indices [n] == indices [(n + 1) % 3]) {
//					//dupIndex = n;
//					faceIndicesToRemove.Add (i);
//					break;
//				}
//			}
//			
//		}
//		
//		for (int i = faceIndicesToRemove.Count - 1; i >= 0; i --) {
//			int idx = faceIndicesToRemove [i];
//			
//			//this.faces.splice( idx, 1 );
//			//this.faces.RemoveRange( idx, 1 );
//			this.faces.RemoveAt (idx);
//			
//			for (int j = this.faceVertexUvs.Count -1; j >= 0; j--) {
//				this.faceVertexUvs [j].RemoveAt(idx); // CHECK ////////////
//			}
//		}
		
		// Use unique set of vertices
		
		int diff = vertices.Length - unique.Count;
//		vertices = unique;

		Debug.Log("checkVertices: "+diff);
		
		return diff;
	}

	//

	
	public static void MeshSmoothNormals (UnityEngine.Mesh mesh) {
		
		Vector3[] normals = mesh.normals;
		int[] trigs = mesh.triangles;
		
		for(int i = 0; i < trigs.Length; i+=3) {
			
			Vector3 avg = (normals[trigs[i]] + normals[trigs[i+1]] + normals[trigs[i+2]])/3;
			normals[trigs[i]] = avg;
			normals[trigs[i+1]] = avg;
			normals[trigs[i+2]] = avg;
		}
		mesh.normals = normals;
	}
	
	public static void MeshSmoothNormals2 (UnityEngine.Mesh mesh)
	{
		//In C#, you'd add `using System.Collections.Generic` to the top of your file for List<> to work
		Vector3[] normals = mesh.normals;
		List<Vector3>[] vertexNormals = new List<Vector3>[normals.Length]; //array of lists, so each element stores a list of normals for that vertex, to be averaged later
		for(int i = 0; i < vertexNormals.Length; i++){
			vertexNormals[i] = new List<Vector3>();
		}
		
		int[] trigs = mesh.triangles;
		
		//create list of normals for each vertex
		for (int i=0; i<trigs.Length; i+=3) {
			Vector3 currNormal = (normals[trigs[i]] + normals[trigs[i+1]] + normals[trigs[i+2]])/3;/*calculate current triangle's normal*/;
			vertexNormals[trigs[i]].Add(currNormal);
			vertexNormals[trigs[i+1]].Add(currNormal);
			vertexNormals[trigs[i+2]].Add(currNormal);
		}
		
		//now we have the lists, calculate average normal for each vertex from its list of normals
		for (int i=0; i<vertexNormals.Length; i++) {
			normals[i] = Vector3.zero; //ensure the normal starts as a zero vector
			//declared as float so we can divide as float with it later, might be ok as int though?
			float numNormals = vertexNormals[i].Count;
			for (int j=0; j<numNormals; j++) {
				normals[i] += vertexNormals[i][j];
			}
			//gets the average of the normals now they're added together
			normals[i].Scale(new Vector3( 1f/numNormals,1f/numNormals,1f/numNormals) );
		}
		mesh.normals = normals;
	}
}
