using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour {

    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Set in Inspector")]

    public GameObject[] prefabEnemies;
    public float enemySpawnsPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;

    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;

    public WeaponType[] powerUpFrequency = new WeaponType[]
    {
        WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield };



    private BoundsCheck bndCheck;

    public void shipDestroyed(Enemy e)
    {
        //Potentially generate a PowerUp
        if(Random.value <= e.powerUpDropChance)
        {
            //Choose which PowerUp to pic
            //Pick one from the possibilities in powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            //Spawn a PowerUp
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            //Set it to the proper WeaponType
            pu.SetType(puType);
            //Set it to the position of the destroyed ship
            pu.transform.position = e.transform.position;

        }
    }

    void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }

    }

    public void SpawnEnemy()
    {
        Debug.Log("Spawning an enemy");
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
    
    /// <summary>
    /// Static function that gets a WeaponDefinition from the WEAP_DICT static
    /// protected field of the Main class
    /// </summary>
    /// <returns>The WeaponDefition or, if there is no WeaponDefinition with
    /// the WeaponType passed in, returns a new WeaponDefinition with a 
    /// EaponType of none..</returns>
    /// <param name="wt">The WeaponType of the desired WeaponDefintion</param>
     
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
