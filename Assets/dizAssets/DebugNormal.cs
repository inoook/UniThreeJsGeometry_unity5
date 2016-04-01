using UnityEngine;
using System.Collections;

public class DebugNormal : MonoBehaviour {

	public float amp = 0.1f;
	public Color color = Color.red;
	void Update()
	{
		Mesh mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
		Vector3[] normals = mesh.normals;
		Vector3[] vertices = mesh.vertices;
		for(int i = 0; i < normals.Length; i++){
			Vector3 pos = this.transform.TransformPoint( vertices[i] );
			Vector3 dir = this.transform.TransformDirection( normals[i] ) * amp;
			Debug.DrawRay(pos, dir, color);
		}
	}
}
