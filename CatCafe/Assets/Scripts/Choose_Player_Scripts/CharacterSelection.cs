using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }
        //toogle to selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }
    public void ToogleCharacter(bool isLeft)
    {
        characterList[index].SetActive(false);
        if (isLeft)
        {
            index--;
            if (index < 0)
                index = characterList.Length - 1;
        }
        else
        {
            index++;
            if (index == characterList.Length)
                index = 0;
        }
        characterList[index].SetActive(true);
    }
    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
    }
}
