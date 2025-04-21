using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SO_DialogTextes", menuName = "Scriptable Objects/SO_DialogTextes")]
public class SO_DialogTextes : ScriptableObject
{
    public string DialogName;
    public int DialogID;

    public int CharaLeft;
    public int CharaRight;

    [SerializeField]
    public List<int> Speaker;

    [SerializeField]
    public List<string> Text;

}
