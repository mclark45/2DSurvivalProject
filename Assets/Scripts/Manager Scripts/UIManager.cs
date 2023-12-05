using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] Text _playerAmmoCount;
    // Start is called before the first frame update
    void Start()
    {
        _playerAmmoCount.text = "Ammo Count: 0";
    }

    public void UpdatePlayerAmmoCounter(int ammoCount)
    {
        _playerAmmoCount.text = "Ammo Count: " + ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
