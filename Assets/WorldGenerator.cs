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
	[Header("Pickups")]
	public GameObject[] pickupPrefabs;
	public Vector2 pickupGaps;
	public Vector2 pickupYrange;


	//Runtime
	private float myX = 0;
	private List<GameObject> spikesList = new List<GameObject>();
	private int bgIndex = 0;
	private List<GameObject> bgList = new List<GameObject>();
	private int pickupIndex = 0;
	private List<GameObject> pickupList = new List<GameObject>();

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
		if(pickupIndex * pickupGaps.x < Camera.main.transform.position.x + renderDistance){
			AddPickup();
		}
	}

    private void AddPickup(){
		GameObject prefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
		float rndDist = Random.Range(pickupGaps.x, pickupGaps.y);
        Vector3 position = new Vector3(rndDist * pickupIndex, Random.Range(pickupYrange.x, pickupYrange.y), 0f);
		GameObject pickup = Instantiate(prefab, Vector3.zero, Quaternion.identity);
		pickup.transform.localPosition = position;
		//bgList.Add(bg)
		pickupIndex++;
    }

    private void UpdateBackground(){
        if(bgList.Count >= 2){
			GameObject oldBG = bgList[0];
			bgList.Remove(oldBG);
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
		if(spikesList.Count >= 12){
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
