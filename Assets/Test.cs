using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	List<Vector2> pts;
	List<Vector2> normals;
	
	int count = 7;

	// Use this for initialization
	void Start () {

		pts = new List<Vector2>();
		normals = new List<Vector2>();

		float normOffset = Mathf.PI / count;
		for ( int i = 0; i < count; i ++ ) {
			float l = 20;
			float a = (float)i / count * Mathf.PI * 2;
			pts.Add( new Vector2 ( Mathf.Cos( a ) * l, Mathf.Sin( a ) * l ) );
			
			normals.Add( new Vector2 ( Mathf.Cos( a + normOffset ), Mathf.Sin( a + normOffset ) ) );
		}

//		float normOffset = Mathf.PI / count + Mathf.PI / 2.0f;
//		for ( int i = 0; i < count; i ++ ) {
//			float l = 20;
//			float a = (float)i / count * Mathf.PI * 2;
//			pts.Add( new Vector2 ( Mathf.Cos( a ) * l, Mathf.Sin( a ) * l ) );
//			
//			normals.Add( new Vector2 ( Mathf.Cos( a + normOffset ), Mathf.Sin( a + normOffset ) ) );
//		}

//		// start
//		int numPts = 5;
//		float normOffset = 0;
//		for ( int i = 0; i < numPts * 2; i ++ ) {
//			int l = i % 2 == 1 ? 10 : 20;
//			float a = (float)i / numPts * Mathf.PI;
//			pts.Add( new Vector2 ( Mathf.Cos( a ) * l, Mathf.Sin( a ) * l ) );
//		}
//		for ( int i = 0; i < pts.Count; i ++ ) {
//			int endI = (i == pts.Count-1) ? 0 : i+1;
//			Vector2 vec = pts[endI] - pts[i];
//			vec.Normalize();
//			normals.Add( new Vector2 ( vec.y, -vec.x ) );
//		}
	}
	
	// Update is called once per frame
	void Update () {
		int count = pts.Count;
		for(int i = 0; i < count; i++){
			Debug.DrawRay(Vector3.zero, pts[i], Color.red);
		}
		for(int i = 0; i < count; i++){
			int endI = (i == pts.Count-1) ? 0 : i+1;
			Debug.DrawLine(pts[i], pts[endI], Color.red);
		}
		for(int i = 0; i < count; i++){
			Debug.DrawRay(pts[i], normals[i] * 10, Color.yellow);
		}
	}
}
