using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;
    public int index;

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
    public void SelectCharacter(bool isLeft)
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
