using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class timeManager : MonoBehaviour {
	
	public static DateTime getCurrentTimeFromInternet(){
		return GetFastestNISTDate ();
	}
	public static DateTime getCurrentTimeFromLocal(){
		return DateTime.Now;
	}
	public static TimeSpan calTimeSpan(DateTime old){
		DateTime current = getCurrentTimeFromLocal ();
		Debug.Log ("current: "+ current + " old: " + old);
		return current.Subtract (old);
	}

	public static void saveQuitTime(){
		//SaveInPlayerPrefs ();
		SaveInFile ();
	}

	public static DateTime loadOldTime(){
		//return LoadInPlayerPrefs ();
		return LoadInFile ();

	}

	static void SaveInFile(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/gameTimeInfo.dat");

		GameData data = new GameData ();
		data.oldTime = getCurrentTimeFromLocal (); 
		bf.Serialize (file,data);
		file.Close ();
		Debug.Log ("save time: " + data.oldTime);

	}
	static DateTime LoadInFile(){
		if (File.Exists (Application.persistentDataPath + "/gameTimeInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gameTimeInfo.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize(file);
			file.Close();
			Debug.Log(data.oldTime);
			return data.oldTime;
		}
		return new DateTime();
	}

	static void SaveInPlayerPrefs(){
		DateTime old_time = getCurrentTimeFromLocal ();
		PlayerPrefs.SetString ("oldTime", old_time.ToBinary().ToString());
		Debug.Log ("saving old time: " + old_time.ToString());
	}
	static DateTime LoadInPlayerPrefs(){
		//Grab the old time from the player prefs as a long
		long temp = Convert.ToInt64 (PlayerPrefs.GetString("oldTime"));
		DateTime oldDate = DateTime.FromBinary(temp);
		return oldDate;
	}

	[Serializable]
	class GameData{
		public DateTime oldTime;
	}

	public static DateTime GetFastestNISTDate()
	{
		var result = DateTime.MinValue;
		
		// Initialize the list of NIST time servers
		// http://tf.nist.gov/tf-cgi/servers.cgi
		string[] servers = new string[] {
			"nist1-ny.ustiming.org",
			"nist1-nj.ustiming.org",
			"nist1-pa.ustiming.org",
			"time-a.nist.gov",
			"time-b.nist.gov",
			"nist1.aol-va.symmetricom.com",
			"nist1.columbiacountyga.gov",
			"nist1-chi.ustiming.org",
			"nist.expertsmi.com",
			"nist.netservicesgroup.com"
		};
		
		// Try 5 servers in random order to spread the load
		System.Random rnd = new System.Random();
		foreach (string server in servers.OrderBy(s => rnd.NextDouble()).Take(5))
		{
			try
			{
				// Connect to the server (at port 13) and get the response
				string serverResponse = string.Empty;
				using (var reader = new StreamReader(new System.Net.Sockets.TcpClient(server, 13).GetStream()))
				{
					serverResponse = reader.ReadToEnd();
				}
				
				// If a response was received
				if (!string.IsNullOrEmpty(serverResponse))
				{
					// Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
					string[] tokens = serverResponse.Split(' ');
					
					// Check the number of tokens
					if (tokens.Length >= 6)
					{
						// Check the health status
						string health = tokens[5];
						if (health == "0")
						{
							// Get date and time parts from the server response
							string[] dateParts = tokens[1].Split('-');
							string[] timeParts = tokens[2].Split(':');
							
							// Create a DateTime instance
							DateTime utcDateTime = new DateTime(
								Convert.ToInt32(dateParts[0]) + 2000,
								Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]),
								Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
								Convert.ToInt32(timeParts[2]));
							
							// Convert received (UTC) DateTime value to the local timezone
							result = utcDateTime.ToLocalTime();
							
							return result;
							// Response successfully received; exit the loop
							
						}
					}
					
				}
				
			}
			catch
			{
				// Ignore exception and try the next server
			}
		}
		return result;
	}
}
