using UnityEngine;
using System.Collections.Generic;

//This script is based on a custom tag script available at: https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html

public class CustomTag : MonoBehaviour
{
    [SerializeField]

    //Prepares an empty list of tags for the object
    private List<string> tags = new List<string>();

    //Returns true if object has tag
    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    //Returns tags an object has
    public IEnumerable<string> GetTags()
    {
        return tags;
    }

    //Renames given tag on object
    public void Rename(int index, string tagName)
    {
        tags[index] = tagName;
    }

    //Gets tag at specific index
    public string GetAtIndex(int index)
    {
        return tags[index];
    }

    //Returns the number of tags an object has
    public int Count
    {
        get { return tags.Count; }
    }
}