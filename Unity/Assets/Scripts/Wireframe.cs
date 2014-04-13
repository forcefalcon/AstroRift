// attach to an object with a mesh filter 
//vers 12/11/2008

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wireframe : MonoBehaviour
{
	public Color lineColor; 
	public Color backgroundColor;
	public bool ZWrite = true;
	public bool AWrite = true;
	public bool blend = true;
	
	private Vector3[] lines; 
	
	private List<Vector3> linesArray; 
	
	private Material lineMaterial; 
	
	private MeshRenderer meshRenderer;
	
	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		if(!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();
		//meshRenderer.material = new Material("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + (ZWrite ? " ZWrite on " : " ZWrite off ") + (blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") + (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}"); 
				
		// Old Syntax without Bind :
		//   lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite On Cull Front Fog { Mode Off } } } }"); 
		
		// New Syntax with Bind :
		lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite On Cull Front Fog { Mode Off } } } }"); 
		
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		
		linesArray = new List<Vector3>();
		
		MeshFilter filter = GetComponent<MeshFilter>();
		var mesh = filter.sharedMesh;
		var vertices = mesh.vertices;
		var triangles = mesh.triangles;
		
		for (int i = 0; i < triangles.Length / 3; i++)
		{
			linesArray.Add(vertices[triangles[i * 3]]);
			linesArray.Add(vertices[triangles[i * 3 + 1]]);
			linesArray.Add(vertices[triangles[i * 3 + 2]]);
		}
		
		lines = linesArray.ToArray();
	}
	
	void OnRenderObject()
	{
		if (Camera.current.name != "HUD Camera")
		{
			meshRenderer.sharedMaterial.color = backgroundColor;
			lineMaterial.SetPass(0);
			
			GL.PushMatrix();
			GL.MultMatrix(transform.localToWorldMatrix);
			GL.Begin(GL.LINES);
			GL.Color(lineColor);
			
			for (int i = 0; i < lines.Length / 3; i++)
			{
				GL.Vertex(lines[i * 3]);
				GL.Vertex(lines[i * 3 + 1]);
				
				GL.Vertex(lines[i * 3 + 1]);
				GL.Vertex(lines[i * 3 + 2]);
				
				GL.Vertex(lines[i * 3 + 2]);
				GL.Vertex(lines[i * 3]);
			}
			
			GL.End(); 
			GL.PopMatrix();
		}
	}
}