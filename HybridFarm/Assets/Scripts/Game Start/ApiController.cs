using UnityEngine;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System.Text;

public static class ApiController
{
    public static string GetJwtKey()
    {
        try
        {
            string url = "http://20.15.114.131:8080/api/login";
            string body = "{\"apiKey\":\"NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNlOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNA\"}";

            UnityWebRequest request = UnityWebRequest.Post(url, body, "application/json");
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "*/*");

            request.SendWebRequest();

            while (!request.isDone) // Wait for the request to complete
            {
                // You can add a loading indicator or handle progress here if needed
            }

            if ((request.result == UnityWebRequest.Result.ConnectionError) || request.isHttpError)
            {
                Debug.LogError($"Error occurred during JWT key retrieval: {request.error}");
                return null;
            }

            string jsonResponse = request.downloadHandler.text;
            JObject jsonObject = JObject.Parse(jsonResponse);
            string token = (string)jsonObject["token"];
            return token;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public static UserProfile GetUserProfile(string jwtKey)
    {
        if (string.IsNullOrEmpty(jwtKey))
        {
            Debug.LogError("JWT key is null or empty");
            return null;
        }

        try
        {
            string url = "http://20.15.114.131:8080/api/user/profile/view";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + jwtKey);

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonResponse);

            UserProfile userProfile = new UserProfile()
            {
                FirstName = (string)jsonObject["user"]["firstname"],
                LastName = (string)jsonObject["user"]["lastname"],
                UserName = (string)jsonObject["user"]["username"],
                Nic = (string)jsonObject["user"]["nic"],
                PhoneNumber = (string)jsonObject["user"]["phoneNumber"],
                Email = (string)jsonObject["user"]["email"],
                ProfilePictureUrl = (string)jsonObject["user"]["profilePictureUrl"]
            };

            PlayerPrefs.SetString("userName", userProfile.UserName);

            return userProfile;
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during user profile retrieval: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public static string UpdateUserProfile(string jwtKey, UpdateProfileDTO updateProfileObject)
    {
        //Debug.Log(updateProfileObject.GetValueOrDefault("firstname"));
        string url = "http://20.15.114.131:8080/api/user/profile/update";

        string jsonData = JsonUtility.ToJson(updateProfileObject);
        Debug.Log(jsonData);

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + jwtKey);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
            }

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonResponse);

            Debug.Log("Update successful");
            return "success";
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during user profile update: {ex.Message}");
            string exceptionBodyString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            JObject exceptionBody = JObject.Parse(exceptionBodyString);
            return (string)exceptionBody["message"];
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }
}
