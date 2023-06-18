using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class TerraformingCamera : MonoBehaviour
{
	Vector3 _hitPoint;
	Camera _cam;

	public float BrushSize = 2f;

	// another runtime variable for add "cost" to the terraforming
	public int score = 100; 

	private const string ScoreKey = "Score";

	private bool isTerraforming = false; 

	public GameObject canvasObj;

	public TMP_Text textComponent;

	// Reference to the player character transform
    public Transform playerTransform;

    // Reference to the terrain mesh
    public MeshFilter terrainMeshFilter;

	private void Awake() {
		_cam = GetComponent<Camera>();
	}

    void Start()
    {
        LoadScore();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && isTerraforming == true) // Check if the left mouse button (mouse button 0) is pressed
        {
            if (score > 0)
            {
                score--; // Subtract from the score variable
            }
        }
        else if (Input.GetMouseButton(1) && isTerraforming == true) // Check if the right mouse button (mouse button 1) is pressed
        {
            score++; // Add to the score variable
        }

        textComponent.text = "Score: " + score.ToString();
    }

	private void LateUpdate() {

		// only allow terraforming if game is not paused, aka the canvas object is hidden
		if(!canvasObj.activeInHierarchy) {
			if (Input.GetMouseButton(0) && score != 0) {
				Terraform(true);
			}
			else if (Input.GetMouseButton(1)) {
			Terraform(false);
			}
		}
	}

	private void Terraform(bool add) {
		RaycastHit hit;

		if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, 1000)) {
			Chunk hitChunk = hit.collider.gameObject.GetComponent<Chunk>();

			isTerraforming = true;

			_hitPoint = hit.point;

			hitChunk.EditWeights(_hitPoint, BrushSize, add);

			// logic process to ensure player is always ontop of the terrain mesh even when terraforming directly below the players feet
			float range = 0.5f * BrushSize;
			Vector2 playerPosition2D = new Vector2(playerTransform.position.x, playerTransform.position.z);
			Vector2 hitPoint2D = new Vector2(_hitPoint.x, _hitPoint.z);
			float distance = Vector2.Distance(playerPosition2D, hitPoint2D);
			if (distance <= range && add == true)
			{
				StartCoroutine(AdjustPlayerPosition());
			}
			
		}
		else {
			isTerraforming = false; 
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(_hitPoint, BrushSize);
	}

    private IEnumerator AdjustPlayerPosition()
    {
        Vector3 playerPosition = playerTransform.position;

        // Get the height of the modified terrain at the player's position
        float terrainHeight = GetTerrainHeightAtPlayerPosition();

        // Calculate the difference in height between player and terrain
        float heightDifference = terrainHeight - playerPosition.y;

        // Adjust the player's position until they are above the modified terrain
        const float adjustmentSpeed = 8f;
        float targetHeight = playerPosition.y + heightDifference;
        while (playerPosition.y < targetHeight)
        {
            playerPosition.y += adjustmentSpeed * Time.deltaTime;
            playerTransform.position = playerPosition;
            yield return null;
        }

        // Ensure the player is precisely positioned on top of the modified terrain
        playerPosition.y = targetHeight;
        playerTransform.position = playerPosition;
    }

    private float GetTerrainHeightAtPlayerPosition()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3[] vertices = terrainMeshFilter.mesh.vertices;
        float terrainHeight = float.MinValue;

        // Find the highest terrain height within a small radius around the player's position
        float radius = 1.5f * BrushSize; // Adjust this value as needed
        foreach (Vector3 vertex in vertices)
        {
            float distance = Vector2.Distance(new Vector2(playerPosition.x, playerPosition.z), new Vector2(vertex.x, vertex.z));
            if (distance <= radius && vertex.y > terrainHeight)
            {
                terrainHeight = vertex.y;
            }
        }

        return terrainHeight;
    }

	public void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            score = PlayerPrefs.GetInt(ScoreKey);
        }
    }
}
