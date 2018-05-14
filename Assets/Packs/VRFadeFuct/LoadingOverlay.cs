using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class LoadingOverlay : MonoBehaviour {

	public Color orgColor = new Color (0, 0, 0, 1);
	public bool fadein = true;
	public bool fadeout = true;

	public float fadespeed1 = .5f;
	public float fadespeed2 = .5f;
	float fadevalue = 1;


    void Start(){
		GetComponent<Renderer> ().material.color = new Color (orgColor.r, orgColor.g, orgColor.b, fadevalue);
        LoadingOverlay.ReverseNormals(this.gameObject);
    }
    
    void Update(){

		if (fadein) {

			if (fadevalue < 0 + .01f) {
				fadevalue = 0;
				GetComponent<Renderer> ().material.color = new Color (orgColor.r, orgColor.g, orgColor.b, fadevalue);
				fadein = false;
			} else {
				fadevalue = Mathf.Lerp (fadevalue, 0, Time.deltaTime * fadespeed1);
				GetComponent<Renderer> ().material.color = new Color (orgColor.r, orgColor.g, orgColor.b, fadevalue);
			}
		}



		if (fadeout) {

			if (fadevalue > 1 - .01f) {
				fadevalue = 1;
				GetComponent<Renderer> ().material.color = new Color (orgColor.r, orgColor.g, orgColor.b, fadevalue);
				fadeout = false;
			} else {
				fadevalue = Mathf.Lerp (fadevalue, 1, Time.deltaTime * fadespeed2);
				GetComponent<Renderer> ().material.color = new Color (orgColor.r, orgColor.g, orgColor.b, fadevalue);
			}
		}



    }

    public static void ReverseNormals(GameObject gameObject){
        // Renders interior of the overlay instead of exterior.
        // Included for ease-of-use. 
        // Public so you can use it, too.
        MeshFilter filter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if(filter != null){
            Mesh mesh = filter.mesh;
            Vector3[] normals = mesh.normals;
            for(int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for(int m = 0; m < mesh.subMeshCount; m++){
                int[] triangles = mesh.GetTriangles(m);
                for(int i = 0; i < triangles.Length; i += 3){
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}
