using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManagement : MonoBehaviour
{
	public GameObject variables;
	public GameObject smokeLayer, wordLayer, buildingLayer, backLayer;
	public GameObject tabManager;
	public Tile[] structures;
	public Tile[] signs;
	public Tile[] rails;
	public Tile[] acronyms;
	public Tile cloud;
	public bool alwaysDisplayed = false;
	public bool editingClouds = false;
	public bool editable = false;
	bool[][] tempClouds;

    // Start is called before the first frame update
    void Start()
    {
        //set rails
        rails[0] = rails[2];
        //rails[0].transform = rails[0].transform.MultiplyPoint(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)));
        rails[3] = rails[2];
        //rails[3].transform = rails[3].transform * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180));
        rails[5] = rails[2];
        //rails[5].transform = rails[5].transform * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 270));
        rails[1] = rails[4];
        //rails[1].transform = rails[1].transform * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90));
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        Display();
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==1 && editable){
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	editClouds();
        }
        if (editable && !editingClouds){
        	smokeLayer.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        }
        if (editingClouds){
        	smokeLayer.GetComponent<Transform>().localScale = new Vector3(1,1,1);
    		tabManager.GetComponent<TabManagement>().hideTabs = true;
        	cloudShift();
        }
    }

    void Display(){
    	//show and hide
    	if (variables.GetComponent<MainVariables>().whichTab == 3 || alwaysDisplayed){
    		smokeLayer.SetActive(true);
    		wordLayer.SetActive(true);
    		buildingLayer.SetActive(true);
    		backLayer.SetActive(true);
    	}
    	else {
    		smokeLayer.SetActive(false);
    		wordLayer.SetActive(false);
    		buildingLayer.SetActive(false);
    		backLayer.SetActive(false);
    	}
    	bool[][] smokeDisplay = copyArray(variables.GetComponent<MainVariables>().Map_Revealed);
    	if (editingClouds) smokeDisplay = copyArray(tempClouds);
    	for (int i=0; i<9; i++){
    		for (int j=0; j<9; j++){
    			//buildings
    			if (variables.GetComponent<MainVariables>().Buildings[i][j] != -1){
    				int temp = variables.GetComponent<MainVariables>().Buildings[i][j];
    				if (temp < 9){
    					int severity = ProblemLevel(variables.GetComponent<MainVariables>().CHM_Values[temp]);
    					if (severity>0){
	    					wordLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), acronyms[temp]);
	    					buildingLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), signs[severity-1]);
	    				}
	    				else{
	    					wordLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);
	    					buildingLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);	
	    				}
    				}
    				else if (temp<14){
	    				buildingLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), structures[temp-9]);
	    				wordLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);	
    				}
    				else {
    					//rails
	    				buildingLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), rails[(temp-14)%6]);
	    				if ((temp-14)%6 == 1 || (temp-14)%6 == 5) //rotate 90
	    				{
	    					buildingLayer.GetComponent<Tilemap>().RemoveTileFlags(new Vector3Int (-9+j, 3-i, 0), TileFlags.LockTransform);
	    					buildingLayer.GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int (-9+j, 3-i, 0), 
	    							Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one));
	    				}
	    				else if ((temp-14)%6 ==3) //rotate 180
	    				{
	    					buildingLayer.GetComponent<Tilemap>().RemoveTileFlags(new Vector3Int (-9+j, 3-i, 0), TileFlags.LockTransform);
	    					buildingLayer.GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int (-9+j, 3-i, 0), 
	    							Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 180f), Vector3.one));
	    				}
	    				else if ((temp-14)%6 ==0) //rotate 270
	    				{
	    					buildingLayer.GetComponent<Tilemap>().RemoveTileFlags(new Vector3Int (-9+j, 3-i, 0), TileFlags.LockTransform);
	    					buildingLayer.GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int (-9+j, 3-i, 0), 
	    							Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 270f), Vector3.one));
	    				}
	    				wordLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);	
    				}
    			}
    			else {
					wordLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);
					buildingLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);
    			}
    			//smoke
    			if (smokeDisplay[i][j]){
    				smokeLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), null);
    			}
    			else {
    				smokeLayer.GetComponent<Tilemap>().SetTile(new Vector3Int (-9+j, 3-i, 0), cloud);
    			}
				
    		}
    	}

    }

    int ProblemLevel(int PU){
    	if (PU>=30) return 3;
    	if (PU>=20) return 2;
    	if (PU>=10) return 1;
    	return 0;
    }

    public void editClouds(){
    	if (editingClouds){
    		variables.GetComponent<MainVariables>().Map_Revealed = copyArray(tempClouds);
    	}
    	editingClouds = !editingClouds;
    	tabManager.GetComponent<TabManagement>().hideTabs = false;
    	tempClouds = copyArray(variables.GetComponent<MainVariables>().Map_Revealed);
    }

    void cloudShift(){
    	smokeLayer.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0.6f);
    	//smokeLayer.GetComponent<TilemapRenderer>().OrderInLayer = 4;
    	if (Input.GetMouseButtonDown(0) && variables.GetComponent<MainVariables>().whichTab == 3){
    		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		if (mousePos.x <= 0.5f && mousePos.x >= -8.5f && mousePos.y <= 4.5f && mousePos.y >= -4.5f){
    			int temp_x = (int)(mousePos.x + 8.5f);
    			int temp_y = (int)(-1 * mousePos.y + 4.5f);
    			tempClouds[temp_y][temp_x] = !tempClouds[temp_y][temp_x];
    			if (variables.GetComponent<MainVariables>().Buildings[temp_y][temp_x] >= 14){
    				//this is just a temporary solution based on knowing the positions of the rails
    				//in the future, this may need to be done dynamically
    				//if so, check that the current rail and any extra rails connect to eachother
    				//this data is encapulated in the variables
    				for (int a=-2; a<3; a++){
    					for (int b=-2; b<3; b++){
    						if (temp_y+a<9 && temp_y+a>=0 && temp_x+b<9 && temp_x+b>=0){
    							if (variables.GetComponent<MainVariables>().Buildings[temp_y+a][temp_x+b] >= 14 && !(a==0 && b==0)){
    								tempClouds[temp_y+a][temp_x+b] = !tempClouds[temp_y+a][temp_x+b];
    							}
    						}
    					}
    				}
    			}

    		}
    	}
    }

    bool[][] copyArray(bool[][] b){
    	bool[][] temp = {
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9],
    		new bool[9]
    	};
    	for (int i=0; i<9; i++){
    		for (int j=0; j<9; j++){
    			temp[i][j] = b[i][j];
    		}
    	}
    	return temp;
    }
}
