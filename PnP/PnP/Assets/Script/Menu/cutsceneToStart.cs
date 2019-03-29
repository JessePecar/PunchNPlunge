using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneToStart : MonoBehaviour {

    public const float TIME_LIMIT = 15F;
    public int scene;
    // timer variable
    private float timer = 0F;
    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;

        // check if it's time to switch scenes
        if (this.timer >= TIME_LIMIT || Input.GetAxis("Fire1") > 0)
        {
            SceneManager.LoadScene(scene);
        }



    }
}
