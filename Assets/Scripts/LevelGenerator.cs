using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	
	//array of Prefabs, which the script can generate
	public GameObject[] availablePrefabRooms;
	public GameObject[] availablePrefabDecors;    
	public GameObject[] availablePrefabObjects;    
	public GameObject exitGatePrefab;    

	// instanced rooms, so that it can check where the last room ends and if it needs to add more rooms.
	public List<GameObject> currentSceneRooms;
	public List<GameObject> decors;
	public List<GameObject> objects;

	// is distance to attach next room prefab
	private float screenWidthInPoints;

	public float decorMinDistance = 5.0f;    
	public float decorMaxDistance = 10.0f;

	public float decorMinY = -1.4f;
	public float decorMaxY = 1.4f;

	public float objectsMinDistance = 5.0f;    
	public float objectsMaxDistance = 10.0f;

	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;


	public int levelLength = 4;


	// Use this for initialization
	void Start () {
		//calculate the size of the screen in points. 
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	  
	void AddRoom(float farhtestRoomEndX) {
		//Decrement levelLength position
		levelLength -= 1;

		//1 Picks a random index of the room type (Prefab) to generate.
		int randomRoomIndex = Random.Range(0, availablePrefabRooms.Length);
 
		//2 Creates a room object from the array of available rooms using the random index above.
		GameObject room = (GameObject)Instantiate(availablePrefabRooms[randomRoomIndex]);

		//3 Get the size of the floor inside the room, which is equal to the room’s width.
		float roomWidth = room.transform.Find("floor").localScale.x;

		//4 When we set the room position, we set the position of its center 
		// so we add the half room width to the position where the level ends.
		// This way gets the point at which we should add the room, 
		// so that it started straight after the last room.
		float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;

		//5 This sets the position of the room.
		room.transform.position = new Vector3(roomCenter, 0, 0);

		//6 Add the room to the list of current rooms.
		currentSceneRooms.Add(room);         
	}

	void GenerateRoomIfRequired() {
		//1 Creates a new list to store rooms that needs to be removed. 
		// Separate lists are required since you cannot remove items from the list while you iterating through it.
		List<GameObject> roomsToRemove = new List<GameObject>();

		//2 This is a flag that shows if you need to add more rooms. 
		// By default it is set to true, but most of the time it will be set to false inside the foreach.
		bool addRooms = true;        

		//3 Reads player position. 
		float playerX = transform.position.x;

		//4 This is the point after which the room should be removed. 
		// If room position is behind this point (to the left), it needs to be removed. 
		float removeRoomX = playerX - screenWidthInPoints;        

		//5 If there is no room after addRoomX point you need to add a room, 
		// since the end of the level is closer then the screen width.
		float addRoomX = playerX + screenWidthInPoints;

		//6 In farthestRoomEndX you store the point where the level currently ends. 
		// You will use this variable to add new room if required, 
		// since new room should start at that point to make the level seamless.
		float farthestRoomEndX = 0;

		foreach(var room in currentSceneRooms)
		{
			//7 In foreach you simply enumerate current rooms. 
			// You use the floor to get the room width and calculate the roomStartX and roomEndX
			float roomWidth = room.transform.Find("floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);    
			float roomEndX = roomStartX + roomWidth;                            

			//8 If there is a room that starts after addRoomX then you don’t need to add rooms right now. 
			// However there is no break instruction here, since you still need to check if this room needs to be removed.
			if (roomStartX > addRoomX)
				addRooms = false;

			//9 If room ends to the left of removeRoomX point, then it is already off the screen and needs to be removed.
			if (roomEndX < removeRoomX)
				roomsToRemove.Add(room);

			//10 Here you simply find the rightmost point of the level.
			// This will be a point where the level currently ends. It is used only if you need to add a room.
			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}

		//11 This removes rooms that are marked for removal. 
		// The mouse GameObject already flew through them and thus, they are far behind, so you need to remove them.
		foreach(var room in roomsToRemove)
		{
 				currentSceneRooms.Remove(room);
				Destroy(room);            
		}

		//12 If at this point addRooms is still true then the level end is near. 
		// addRooms will be true if it didn’t find a room starting farther then screen width. This indicate that a new room needs to be added.
		if (addRooms)
			AddRoom(farthestRoomEndX);
	}

	void AddDecorations(float lastDecorX) {
		//1
		int randomIndex = Random.Range(0, availablePrefabDecors.Length);
		//	Debug.Log("Decor index: " + randomIndex);
		//2
		GameObject decor = (GameObject)Instantiate(availablePrefabDecors[randomIndex]);

		//3
		float decorPositionX = lastDecorX + Random.Range(decorMinDistance, decorMaxDistance);
		float randomY = Random.Range(decorMinY, decorMaxY);
		decor.transform.position = new Vector3(decorPositionX,randomY,0); 

		//4

		//5
		decors.Add(decor);            
	}

	void AddObject(float lastObjectX) {
		//If end of level reached add Exit object
		bool exitReady = levelLength == 0;
	
		if (exitReady) {
			levelLength -= 1;
		}
		Debug.Log (levelLength);
		//1
		int randomIndex = Random.Range(0, availablePrefabObjects.Length);

		//2
		GameObject obj = exitReady ? 
			(GameObject)Instantiate(exitGatePrefab) : (GameObject)Instantiate(availablePrefabObjects[randomIndex]);
		

		//3
		float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
		float randomY = exitReady ? 0.72f : Random.Range(objectsMinY, objectsMaxY);
		obj.transform.position = new Vector3(objectPositionX,randomY,0); 

		//4
		//float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
		//obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
		//5
		objects.Add(obj);            
	}

	void GenerateDecorsIfRequired() {
		//1
		float playerX = transform.position.x;        
		float removeDecorX = playerX - screenWidthInPoints;
		float addDecorX = playerX + screenWidthInPoints;
		float farthestDecorX = 0;

		//2
		List<GameObject> decorsToRemove = new List<GameObject>();

		foreach (var decor in decors)
		{
			//3
			float decorX = decor.transform.position.x;

			//4
			farthestDecorX = Mathf.Max(farthestDecorX, decorX);

			//5
			if (decorX < removeDecorX)            
				decorsToRemove.Add(decor);
		}

		//6
		foreach (var decor in decorsToRemove)
		{
				decors.Remove(decor);
				Destroy(decor);
		}

		//7
		if (farthestDecorX < addDecorX)
			AddDecorations(farthestDecorX);
	}

	void GenerateObjectsIfRequired() {
		//1
		float playerX = transform.position.x;        
		float removeObjectsX = playerX - screenWidthInPoints;
		float addObjectX = playerX + screenWidthInPoints;
		float farthestObjectX = 0;

		//2
		List<GameObject> objectsToRemove = new List<GameObject>();

		foreach (var obj in objects)
		{
			//3
			float objX = obj.transform.position.x;

			//4
			farthestObjectX = Mathf.Max(farthestObjectX, objX);

			//5
			if (objX < removeObjectsX)            
				objectsToRemove.Add(obj);
		}

		//6
		foreach (var obj in objectsToRemove)
		{
				objects.Remove(obj);
				Destroy(obj);
		}

		//7
		if (farthestObjectX < addObjectX)
			AddObject(farthestObjectX);
	}

	void FixedUpdate () 
	{    
		GenerateRoomIfRequired();
		GenerateDecorsIfRequired();
		GenerateObjectsIfRequired();
	}
}
