using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

	//Editor Config
	[Header("Spikes")]
	public int renderDistance;
	public int xGaps;
	public Transform spikesParent;
	public GameObject[] spikePrefabs;
	public Vector2 spikeDistance;
	[Header("Background")]
	public Transform backgroundParent;
	public GameObject backgroundPrefab;
	public int bgWidth;


	//Runtime
	private float myX = 0;
	private List<GameObject> spikesList = new List<GameObject>();
	private List<GameObject> bgList = new List<GameObject>();
	private int bgIndex = 0;

	//Unity functions
	void Start () {

	}

	void Update () {
		if(myX < Camera.main.transform.position.x + renderDistance){
			myX += spikeDistance.x + xGaps;
			PlaceSpike();
		}
		if(bgIndex * bgWidth < Camera.main.transform.position.x + bgWidth){
			UpdateBackground();
		}
	}

    private void UpdateBackground(){
        if(bgList.Count >= 3){
			GameObject oldBG = bgList[0];
			spikesList.Remove(oldBG);
			Destroy(oldBG);
		}
		Vector3 position = new Vector3(bgIndex * bgWidth, 0f, 0f);
		GameObject bg = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity, backgroundParent);
		bg.transform.localPosition = position;
		bgList.Add(bg);
		bgIndex++;
    }

    //Functions
    private void PlaceSpike(){
		if(spikesList.Count >= renderDistance){
			GameObject oldSpike = spikesList[0];
			spikesList.Remove(oldSpike);
			Destroy(oldSpike);
		}
		GameObject prefab = spikePrefabs[Random.Range(0, spikePrefabs.Length)];
		float rndDist = Random.Range(spikeDistance.x, spikeDistance.y);
		Vector3 position = new Vector3(myX - (spikeDistance.y - spikeDistance.x) + rndDist, 0f, 0f);
		GameObject spike = Instantiate(prefab, Vector3.zero, Quaternion.identity, spikesParent);
		spike.transform.localPosition = position;
		spikesList.Add(spike);
	}
}
