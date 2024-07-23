using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;
    public static DataPersistenceManager instance {get; private set;}
    private FileDataHandler dataHandler;
    private int currentIndexScene;

    private void Awake(){
        if(instance != null){
            Debug.Log("Found more than one Data Persistence Manager");
        }
        instance = this;
        
    }

    void Start(){
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObjects = FindAllDataPersistenceObject();
        LoadGame();
    }

    public void NewGame(){
        QuestGiverlv1 questGiver = FindObjectOfType<QuestGiverlv1>();
        QuestGiverlv2 questGiverTP = FindObjectOfType<QuestGiverlv2>();
        QuestGiverlv3 questGiverSM = FindObjectOfType<QuestGiverlv3>();
        // MQuestGiver questGiverSM = FindObjectOfType<MQuestGiver>();

        currentIndexScene = SceneManager.GetActiveScene().buildIndex;

        if (questGiverTP == null){
            Debug.LogError("Script : DataPersistence / No data found for questGiverTP inside error :" + questGiverTP);
        }else{
            Debug.Log("DataFound");
        }

        this.gameData = new GameData(questGiver,questGiverTP,questGiverSM);
    }

    public void LoadGame(){
        gameData = dataHandler.Load();

        if(gameData == null){
            Debug.Log("No data was found, set to defaults");
            NewGame();
        }else{
            Debug.Log("Data Found, succes to load");
        }

        foreach (IDataPersistence dataPersistenceobj in dataPersistencesObjects)
        {
            
            dataPersistenceobj.LoadData(gameData);
        }

        // Debug.Log("LoadedWorld : "+ gameData.GameQuestData[0].questList[0].isCompleted);
        Debug.Log("LoadedWorld : "+ gameData.GameQuestData[0].questList[0].isCompleted);
        RefreshInspector();
    }


    private void RefreshInspector() {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public void SaveGame(){
        foreach (IDataPersistence dataPersistenceobj in dataPersistencesObjects)
        {
            dataPersistenceobj.SaveData(ref gameData);
        }
        // Debug.Log("SavedWorld : "+ gameData.GameQuestData[0].questList[0].isCompleted);
        Debug.Log("SavedWorld : "+ gameData.GameQuestData[0].questList[0].isCompleted);
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit(){
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObject(){
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObjects);
    }


}
