using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Player player;
    public int a;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                ScoringSystem.ChangePlayerScore(player, a);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
