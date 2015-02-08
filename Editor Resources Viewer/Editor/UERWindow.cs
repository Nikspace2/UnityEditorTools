////////////////////////////////////////////////////////////////////////////////
//
//  NIGHTSTORM GAMES STUDIOS
//  Copyright 2015 Nightstomr  Games Studio. http://nightstorm.gr
// All Rights Reserved. "$(ProjectName)" Project
// @author Nick Lampadarios http://nicklampadarios.com/
// 
//
//  NOTICE: Nightstomr allow to use, modify, or distribute this file
//  for any purpose
//
//
//  You can load this assets in editor only
//
//  To load an asset: (ASSET_TYPE)EditorGUIUtility.Load("ASSET_PATH")
//	example: Texture2D myImage =  (Texture2D)EditorGUIUtility.Load("brushes/builtin_brush_19.png")
//
//  To load an icon: EditorGUIUtility.IconContent("ICON_NAME").image
//
//  EditorGUIUtility.IconContent loads icons that are inside these folders:
//	icons/
//	icons/generated/
//  
//  and the name of the icon without the file extension.
// 
//  example: 	Texture2D myIcon  =  EditorGUIUtility.IconContent("prefab icon")
//				icons/generated/prefab icon.asset
//
//  example2:	Texture2D myIcon  =  EditorGUIUtility.IconContent("avatarinspector/righthandzoomsilhouette")
//				icons/avatarinspector/righthandzoomsilhouette.png
//
//
////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;


public class UERWindow : EditorWindow {

	string path = "";

	private int showImageTab = 0;
	private int showSkinTab = 0;
	private int showSkin = 0;
	private Vector2 scrollPosition = Vector2.zero;

	List<Texture2D> images;
	List<string> imagePath;
	
	List<GUISkin> guiSkins;
	List<string> guiSkinsPath;

	void LoadSkin(){
		string line = "";
		guiSkins = new List<GUISkin>();
		guiSkinsPath = new List<string>();
		using (StreamReader streamReader = new StreamReader(path)) {
			while((line = streamReader.ReadLine()) != null) {
				if( line.EndsWith(".guiskin")){
					switch (showSkin) {
					case 0:
						if(line.StartsWith("builtin skins/darkskin/skins") || line.StartsWith("builtin skins/generated/darkskin")){
							guiSkins.Add((GUISkin)EditorGUIUtility.Load(line));	
							guiSkinsPath.Add(line);
						}
						break;
					case 1:
						if(line.StartsWith("builtin skins/lightskin/skins") || line.StartsWith("builtin skins/generated/lightskin")){
							guiSkins.Add((GUISkin)EditorGUIUtility.Load(line));	
							guiSkinsPath.Add(line);
						}
						break;
					}
				}
			}
		}
	}


	void LoadImages(){
		string line = "";
		images = new List<Texture2D>();
		imagePath = new List<string>();
		using (StreamReader streamReader = new StreamReader(path)) {
			while((line = streamReader.ReadLine()) != null) {
				if( line.EndsWith(".tga") ||line.EndsWith(".png") || line.EndsWith(".psd") || line.EndsWith(".asset")){

					switch (showImageTab) {
					case 0:
						if(line.StartsWith("avatar")){
							images.Add((Texture2D)EditorGUIUtility.Load(line));	
							imagePath.Add(line);
						}
						break;
					case 1:
						if(line.StartsWith("brushes")){
							images.Add((Texture2D)EditorGUIUtility.Load(line));	
							imagePath.Add(line);
						}
						break;
					case 2:
						if(line.StartsWith("builtin skins")){
							if(showSkin == 0){
								if(line.StartsWith("builtin skins/darkskin")){
									images.Add((Texture2D)EditorGUIUtility.Load(line));	
									imagePath.Add(line);
								}
							}
							else {
								if(line.StartsWith("builtin skins/lightskin")){
									images.Add((Texture2D)EditorGUIUtility.Load(line));	
									imagePath.Add(line);
								}
							}
						}
						break;
					case 3:
						if(line.StartsWith("icons")){
							images.Add((Texture2D)EditorGUIUtility.Load(line));	
							imagePath.Add(line);
					}
						break;
					case 4:
						if(line.StartsWith("previews")){
							images.Add((Texture2D)EditorGUIUtility.Load(line));	
							imagePath.Add(line);
						}
						break;
					case 5:
						if(line.StartsWith("sceneview")){
							images.Add((Texture2D)EditorGUIUtility.Load(line));	
							imagePath.Add(line);
						}
						break;
					}
				}
			}
		}
	}




	void OnGUI(){
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Unity Version : ");
#if UNITY_4_6
		GUILayout.Label("4.6");
#elif UNITY_5
		GUILayout.Label("5.0");
#else
		GUILayout.Label("Only Unity 4.6.2 & Unity 5 is supported");
#endif
		EditorGUILayout.EndHorizontal();


		
#if UNITY_4_6 || UNITY_5

		EditorGUILayout.BeginHorizontal();

		GUILayout.Label("Image Type :");
		if(GUILayout.Button("Avatar")){
			showImageTab = 0;
			showSkinTab = 0;
			LoadImages();
		}
		if(GUILayout.Button("Brushes")){
			showImageTab = 1;
			showSkinTab = 0;
			LoadImages();
		}
		if(GUILayout.Button("Builtin Skins")){
			showImageTab = 2;
			showSkinTab = 0;
			LoadImages();
		}
		if(GUILayout.Button("Icons")){
			showImageTab = 3;
			showSkinTab = 0;
			LoadImages();
		}
		if(GUILayout.Button("Previews")){
			showImageTab = 4;
			showSkinTab = 0;
			LoadImages();
		}
		if(GUILayout.Button("Sceneview")){
			showImageTab = 5;
			showSkinTab = 0;
			LoadImages();
		}
		EditorGUILayout.EndHorizontal();


		if(showImageTab == 2){
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Skin Color :");
			if(GUILayout.Button("Dark Skin")){
				showSkin = 0;
				showSkinTab = 0;
				LoadImages();
			}
			if(GUILayout.Button("Light Skin")){
				showSkin = 1;
				showSkinTab = 0;
				LoadImages();
			}
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			
			GUILayout.Label("File Type :");
			if(GUILayout.Button("Images")){
				showSkinTab = 0;
				LoadImages();

			}
			if(GUILayout.Button("GUI Skins")){
				showSkinTab = 1;
				LoadSkin();
				
			}
			EditorGUILayout.EndHorizontal();


		}

		if(showSkinTab == 0) {
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			for (int i = 0; i < images.Count; i++) {
				EditorGUILayout.BeginHorizontal("box");
				GUILayout.Label(i.ToString() +":");
				GUILayout.Label(images[i]);
				EditorGUILayout.SelectableLabel(imagePath[i]);
				EditorGUILayout.EndHorizontal();

			}
			EditorGUILayout.EndScrollView();
		}
		else{
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			for (int i = 0; i < guiSkins.Count; i++) {
				EditorGUILayout.BeginHorizontal("box");
				EditorGUILayout.ObjectField(i.ToString(),guiSkins[i], typeof(GUISkin), false);
				EditorGUILayout.SelectableLabel(guiSkinsPath[i]);
				EditorGUILayout.EndHorizontal();
				
			}
			EditorGUILayout.EndScrollView();
		}

#endif
	}

	void OnEnable ()
	{
		string[] paths = AssetDatabase.FindAssets("unity edit rec");
		
		foreach (string item in paths) {
			string treuPath = AssetDatabase.GUIDToAssetPath(item);

			#if UNITY_4_6
			if(treuPath.EndsWith("4.6.2.txt"))
				path = treuPath;
			#endif
			#if UNITY_5
			if(treuPath.EndsWith("5.0.txt"))
				path = treuPath;
			#endif
		}

		#if UNITY_4_6_2 || UNITY_5
		LoadImages();
		#endif
	
	}

	[MenuItem ("Window/Tools/Unity Editor Resources Viewer")]
	public static void  Init ()
	{
		UERWindow window = EditorWindow.GetWindow<UERWindow> ("UER Viewer", typeof(UERWindow));
		window.minSize = new Vector2 (400, 200);
	}

}
