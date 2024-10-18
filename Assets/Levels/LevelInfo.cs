using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create Level/New Level", order = 54)]
public class LevelInfo : ScriptableObject
{
    public string Main;
    public string Left;
    public string Right;
    public string Bottom;
    public string Forward;
    public string Back;

    public Cell[,] MainArray => JsonConvert.DeserializeObject<Cell[,]>(Main);

    public IEnumerable<Cell[,]> OtherArray()
    {
        yield return JsonConvert.DeserializeObject<Cell[,]>(Left);
        yield return JsonConvert.DeserializeObject<Cell[,]>(Right);
        yield return JsonConvert.DeserializeObject<Cell[,]>(Bottom);
        yield return JsonConvert.DeserializeObject<Cell[,]>(Forward);
        yield return JsonConvert.DeserializeObject<Cell[,]>(Back);
    }
}
