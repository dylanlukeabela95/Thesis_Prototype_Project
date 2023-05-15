using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    
    //Resolution
    public Vector2 textureSize = new Vector2(2048, 2048);

    public GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    void Update()
    {
        if (gameManager.clearWhiteboard)
        {
            gameManager.clearWhiteboard = false;
            var r = GetComponent<Renderer>();
            texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
            r.material.mainTexture = texture;
        }
    }
    
}
