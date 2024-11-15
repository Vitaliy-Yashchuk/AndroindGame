using UnityEngine;

public class ChooseMap : MonoBehaviour
{
    public void ChooseNewMap(int numberMap)
    {
        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().whichMapSelected();
    }
}
