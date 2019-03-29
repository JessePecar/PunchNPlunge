//GameDiscovery class handles data from the parsed network discovery.
//Use this struct for normalized GUI functions.

public class GameDiscovery
{
    public string name; //This is just for Identification
    public int players; 
    public string levelName; 
    public string ipAddress; 
    public int port;  
    public string status;  
    
    public GameDiscovery(string newName, int players, string levelName, string ipAddress, int port, string status)
    {
        name = newName;
        this.players = players;
        this.levelName = levelName;
        this.ipAddress = ipAddress;
        this.port = port;
        this.status = status;
    }
}