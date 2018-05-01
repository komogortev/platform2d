﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour {

	public GameObject[] availableRooms;
	public GameObject[] availableDecors;    
	public GameObject[] availableObjects;    

	public List<GameObject> currentRooms;
	public List<GameObject> decors;
	public List<GameObject> objects;

	private float screenWidthInPoints;

	public float decorMinDistance = 5.0f;    
	public float decorMaxDistance = 10.0f;

	public float decorMinY = -1.4f;
	public float decorMaxY = 1.4f;

	public float objectsMinDistance = 5.0f;    
	public float objectsMaxDistance = 10.0f;

	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;


	// Use this for initialization
	void Start () {
		
		//calculate the size of the screen in points. 
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddRoom(float farhtestRoomEndX)
	{
		//1
		int randomRoomIndex = Random.Range(0, availableRooms.Length);

		//2
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

		//3
		float roomWidth = room.transform.Find("floor").localScale.x;

		//4
		float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;

		//5
		room.transform.position = new Vector3(roomCenter, 0, 0);

		//6
		currentRooms.Add(room);         
	}

	void GenerateRoomIfRequired()
	{
		//1
		List<GameObject> roomsToRemove = new List<GameObject>();

		//2
		bool addRooms = true;        

		//3
		float playerX = transform.position.x;

		//4
		float removeRoomX = playerX - screenWidthInPoints;        

		//5
		float addRoomX = playerX + screenWidthInPoints;

		//6
		float farthestRoomEndX = 0;

		foreach(var room in currentRooms)
		{
			//7
			float roomWidth = room.transform.Find("floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);    
			float roomEndX = roomStartX + roomWidth;                            

			//8
			if (roomStartX > addRoomX)
				addRooms = false;

			//9
			if (roomEndX < removeRoomX)
				roomsToRemove.Add(room);

			//10
			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}

		//11
		foreach(var room in roomsToRemove)
		{
			currentRooms.Remove(room);
			Destroy(room);            
		}

		//12
		if (addRooms)
			AddRoom(farthestRoomEndX);
	}

	void AddDecorations(float lastDecorX)
	{
		//1
		int randomIndex = Random.Range(0, availableDecors.Length);
		Debug.Log("Decor index: " + randomIndex);
		//2
		GameObject decor = (GameObject)Instantiate(availableDecors[randomIndex]);

		//3
		float decorPositionX = lastDecorX + Random.Range(decorMinDistance, decorMaxDistance);
		float randomY = Random.Range(decorMinY, decorMaxY);
		decor.transform.position = new Vector3(decorPositionX,randomY,0); 

		//4

		//5
		decors.Add(decor);            
	}

	void AddObject(float lastObjectX)
	{
		//1
		int randomIndex = Random.Range(0, availableObjects.Length);

		//2
		GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);

		//3
		float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
		float randomY = Random.Range(objectsMinY, objectsMaxY);
		obj.transform.position = new Vector3(objectPositionX,randomY,0); 

		//4
		//float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
		//obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

		//5
		objects.Add(obj);            
	}

	void GenerateDecorsIfRequired()
	{
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

	void GenerateObjectsIfRequired()
	{
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
