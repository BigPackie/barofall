using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class is for simulating friction as ball has no friction (almost no point of contact), we must do it by Drag
public class Surface : MonoBehaviour {


    public enum Type
    {
        Metal,
        OilyMetal,
        RustyMetal
    }

    public const float Metal = 5f;
    public const float OilyMetal = 1f;
    public const float RustyMetal = 10f;

    public Material metal;
    public Material oilyMetal;
    public Material rustyMetal;

    public Type type = Type.Metal;
    public float drag { get; private set; }

	// Use this for initialization
	void Start () {

        switch (type){
            case (Type.Metal):
                drag = Metal;
                GetComponent<Renderer>().material = metal;
                break;
            case (Type.OilyMetal):
                drag = OilyMetal;
                GetComponent<Renderer>().material = oilyMetal;
                break;
            case (Type.RustyMetal):
                drag = RustyMetal;
                GetComponent<Renderer>().material =  rustyMetal;
                break;
        }

        Debug.Log("Surface drag:" + drag);
	}

    // Update is called once per frame
    void Update () {
		
	}
}
