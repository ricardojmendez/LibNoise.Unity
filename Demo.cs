using System.Collections;
using LibNoise.Unity;
using LibNoise.Unity.Generator;
using LibNoise.Unity.Operator;
using System.IO;
using System;
using UnityEngine;

public enum NoiseType {Perlin, Billow, RidgedMultifractal, Voronoi, Mix};

public class Demo : MonoBehaviour {
        private Noise2D m_noiseMap = null;
        private Texture2D[] m_textures = new Texture2D[3];
        public int resolution = 64; 
        public NoiseType noise = NoiseType.Perlin;
		float[] clipping;
		float[] new_clipping;
		string[] clipping_labels;
		private int octave_count;
		private float frequency;
		private float persistence;
	
    
    public void Start() {
		clipping = new float[] {0f, 0f, 1f};
		new_clipping = new float[] {0f, 0f, 1f};
		clipping_labels = new string[] {"horizontal offset", "vertical offset", "zoom"};
		
		octave_count = 6;
		frequency = 1f;
		persistence = 0.5f;
		
    	Generate();
		writeImages();
    }
	
	public void Update() {
		if (GUI.changed) {
			for(int i=0; i < 3; i++) {
				clipping[i] = new_clipping[i];
			}
			Generate();
		}
	}	
    
    public void OnGUI() {
    	int y = 0;
    	foreach ( string i in System.Enum.GetNames(typeof(NoiseType)) ) {
    		if (GUI.Button(new Rect(0,y,125,20), i) ) {
    			noise = (NoiseType) Enum.Parse(typeof(NoiseType), i);
    			Generate();
    		}
    		y+=20;
    	}
		
		for(int i=0; i < 3; i++) {
			GUI.Label(new Rect(5,i*20 + 120,100,20), clipping_labels[i] + ": " + new_clipping[i].ToString("F2"));
		}
		
		new_clipping[0] = GUI.HorizontalSlider (new Rect(100, 120, 500,20), new_clipping[0], 0.0f, 100f);
		new_clipping[1] = GUI.HorizontalSlider (new Rect(100, 140, 500,20), new_clipping[1], 0.0f, 100);
		new_clipping[2] = GUI.HorizontalSlider (new Rect(100, 160, 500,20), new_clipping[2], 0.1f, 5.0f);
		
		//Perlin
		if (noise == NoiseType.Perlin) {
			GUI.Label(new Rect(600, 10, 200,20), "Octave Count: " + octave_count);
			octave_count = (int)GUI.HorizontalSlider (new Rect(600, 30, 200,20), octave_count, 1, 6);
			GUI.Label(new Rect(600, 55, 200,20), "Frequency: " + frequency);
			frequency = GUI.HorizontalSlider (new Rect(600, 75, 200,20), frequency, 0f, 16f);
			GUI.Label(new Rect(600, 100, 200,20), "Persistence: " + persistence);
			persistence = GUI.HorizontalSlider (new Rect(600, 120, 200,20), persistence, 0f, 1f);
		
		}
		
		if (GUI.Button(new Rect(0, 200, 125, 20), "Write Images")) {
			writeImages();
		}
    }
    	
    public void Generate() {	
            // Create the module network
            ModuleBase moduleBase;
            switch(noise) {
	            case NoiseType.Billow:	
            	moduleBase = new Billow();
            	break;
            	
	            case NoiseType.RidgedMultifractal:	
            	moduleBase = new RidgedMultifractal();
            	break;   
            	
	            case NoiseType.Voronoi:	
            	moduleBase = new Voronoi();
            	break;             	         	
            	
              	case NoiseType.Mix:            	
            	Perlin perlin = new Perlin();
				perlin.OctaveCount = octave_count;
				perlin.Frequency = frequency;
				perlin.Persistence = persistence;
            	RidgedMultifractal rigged = new RidgedMultifractal();
            	moduleBase = new Add(perlin, rigged);
            	break;
            	
            	default:
				Perlin perlin1 = new Perlin();
				perlin1.OctaveCount = octave_count;
				perlin1.Frequency = frequency;
				perlin1.Persistence = persistence;
            	moduleBase = new Perlin();
				moduleBase = perlin1;
            	break;
            	
            }

            // Initialize the noise map
            this.m_noiseMap = new Noise2D(resolution, resolution, moduleBase);
            this.m_noiseMap.GeneratePlanar(
            clipping[0] - .5/clipping[2], 
            clipping[0] + .5/clipping[2], 
			clipping[1] - .5/clipping[2], 
            clipping[1] + .5/clipping[2]);

            // Generate the textures
            this.m_textures[0] = this.m_noiseMap.GetTexture(LibNoise.Unity.Gradient.Grayscale);
            this.m_textures[0].Apply();
            
            this.m_textures[1] = this.m_noiseMap.GetTexture(LibNoise.Unity.Gradient.Terrain);
            this.m_textures[1].Apply();
             
            this.m_textures[2] = this.m_noiseMap.GetNormalMap(3.0f);
			 this.m_textures[2].Apply();
			 
			 //display on plane
			 renderer.material.mainTexture = m_textures[0];
            
        
    }
	
	private void writeImages() {
		//write images to disk
		File.WriteAllBytes(Application.dataPath + "/../Gray.png", m_textures[0].EncodeToPNG() );
		File.WriteAllBytes(Application.dataPath + "/../Terrain.png", m_textures[1].EncodeToPNG() );
		File.WriteAllBytes(Application.dataPath + "/../Normal.png", m_textures[2].EncodeToPNG() );
		
		Debug.Log("Wrote Textures out to "+Application.dataPath + "/../");
	
	}
    
}