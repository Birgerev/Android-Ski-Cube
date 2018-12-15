using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    private Animator anim;

    public const string menu = "menu";
    public const string game = "game";
    public const string lose = "lose";

    // Use this for initialization
    void Start () {
        instance = this;
        anim = GetComponent<Animator>();

        SetState("menu");
	}
	
	public void SetState (string name) {
        anim.SetTrigger("Change");  //Play the menu change animation

        Transform sectionParent = transform.Find("section");

        for (int i = 0; i < sectionParent.childCount; i++)
        {
            GameObject obj = sectionParent.GetChild(i).gameObject;

            obj.SetActive(false);
        }

        sectionParent.Find(name).gameObject.SetActive(true);
	}
}
