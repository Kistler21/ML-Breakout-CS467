using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    //Declares text object that the life counter will be bound to in Unity
    public Text lives_count;
    static public int lives = 5;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update() => lives_count.text = lives.ToString();
}
