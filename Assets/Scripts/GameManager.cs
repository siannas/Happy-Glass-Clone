using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Pipe pipe;

    private LineDrawer ldrawer;

    private GlassTrigger gtrigger;

    public GameObject win_text;

    // Update is called once per frame
    void Start()
    {
        pipe = FindObjectOfType<Pipe>();

        ldrawer = FindObjectOfType<LineDrawer>();

        ldrawer.drawCallback.AddListener(OnDrawSuccess);

        gtrigger = FindObjectOfType<GlassTrigger>();

        win_text.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnDrawSuccess()
    {
        ldrawer.gameObject.SetActive(false);

        pipe.StartPipe();
    }

    void Update() 
    {
        /// <note> 37 is minimum number of water in the glass to win the game </note>
        if(gtrigger.count >= 37)
        {
            win_text.SetActive(true);
        }
    }
}
